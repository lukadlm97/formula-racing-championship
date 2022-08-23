using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Importers.Configurations;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;

namespace FormulaCar.Championships.Importers.Fetchers
{
    public class QualificationClassificationFetcher:IQualificationClassificationFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly ImportSettings _importSettings;
        private readonly CircuitMapperSettings _circuiteMapper;

        public QualificationClassificationFetcher(HttpClient httpClient, IOptions<ImportSettings> options, IOptions<CircuitMapperSettings> circuiteOptions)
        {
            _httpClient = httpClient;
            _circuiteMapper = circuiteOptions.Value;
            _importSettings = options.Value;
        }
        public async Task<IEnumerable<QualificationClassificationForCreationDto>> Get(string grandPrix, string season)
        {
            HttpClient client = new HttpClient();
            var mappedGradnPrix = _circuiteMapper.Values[grandPrix];

            var response = await client.GetAsync($"https://www.fia.com/events/fia-formula-one-world-championship/season-{season}/{mappedGradnPrix}/qualifying-classification");
            

            if (!response.IsSuccessStatusCode)
            {
                response = await client.GetAsync($"https://www.fia.com/events/fia-formula-one-world-championship/season-{season}/{mappedGradnPrix}/qualifying");

                if(!response.IsSuccessStatusCode)
                    return null;
                
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            List<QualificationClassificationForCreationDto> qualificationClassifications = new List<QualificationClassificationForCreationDto>();

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
                    tableType.InnerHtml.ToLower() == "QUALIFYING - SPEED TRAP".ToLower() ||
                    tableType.InnerHtml.ToLower() == "QUALIFYING - MAXIMUM SPEEDS".ToLower()
                  //  || tableType.InnerHtml.ToLower() == "RACE - MAXIMUM SPEEDS".ToLower()
                    )
                {
                    continue;
                }
                if (tableType.InnerHtml.ToLower() == "QUALIFYING - CLASSIFICATION".ToLower())
                {

                    var tableContent = htmlNode.Descendants($"tbody").SingleOrDefault();
                    var drivers = tableContent.Descendants("tr").Skip(1).ToList();
                 
                    foreach (var driver in drivers)
                    {
                        
                        var column = driver.Descendants("td").ToArray();
                        var name = column[1].InnerHtml.ToString();
                        var rawTime = column[2].InnerHtml.ToString();
                        var sector = 1;
                        if (string.IsNullOrWhiteSpace(rawTime))
                        {
                            rawTime = "0:0.000";
                        }
                        rawTime = "0:" + rawTime;
                        var totalTime = TimeSpan.Parse(rawTime);
                        var preform = new QualificationClassificationForCreationDto()
                        {
                            Position = column[0].InnerHtml.ToString(),
                            Driver = name,
                            Circuite = grandPrix,
                            Time = totalTime,
                            QualificationPeriod = "Q1",
                            Laps = int.Parse(column[3].InnerHtml.ToString())
                        };
                     
                        qualificationClassifications.Add(preform);
                        if (string.IsNullOrWhiteSpace(column[4].InnerHtml.ToString()))
                        {
                            continue;
                        }
                        rawTime = column[4].InnerHtml.ToString();
                        sector++;
                        rawTime = "0:" + rawTime;
                        totalTime = TimeSpan.Parse(rawTime);
                        preform = new QualificationClassificationForCreationDto()
                        {
                            Position = column[0].InnerHtml.ToString(),
                            Driver = name,
                            Circuite = grandPrix,
                            Time = totalTime,
                            QualificationPeriod = "Q2",
                            Laps = int.Parse(column[5].InnerHtml.ToString())
                        };
                        qualificationClassifications.Add(preform);
                        if (string.IsNullOrWhiteSpace(column[6].InnerHtml.ToString()))
                        {
                            continue;
                        }
                        rawTime = column[6].InnerHtml.ToString();
                        sector++;
                        rawTime = "0:" + rawTime;
                        totalTime = TimeSpan.Parse(rawTime);
                        preform = new QualificationClassificationForCreationDto()
                        {
                            Position = column[0].InnerHtml.ToString(),
                            Driver = name,
                            Circuite = grandPrix,
                            Time = totalTime,
                            QualificationPeriod = "Q3",
                            Laps = int.Parse(column[7].InnerHtml.ToString())
                        };
                        qualificationClassifications.Add(preform);


                    }
                }

            }

            return qualificationClassifications;
        }
    }
}
