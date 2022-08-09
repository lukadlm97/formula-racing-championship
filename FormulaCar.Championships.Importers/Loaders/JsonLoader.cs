using System.Text.Json;
using FormulaCar.Championships.Contracts;
using FormulaCar.Championships.Importers.Utilities;

namespace FormulaCar.Championships.Importers.Loaders;

public class JsonLoader : IJsonLoader
{
    public async Task<IEnumerable<CountryDto>> GetCountries(string jsonPath)
    {
        var countryDtos = new List<CountryDto>();
        var phisicalPath = Path.Combine(Environment.CurrentDirectory, jsonPath);
        using (StreamReader streamReader = new StreamReader(phisicalPath))
        {
            var countriesJson = streamReader.ReadToEnd();
            var list = JsonSerializer.Deserialize<List<CountryImportFormat>>(countriesJson);
            foreach (var countryImportFormat in list)
            {
                var newCountry = new CountryDto
                {
                    Name = countryImportFormat.name,
                    Code = countryImportFormat.alpha3
                };
                countryDtos.Add(newCountry);
            }

        }
      
        return countryDtos;
    }
}