using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;
using HtmlAgilityPack;

namespace FormulaCar.Championships.Importers.Fetchers
{
    public class EngineFetcher:IEngineFetcher
    {
        public async Task<IEnumerable<EngineForCreationDto>> GetEngines()
        {
            HttpClient client = new HttpClient();


            var response = await client.GetAsync($"https://en.wikipedia.org/wiki/List_of_Formula_One_engine_manufacturers");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseContent);

            var nodes = doc.DocumentNode.Descendants("table").ToList();
            var manufacturerNode = nodes[1];

            var manufacturers = manufacturerNode.Descendants("tr").ToList();

            var init = true;
            List<EngineForCreationDto> engineForCreationDtos = new List<EngineForCreationDto>();
            foreach (var manufacturer in manufacturers)
            {
                if (init)
                {
                    init = false;
                    continue;
                }
                var columns = manufacturer.Descendants("td").ToArray();
                if (columns.Length > 0)
                {
                    var manufacturerName = columns[0].Descendants("a").FirstOrDefault().InnerHtml;
                    var country = columns[1].Descendants("a").FirstOrDefault().InnerHtml;
                    var newEngine = new EngineForCreationDto()
                    {
                        Country = country,
                        Manufacturer = manufacturerName,
                        IsActive = true,
                        FirstRun =false
                    };
                    engineForCreationDtos.Add(newEngine);
                }
            }


            nodes = doc.DocumentNode.Descendants("table").ToList();
            manufacturerNode = nodes[2];

            manufacturers = manufacturerNode.Descendants("tr").ToList();

            init = true;
            foreach (var manufacturer in manufacturers)
            {
                if (init)
                {
                    init = false;
                    continue;
                }
                var columns = manufacturer.Descendants("td").ToArray();
                if (columns.Length > 0)
                {
                    var manufacturerName = columns[0].Descendants("a").FirstOrDefault().InnerHtml;
                    var country = columns[1].Descendants("a").FirstOrDefault().InnerHtml;
                    var newEngine = new EngineForCreationDto()
                    {
                        Country = country,
                        Manufacturer = manufacturerName,
                        IsActive = false,
                        FirstRun = false
                    };
                    engineForCreationDtos.Add(newEngine);
                }


                // Console.WriteLine(constructor.InnerHtml);
            }

            return engineForCreationDtos;
        }
    }
}
