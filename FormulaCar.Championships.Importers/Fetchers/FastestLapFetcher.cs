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
    public class FastestLapFetcher:IFastestLapFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly ImportSettings _importSettings;
        private readonly CircuitMapperSettings _circuiteMapper;

        public FastestLapFetcher(HttpClient httpClient, IOptions<ImportSettings> options, IOptions<CircuitMapperSettings> circuiteOptions)
        {
            _httpClient = httpClient;
            _circuiteMapper = circuiteOptions.Value;
            _importSettings = options.Value;
        }
        public async Task<IEnumerable<RaceFastestLapForCreationDto>> GetFastestLaps(string grandPrix, string season)
        {
            HttpClient client = new HttpClient();

            var mappedGradnPrix = _circuiteMapper.Values[grandPrix];

            var response = await client.GetAsync($"https://www.fia.com/events/fia-formula-one-world-championship/season-{season}/{mappedGradnPrix}/race-classification");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            List<RaceFastestLapForCreationDto> fastestLapDtos = new List<RaceFastestLapForCreationDto>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseContent);

            var nodes = doc.DocumentNode.Descendants("table").Where(x => x.Attributes["class"].Value == "sticky-enabled").ToList();
            
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
                if (tableType.InnerHtml.ToLower() == "RACE - FASTEST LAPS".ToLower())
                {

                    var tableContent = htmlNode.Descendants($"tbody").SingleOrDefault();
                    var drivers = tableContent.Descendants("tr").Skip(1).ToList();
                    foreach (var driver in drivers)
                    {
                        var column = driver.Descendants("td").ToArray();
                        var time = column[2].InnerHtml.ToString();
                        var gap = column[4].InnerHtml.ToString();
                        var localTime = column[6].InnerHtml.ToString();
                        if (string.IsNullOrWhiteSpace(time))
                        {
                            time = "0";
                        }

                        if (string.IsNullOrWhiteSpace(gap))
                        {
                            gap = "0";
                        }
                        if (string.IsNullOrWhiteSpace(localTime))
                        {
                            localTime = "0";
                        }

                        time = "0:" + time;
                        gap = "0:0:" + gap.Split('+')[1];
                        var speed = column[5].InnerHtml.ToString();

                        var fastesLap = new RaceFastestLapForCreationDto()
                        {
                            Postion = column[0].InnerHtml.ToString(),
                            AvgSpeed = double.Parse(speed, System.Globalization.CultureInfo.InvariantCulture),
                            Driver = column[1].InnerHtml.ToString(),
                            Circuite = grandPrix,
                            Gap = TimeSpan.Parse(gap),
                            Lap = Int32.Parse(column[3].InnerHtml.ToString()),
                            LapTime = TimeSpan.Parse(time),
                            RegistrationTime = DateTime.Parse(localTime),
                            Season = int.Parse(season)
                        };
                        fastestLapDtos.Add(fastesLap);
                    }
                }

            }

            return fastestLapDtos;
        }
    }
}
