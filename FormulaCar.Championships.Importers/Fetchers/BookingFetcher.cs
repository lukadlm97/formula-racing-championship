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
            var response = await _httpClient.GetAsync($"https://www.goodwood.com/grr/race/modern/2021/7/2022-f1-drivers-and-teams/");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Console.WriteLine(responseContent);
            }

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
                }
                var driver1 = columns[1].Descendants("p").FirstOrDefault().InnerHtml;
                var driver2 = columns[1].Descendants("p").LastOrDefault().InnerHtml;
                bookingDtos.Add(new BookingDto()
                {
                    Season = 2022.ToString(),
                    ConstructorName = team,
                    DriverName = driver1
                });
                bookingDtos.Add(new BookingDto()
                {
                    Season = 2022.ToString(),
                    ConstructorName = team,
                    DriverName = driver2
                });
                //var firstApperance = columns[4].Descendants("a").LastOrDefault().InnerHtml;

                Console.WriteLine(team + " [ " + driver1 + " | " + driver2 + "    " + " ] ");

                // Console.WriteLine(constructor.InnerHtml);
            }


            return bookingDtos;
        }
    }
}
