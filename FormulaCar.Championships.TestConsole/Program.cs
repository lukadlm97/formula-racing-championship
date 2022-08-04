
using System.Text;
using HtmlAgilityPack;

HttpClient client = new HttpClient();


var response = await client.GetAsync($"https://www.fia.com/events/fia-formula-one-world-championship/season-2022/hungarian-grand-prix/qualifying-classification");

var responseContent = await response.Content.ReadAsStringAsync();

if (response.IsSuccessStatusCode)
{
    //Console.WriteLine(responseContent);
}


HtmlDocument doc = new HtmlDocument();
doc.LoadHtml(responseContent);

var nodes = doc.DocumentNode.Descendants("table").Where(x => x.Attributes["class"].Value== "sticky-enabled").ToList();

StringBuilder stringBuilder = new StringBuilder();
foreach (var htmlNode in nodes)
{
    var tableType = htmlNode.Descendants("th").Where(x => x.Attributes["class"].Value == "table-head").SingleOrDefault();
    Console.WriteLine(tableType.InnerHtml);
    
    if (tableType.InnerHtml.ToLower() == "RACE - BEST SECTOR TIMES".ToLower() ||
        tableType.InnerHtml.ToLower() == "RACE - PIT STOP - SUMMARY".ToLower() ||
        tableType.InnerHtml.ToLower() == "QUALIFYING - BEST SECTOR TIMES".ToLower() ||
        tableType.InnerHtml.ToLower() == "QUALIFYING - MAXIMUM SPEEDS".ToLower() ||
        tableType.InnerHtml.ToLower() == "RACE - MAXIMUM SPEEDS".ToLower())
    {
        continue;
    }

    stringBuilder.Append("==================================================================================");
    stringBuilder.AppendLine();
    stringBuilder.Append(tableType.InnerHtml);
    stringBuilder.AppendLine();
    var tableContent = htmlNode.Descendants($"tbody").SingleOrDefault();
    var drivers = tableContent.Descendants("tr").Skip(1).ToList();
    foreach (var driver in drivers)
    {
        var position = driver.Descendants("td").Where(x => x.Attributes["class"].Value == "position").SingleOrDefault();
        if (position != null)
        {
            Console.Write(position.InnerHtml);
        }
        var name = driver.Descendants("td").Where(x => x.Attributes["class"].Value == "driver").SingleOrDefault();
        if (name != null)
        {
            Console.Write(name.InnerHtml);
        }

        stringBuilder.Append(position.InnerHtml + "\t" + name.InnerHtml);
        stringBuilder.AppendLine();
        Console.WriteLine();

    }
    stringBuilder.Append("=================================================================================="); 
    stringBuilder.AppendLine();

}

Console.WriteLine(stringBuilder.ToString());

await File.WriteAllTextAsync("hungary-qualifying-22", stringBuilder.ToString());

Console.WriteLine("press any key to close...");

Console.ReadLine();



















