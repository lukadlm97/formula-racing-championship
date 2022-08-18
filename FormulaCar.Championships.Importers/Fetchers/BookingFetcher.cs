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
    public class BookingFetcher:IBookingfetcher
    {
        private readonly HttpClient _httpClient;
        private readonly ImportSettings _importSettings;

        public BookingFetcher(HttpClient httpClient, IOptions<ImportSettings> options)
        {
            _httpClient = httpClient;
            _importSettings = options.Value;
        }
        public async Task<IEnumerable<BookingDto>> GetBookings()
        {
            List<BookingDto> bookingDtos = new List<BookingDto>();
            var response = await _httpClient.GetAsync(_importSettings.BookingsSourceUrl);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Console.WriteLine(responseContent);
           

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseContent);

            var nodes = doc.DocumentNode.Descendants("table").ToList();
            var bookingNode = nodes[0];

            var bookings = bookingNode.Descendants("tr").ToList();

            var init = true;
            foreach (var booking in bookings)
            {
                if (init)
                {
                    init = false;
                    continue;
                }
                var columns = booking.Descendants("td").ToArray();
                var team = columns[0].Descendants("p").FirstOrDefault().InnerHtml;
                if (team.ToLower().Contains("racing"))
                {
                    //team = team.Replace("Racing","");
                    var splittedTeam = team.Split("Racing");
                    foreach (var s in splittedTeam)
                    {
                        s.Trim(' ');
                    }
                    team = string.Join('\0', splittedTeam);
                }else if (team.ToLower().Contains('-'))
                {
                    team = team.Split('-')[0];
                }
                var driver1 = columns[1].Descendants("p").FirstOrDefault().InnerHtml;
                var driver2 = columns[1].Descendants("p").LastOrDefault().InnerHtml;
                if (driver1.Contains("P&eacute;rez"))
                {
                    driver1 = driver1.Replace("P&eacute;rez", "Perez");
                }
                if (driver2.Contains("P&eacute;rez"))
                {
                    driver2 = driver2.Replace("P&eacute;rez", "Perez");
                } 
                
                if (driver1.Contains("Sebastien"))
                {
                    driver1 = driver1.Replace("Sebastien", "Sebastian");
                }
                if (driver2.Contains("Sebastien"))
                {
                    driver2 = driver2.Replace("Sebastien", "Sebastian");
                }  
                if (driver1.Contains("Antonion"))
                {
                    driver1 = driver1.Replace("Antonion", "Antonio");
                }
                if (driver2.Contains("Antonion"))
                {
                    driver2 = driver2.Replace("Antonion", "Antonio");
                }


                if (driver1.Contains("Jr."))
                {
                    driver1 = driver1.Replace("Jr.", "");
                    driver1 = driver1.Trim(' ');
                }
                if (driver2.Contains("Jr."))
                {
                    driver2 = driver2.Replace("Jr.", "");
                    driver2 = driver2.Trim(' ');
                }

                if (driver1.Contains("R&auml;ikk&ouml;nen"))
                {
                    driver1 = driver1.Replace("R&auml;ikk&ouml;nen", "Raikkonen");
                    driver1 = driver1.Trim(' ');
                }
                if (driver2.Contains("R&auml;ikk&ouml;nen"))
                {
                    driver2 = driver2.Replace("R&auml;ikk&ouml;nen", "Raikkonen");
                    driver2 = driver2.Trim(' ');
                }


                bookingDtos.Add(new BookingDto()
                {
                    Season = _importSettings.Year.ToString(),
                    ConstructorName = team,
                    DriverName = driver1
                });
                bookingDtos.Add(new BookingDto()
                {
                    Season = _importSettings.Year.ToString(),
                    ConstructorName = team,
                    DriverName = driver2
                });
                //var firstApperance = columns[4].Descendants("a").LastOrDefault().InnerHtml;

                Console.WriteLine(team + " [ " + driver1 + " | " + driver2 + "    " + " ] ");

                // Console.WriteLine(constructor.InnerHtml);
            }


            return bookingDtos;
            }

            return null;
        }
        
    }
}
