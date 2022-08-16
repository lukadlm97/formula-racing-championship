using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Domain.Exceptions;
using FormulaCar.Championships.Importers.Configurations;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;

namespace FormulaCar.Championships.Importers.Fetchers
{
    public class ConstructorFetcher:IConstructorFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly ImportSettings _importSettings;

        public ConstructorFetcher(HttpClient httpClient, IOptions<ImportSettings> options)
        {
            _httpClient = httpClient;
            _importSettings = options.Value;
        }
        public async Task<IEnumerable<ConstructorDto>> GetConstructors()
        {
            _httpClient.BaseAddress = new Uri(_importSettings.ConstructorSourceUrl);
            var response = await _httpClient.GetAsync("");


            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ItemsNotFoundException("");
            }

            List<ConstructorDto> contructorDtos = new List<ConstructorDto>();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseContent);

            var nodes = doc.DocumentNode.Descendants("table").ToList();
            var constructorNode = nodes[1];

            var constructors = constructorNode.Descendants("tr").ToList();

            var init = true;
            foreach (var constructor in constructors)
            {
                if (init)
                {
                    init = false;
                    continue;
                }
                var columns = constructor.Descendants("td").ToArray();
                var team = columns[0].Descendants("a").FirstOrDefault().InnerHtml;
                var engin = columns[1].Descendants("a").FirstOrDefault().InnerHtml;
                var country = columns[2].Descendants("a").FirstOrDefault().InnerHtml;
                var firstApperance = columns[4].Descendants("a").LastOrDefault().InnerHtml;

                contructorDtos.Add(new ConstructorDto()
                {
                    Country = country,
                    FirstApperance = firstApperance,
                    Name = team
                });
            }


            return contructorDtos;
        }
    }
}
