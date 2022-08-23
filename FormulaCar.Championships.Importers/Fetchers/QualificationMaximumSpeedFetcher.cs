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
    public class QualificationMaximumSpeedFetcher: IQualificationMaximumSpeedFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly ImportSettings _importSettings;
        private readonly CircuitMapperSettings _circuiteMapper;

        public QualificationMaximumSpeedFetcher(HttpClient httpClient, IOptions<ImportSettings> options, IOptions<CircuitMapperSettings> circuiteOptions)
        {
            _httpClient = httpClient;
            _circuiteMapper = circuiteOptions.Value;
            _importSettings = options.Value;
        }
        public async Task<IEnumerable<QualificationMaximumSpeedForCreationDto>> Get(string grandPrix, string season)
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
            List<QualificationMaximumSpeedForCreationDto> qualificationMaximumSpeedForCreationDtos = new List<QualificationMaximumSpeedForCreationDto>();

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


                if (//tableType.InnerHtml.ToLower() == "RACE - BEST SECTOR TIMES".ToLower() ||
                    tableType.InnerHtml.ToLower() == "QUALIFYING - BEST SECTOR TIMES".ToLower() ||
                    tableType.InnerHtml.ToLower() == "QUALIFYING - SPEED TRAP".ToLower()
                      //     || tableType.InnerHtml.ToLower() == "QUALIFYING - MAXIMUM SPEEDS".ToLower()
                      || tableType.InnerHtml.ToLower() == "RACE - MAXIMUM SPEEDS".ToLower()
                    )
                {
                    continue;
                }
                if (tableType.InnerHtml.ToLower() == "QUALIFYING - MAXIMUM SPEEDS".ToLower())
                {

                    var tableContent = htmlNode.Descendants($"tbody").SingleOrDefault();
                    var drivers = tableContent.Descendants("tr").Skip(1).ToList();
                    bool init = true;
                    foreach (var driver in drivers)
                    {
                        if (init)
                        {
                            init = false;
                            continue;
                        }
                        var column = driver.Descendants("td").ToArray();
                        var name = column[1].InnerHtml.ToString().Split(' ')[1].Trim(' ');
                        var kmPerSpeed = column[2].InnerHtml.ToString();
                        int sector = 1;
                        var preform = new QualificationMaximumSpeedForCreationDto()
                        {
                            Position = column[0].InnerHtml.ToString(),
                            Driver = name,
                            Circuite = grandPrix,
                            MaxAvgSpeed = double.Parse(kmPerSpeed, System.Globalization.CultureInfo.InvariantCulture),
                            Season = season.ToString(),
                            Sector = sector.ToString()
                        };
                        qualificationMaximumSpeedForCreationDtos.Add(preform);

                         name = column[3].InnerHtml.ToString().Split(' ')[1].Trim(' ');
                         kmPerSpeed = column[4].InnerHtml.ToString();
                         sector++;
                         preform = new QualificationMaximumSpeedForCreationDto()
                        {
                            Position = column[0].InnerHtml.ToString(),
                            Driver = name,
                            Circuite = grandPrix,
                            MaxAvgSpeed = double.Parse(kmPerSpeed, System.Globalization.CultureInfo.InvariantCulture),
                            Season = season.ToString(),
                             Sector = sector.ToString()
                        };
                        qualificationMaximumSpeedForCreationDtos.Add(preform);

                        name = column[5].InnerHtml.ToString().Split(' ')[1].Trim(' ');
                        kmPerSpeed = column[6].InnerHtml.ToString();
                        sector++;
                        preform = new QualificationMaximumSpeedForCreationDto()
                        {
                            Position = column[0].InnerHtml.ToString(),
                            Driver = name,
                            Circuite = grandPrix,
                            MaxAvgSpeed = double.Parse(kmPerSpeed, System.Globalization.CultureInfo.InvariantCulture),
                            Season = season.ToString(),
                            Sector = sector.ToString()
                        };
                        qualificationMaximumSpeedForCreationDtos.Add(preform);


                    }
                }

            }

            return qualificationMaximumSpeedForCreationDtos;
        }
    }
}
