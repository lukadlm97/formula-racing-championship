﻿using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Importers.Configurations;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;

namespace FormulaCar.Championships.Importers.Fetchers;

public class CircuiteFetcher : ICircuitFetcher
{
    private readonly HttpClient _httpClient;
    private readonly ImportSettings _importSettings;

    public CircuiteFetcher(HttpClient httpClient, IOptions<ImportSettings> options)
    {
        _httpClient = httpClient;
        _importSettings = options.Value;
    }

    public async Task<IEnumerable<CircuitDto>> GetCircuites()
    {
        _httpClient.BaseAddress = new Uri(_importSettings.CircuiteSourceUrl);
        var response = await _httpClient.GetAsync("");

        var responseContent = await response.Content.ReadAsStringAsync();

        var circuitDtos = new List<CircuitDto>();
        if (response.IsSuccessStatusCode)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(responseContent);


            var tables = doc.DocumentNode.Descendants("table").ToArray();
            var table = tables[2];

            var rows = table.Descendants("tr").ToList();
            var init = true;
            foreach (var htmlNode in rows)
            {
                if (init)
                {
                    init = false;
                    continue;
                }
                var columns = htmlNode.Descendants("td").ToArray();
                if (columns.Length < 4)
                {
                    var defaultColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(htmlNode.InnerHtml);
                    Console.ForegroundColor = defaultColor;
                    continue;
                }
                var circuit = columns[0].Descendants("a").FirstOrDefault().InnerHtml;
                var location = columns[4].Descendants("a").FirstOrDefault().InnerHtml;
                var country = columns[4].Descendants("a").LastOrDefault().InnerHtml;
                var race = columns[6].Descendants("a").FirstOrDefault().InnerHtml;
                //  var length = double.Parse(columns[5].Descendants("a").FirstOrDefault().InnerHtml.Split(' ')[0]);
                Console.WriteLine(circuit + " - " + location + " - " + country + " - " + race);
                var circuiteDto = new CircuitDto()
                {
                    City = location,
                    CountryCode = country,
                    Name = circuit
                };
                circuitDtos.Add(circuiteDto);
            }
            
        }

        return circuitDtos;
    }
}