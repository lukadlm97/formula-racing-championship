﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Importers.Configurations;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;

namespace FormulaCar.Championships.Importers.Fetchers
{
    public class QualificationSpeedTrapFetcher:IQualificationSpeedTrapFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly ImportSettings _importSettings;
        private readonly CircuitMapperSettings _circuiteMapper;

        public QualificationSpeedTrapFetcher(HttpClient httpClient, IOptions<ImportSettings> options, IOptions<CircuitMapperSettings> circuiteOptions)
        {
            _httpClient = httpClient;
            _circuiteMapper = circuiteOptions.Value;
            _importSettings = options.Value;
        }
        public async Task<IEnumerable<QualificationSpeedTrapForCreationDto>> Get(string grandPrix, string season)
        {
            HttpClient client = new HttpClient();

            var mappedGradnPrix = _circuiteMapper.Values[grandPrix];

            var response = await client.GetAsync($"https://www.fia.com/events/fia-formula-one-world-championship/season-{season}/{mappedGradnPrix}/qualifying-classification");
            
            if (!response.IsSuccessStatusCode)
            {
                response = await client.GetAsync($"https://www.fia.com/events/fia-formula-one-world-championship/season-{season}/{mappedGradnPrix}/qualifying");

                if (!response.IsSuccessStatusCode)
                    return null;

            }

            var responseContent = await response.Content.ReadAsStringAsync();
            List<QualificationSpeedTrapForCreationDto> fastestLapDtos = new List<QualificationSpeedTrapForCreationDto>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseContent);

            List<HtmlNode> nodes;
            try
            {
                nodes = doc.DocumentNode.Descendants("table").Where(x => x.Attributes["class"].Value == "sticky-enabled").ToList();

            }
            catch (NullReferenceException)
            {
                return null;
            }
            foreach (var htmlNode in nodes)
            {
                var tableType = htmlNode.Descendants("th").Where(x => x.Attributes["class"].Value == "table-head").SingleOrDefault();


                if (tableType.InnerHtml.ToLower() == "RACE - BEST SECTOR TIMES".ToLower() ||
                    tableType.InnerHtml.ToLower() == "RACE - PIT STOP - SUMMARY".ToLower() ||
                    tableType.InnerHtml.ToLower() == "QUALIFYING - BEST SECTOR TIMES".ToLower() ||
                    tableType.InnerHtml.ToLower() == "QUALIFYING - MAXIMUM SPEEDS".ToLower() ||
                    tableType.InnerHtml.ToLower() == "RACE - MAXIMUM SPEEDS".ToLower())
                {
                    continue;
                }
                if (tableType.InnerHtml.ToLower() == "QUALIFYING - SPEED TRAP".ToLower())
                {

                    var tableContent = htmlNode.Descendants($"tbody").SingleOrDefault();
                    var drivers = tableContent.Descendants("tr").Skip(1).ToList();
                    int position = 1;
                    foreach (var driver in drivers)
                    {
                        var column = driver.Descendants("td").ToArray();
                        var speed = column[3].InnerHtml.ToString();

                        var fastesLap = new QualificationSpeedTrapForCreationDto()
                        {
                            Driver = column[1].InnerHtml.ToString(),
                            Circuite = grandPrix,
                            MaxSpeed = double.Parse(speed, System.Globalization.CultureInfo.InvariantCulture),
                            Season = season.ToString(),
                            Position = column[0].InnerHtml.ToString()
                        };
                        fastestLapDtos.Add(fastesLap);
                        position++;
                    }
                }

            }

            return fastestLapDtos;
        }
    }
}
