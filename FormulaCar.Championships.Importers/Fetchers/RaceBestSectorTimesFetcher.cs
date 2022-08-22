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
    public class RaceBestSectorTimesFetcher:IRaceBestSectorTimesFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly ImportSettings _importSettings;
        private readonly CircuitMapperSettings _circuiteMapper;

        public RaceBestSectorTimesFetcher(HttpClient httpClient, IOptions<ImportSettings> options, IOptions<CircuitMapperSettings> circuiteOptions)
        {
            _httpClient = httpClient;
            _circuiteMapper = circuiteOptions.Value;
            _importSettings = options.Value;
        }
        public async Task<IEnumerable<RaceBestSectorTimesForCreationDto>> GetBestSectorTimes(string grandPrix, string season)
        {
            HttpClient client = new HttpClient();

            var mappedGradnPrix = _circuiteMapper.Values[grandPrix];

            var response = await client.GetAsync($"https://www.fia.com/events/fia-formula-one-world-championship/season-{season}/{mappedGradnPrix}/race-classification");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            List<RaceBestSectorTimesForCreationDto> bestSectorTimes = new List<RaceBestSectorTimesForCreationDto>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseContent);

            var nodes = doc.DocumentNode.Descendants("table").Where(x => x.Attributes["class"].Value == "sticky-enabled").ToList();

            foreach (var htmlNode in nodes)
            {
                var tableType = htmlNode.Descendants("th").Where(x => x.Attributes["class"].Value == "table-head").SingleOrDefault();


                if (//tableType.InnerHtml.ToLower() == "RACE - BEST SECTOR TIMES".ToLower() ||
                    tableType.InnerHtml.ToLower() == "RACE - PIT STOP - SUMMARY".ToLower() ||
                    tableType.InnerHtml.ToLower() == "QUALIFYING - BEST SECTOR TIMES".ToLower() ||
                    tableType.InnerHtml.ToLower() == "QUALIFYING - MAXIMUM SPEEDS".ToLower() ||
                    tableType.InnerHtml.ToLower() == "RACE - MAXIMUM SPEEDS".ToLower())
                {
                    continue;
                }
                if (tableType.InnerHtml.ToLower() == "RACE - BEST SECTOR TIMES".ToLower())
                {

                    var tableContent = htmlNode.Descendants($"tbody").SingleOrDefault();
                    var drivers = tableContent.Descendants("tr").Skip(1).ToList();
                    var position = 0;
                    var init = true;
                    foreach (var driver in drivers)
                    {
                        position++;
                        if (init)
                        {
                            init = false;
                            continue;
                        }
                        var column = driver.Descendants("td").ToArray();
                        var pos = column[0].InnerHtml.ToString();
                        var name = column[1].InnerHtml.ToString().Split('.')[1].Trim(' ');
                        var rawTime = column[2].InnerHtml.ToString();
                        var sector = 1;
                        rawTime = "0:0:" + rawTime;
                        var totalTime = TimeSpan.Parse(rawTime);
                        var bestSector = new RaceBestSectorTimesForCreationDto()
                        {
                            Postion = column[0].InnerHtml.ToString(),
                            Driver = name,
                            Circuite = grandPrix,
                            Season = int.Parse(season),Time = totalTime,
                            Sector = sector
                        };
                        bestSectorTimes.Add(bestSector);
                        name = column[3].InnerHtml.ToString().Split('.')[1].Trim(' ');
                        rawTime = column[4].InnerHtml.ToString();
                        sector++;
                        rawTime = "0:0:" + rawTime;
                        totalTime = TimeSpan.Parse(rawTime); 
                        bestSector = new RaceBestSectorTimesForCreationDto()
                        {
                            Postion = column[0].InnerHtml.ToString(),
                            Driver = name,
                            Circuite = grandPrix,
                            Season = int.Parse(season),
                            Time = totalTime,
                            Sector = sector
                        };
                        bestSectorTimes.Add(bestSector);
                        name = column[5].InnerHtml.ToString().Split('.')[1].Trim(' ');
                        rawTime = column[6].InnerHtml.ToString();
                        sector++;
                        rawTime = "0:0:" + rawTime;
                        totalTime = TimeSpan.Parse(rawTime); 
                        bestSector = new RaceBestSectorTimesForCreationDto()
                        {
                            Postion = column[0].InnerHtml.ToString(),
                            Driver = name,
                            Circuite = grandPrix,
                            Season = int.Parse(season),
                            Time = totalTime,
                            Sector = sector
                        };
                        bestSectorTimes.Add(bestSector);


                    }
                }

            }

            return bestSectorTimes;
        }
    }
}
