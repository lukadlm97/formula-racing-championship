using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Importers.Configurations;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;

namespace FormulaCar.Championships.Importers.Fetchers;

public class GrandPrixFetcher : IGrandPrixFetcher
{
    private readonly HttpClient _httpClient;
    private readonly ImportSettings _importSettings;

    public GrandPrixFetcher(HttpClient httpClient, IOptions<ImportSettings> options)
    {
        _httpClient = httpClient;
        _importSettings = options.Value;
    }

    public async Task<IEnumerable<GrandPrixForCreation>> GetGrandPrix()
    {
        var grandPrixForCreations = new List<GrandPrixForCreation>();
        var client = new HttpClient();


        var response = await client.GetAsync(_importSettings.Calendar);

        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            // Console.WriteLine(responseContent);
        }

        var doc = new HtmlDocument();
        doc.LoadHtml(responseContent);

        var nodes = doc.DocumentNode.Descendants("table").ToList();
        var grandPrixs = nodes[1];

        var singleGrandPrixs = grandPrixs.Descendants("tr").ToList();

        var init = true;
        var round = 0;
        foreach (var grandPrix in singleGrandPrixs)
        {
            if (init)
            {
                init = false;
                continue;
            }

            var columns = grandPrix.Descendants("td").ToArray();
            if (columns.Length != 3) continue;
            var splited = columns[1].Descendants("a").ToArray();
            var fullName = splited[1].InnerHtml;
          /*  var city = fullName;
            if (fullName.Contains(','))
            {
                var parted = fullName.Split(',');
                city = parted[0];
            }


            if (city.ToLower() == "Stavelot".ToLower()) city = "Francorchamps";
            if (city.ToLower() == "Portimão".ToLower()) city = "Portimao";
            if (city.ToLower() == "Monte Carlo".ToLower()) city = "Monaco";
            if (city.ToLower() == "Sochi".ToLower()) city = "Aldersky";
            if (city.ToLower() == "Lusail".ToLower()) city = "Losail, Qatar";
            Console.WriteLine(round + "   -   " + city);*/
            round++;

            var newGrandPrix = new GrandPrixForCreation
            {
                GrandPrixName = fullName,
                No = round,
                Season = _importSettings.Year
            };
            grandPrixForCreations.Add(newGrandPrix);
        }

        return grandPrixForCreations;
    }
}