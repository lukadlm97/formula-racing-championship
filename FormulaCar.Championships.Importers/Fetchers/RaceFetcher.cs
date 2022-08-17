using System;
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
    public class RaceFetcher:IRaceFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly ImportSettings _importSettings;
        private readonly CircuitMapperSettings _circuiteMapper;

        public RaceFetcher(HttpClient httpClient, IOptions<ImportSettings> options,IOptions<CircuitMapperSettings> circuiteOptions)
        {
            _httpClient = httpClient;
            _circuiteMapper = circuiteOptions.Value;
            _importSettings = options.Value;
        }

        public async Task<IEnumerable<RaceResultItemForCreationDto>> GetRaceResults(string grandPrix,int season)
        {
            HttpClient client = new HttpClient();
            var mappedGradnPrix = _circuiteMapper.Values["Bahrain International Circuit"];

            var response = await client.GetAsync($"https://www.fia.com/events/fia-formula-one-world-championship/season-2022/{mappedGradnPrix}/race-classification");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            List<RaceResultItemForCreationDto> raceResultItemForCreationDtos = new List<RaceResultItemForCreationDto>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseContent);

            var nodes = doc.DocumentNode.Descendants("table").Where(x => x.Attributes["class"].Value == "sticky-enabled").ToList();
            
            foreach (var htmlNode in nodes)
            {
                var tableType = htmlNode.Descendants("th").Where(x => x.Attributes["class"].Value == "table-head").SingleOrDefault();
                Console.WriteLine(tableType.InnerHtml);

                if (tableType.InnerHtml.ToLower() == "RACE - BEST SECTOR TIMES".ToLower() ||
                    tableType.InnerHtml.ToLower() == "RACE - PIT STOP - SUMMARY".ToLower() ||
                    tableType.InnerHtml.ToLower() == "QUALIFYING - BEST SECTOR TIMES".ToLower() ||
                    tableType.InnerHtml.ToLower() == "QUALIFYING - MAXIMUM SPEEDS".ToLower() ||
                    tableType.InnerHtml.ToLower() == "RACE - MAXIMUM SPEEDS".ToLower())
                {
                    continue;
                }

                if (tableType.InnerHtml.ToLower() == "RACE - CLASSIFICATION".ToLower())
                {

                    var tableContent = htmlNode.Descendants($"tbody").SingleOrDefault();
                    var drivers = tableContent.Descendants("tr").Skip(1).ToList();
                    foreach (var driver in drivers)
                    {
                        var column = driver.Descendants("td").ToArray();
                        var result = new RaceResultItemForCreationDto()
                        {
                            Driver = column[1].InnerHtml.ToString(),
                            Postion = column[0].InnerHtml.ToString(),
                            Laps = Int32.Parse(column[3].InnerHtml.ToString()),
                            Time = TimeSpan.Parse(column[4].InnerHtml.ToString()),
                            Raceweek = grandPrix+"-"+ season
                        };
                        raceResultItemForCreationDtos.Add(result);

                    }
                }
                

            }

            return raceResultItemForCreationDtos;
        }
    }
}
