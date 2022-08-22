using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Repositories;
using FormulaCar.Championships.Importers.Configurations;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using Microsoft.Office.Interop.Excel;

namespace FormulaCar.Championships.Importers.Fetchers
{
    public class RacePitStopFetcher : IPitStopFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly ImportSettings _importSettings;
        private readonly CircuitMapperSettings _circuiteMapper;

        public RacePitStopFetcher(HttpClient httpClient, IOptions<ImportSettings> options, IOptions<CircuitMapperSettings> circuiteOptions)
        {
            _httpClient = httpClient;
            _circuiteMapper = circuiteOptions.Value;
            _importSettings = options.Value;
        }
        public async Task<IEnumerable<RacePitStopForCreation>> Get(string grandPrix, string season)
        {
            HttpClient client = new HttpClient();

            var mappedGradnPrix = _circuiteMapper.Values[grandPrix];

            var response = await client.GetAsync($"https://www.fia.com/events/fia-formula-one-world-championship/season-{season}/{mappedGradnPrix}/race-classification");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            List<RacePitStopForCreation> racePitStopForCreations = new List<RacePitStopForCreation>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseContent);

            var nodes = doc.DocumentNode.Descendants("table").Where(x => x.Attributes["class"].Value == "sticky-enabled").ToList();

            foreach (var htmlNode in nodes)
            {
                var tableType = htmlNode.Descendants("th").Where(x => x.Attributes["class"].Value == "table-head").SingleOrDefault();


                if (tableType.InnerHtml.ToLower() == "RACE - BEST SECTOR TIMES".ToLower() ||
                   // tableType.InnerHtml.ToLower() == "RACE - PIT STOP - SUMMARY".ToLower() ||
                    tableType.InnerHtml.ToLower() == "QUALIFYING - BEST SECTOR TIMES".ToLower() ||
                    tableType.InnerHtml.ToLower() == "QUALIFYING - MAXIMUM SPEEDS".ToLower() ||
                    tableType.InnerHtml.ToLower() == "RACE - MAXIMUM SPEEDS".ToLower())
                {
                    continue;
                }
                if (tableType.InnerHtml.ToLower() == "RACE - PIT STOP - SUMMARY".ToLower())
                {

                    var tableContent = htmlNode.Descendants($"tbody").SingleOrDefault();
                    var drivers = tableContent.Descendants("tr").Skip(1).ToList();
                    int position = 0;
                    foreach (var driver in drivers)
                    {
                        position++;
                        var column = driver.Descendants("td").ToArray();
                        if (column.Length > 5|| column.Length == 1)
                        {
                            break;
                        }
                        var pos = position;
                        var stops = int.Parse(column[3].InnerHtml.ToString());
                        var rawTime = column[4].InnerHtml.ToString();
                        if (rawTime.Contains(':'))
                        {
                            rawTime = "0:" + rawTime;
                        }
                        else
                        {

                            rawTime = "0:0:" + rawTime;
                        }
                        var totalTime = TimeSpan.Parse(rawTime);
                        var newRacePitStopItem = new RacePitStopForCreation()
                        {
                            Postion = pos.ToString(),
                            Driver = column[1].InnerHtml.ToString(),
                            Circuite = grandPrix,
                            Season = int.Parse(season),
                            Count = stops,
                            TotalTime = totalTime
                        };
                        racePitStopForCreations.Add(newRacePitStopItem);
                        if (position >= 20)
                        {
                            break;
                        }
                    }
                }

            }

            return racePitStopForCreations;
        }
    }
}
