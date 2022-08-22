using HtmlAgilityPack;
using System.Text;


/*
HttpClient client = new HttpClient();


var response = await client.GetAsync($"https://www.fia.com/events/fia-formula-one-world-championship/season-2022/hungarian-grand-prix/qualifying-classification");

var responseContent = await response.Content.ReadAsStringAsync();

if (response.IsSuccessStatusCode)
{
    //Console.WriteLine(responseContent);
}


HtmlDocument doc = new HtmlDocument();
doc.LoadHtml(responseContent);

var nodes = doc.DocumentNode.Descendants("table").Where(x => x.Attributes["class"].Value == "sticky-enabled").ToList();

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

*/


//TODO circuits import
/*
HttpClient client = new HttpClient();


var response = await client.GetAsync($"https://f1.fandom.com/wiki/Circuits");

var responseContent = await response.Content.ReadAsStringAsync();

if (response.IsSuccessStatusCode)
{
    //Console.WriteLine(responseContent);
}

HtmlDocument doc = new HtmlDocument();
doc.LoadHtml(responseContent);

var table = doc.DocumentNode.Descendants("table").FirstOrDefault();

var rows = table.Descendants("tr").ToList();
var init = true;
foreach (var htmlNode in rows)
{
    if (init)
    {
        init = false;
        continue;
    }
    var columns = htmlNode.Descendants("td").ToArray();
    if (columns.Length < 4)
    {
        var defaultColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(htmlNode.InnerHtml);
        Console.ForegroundColor = defaultColor;
        continue;
    }
    var circuit = columns[0].Descendants("a").FirstOrDefault().InnerHtml;
    var location = columns[2].InnerHtml;
    var indexOfNewLine = location.IndexOf("\n");
    location = location.Substring(0, indexOfNewLine);
    var country = columns[3].Descendants("a").LastOrDefault().InnerHtml;
    var race = columns[4].Descendants("a").FirstOrDefault().InnerHtml;
    Console.WriteLine(circuit+" - "+location+ " - " + country + " - " + race);
}
Console.WriteLine(table.InnerHtml);
//Console.WriteLine(table.InnerHtml.ToString());
/*
var rows = table.SelectNodes("//tr");
var init = true;
foreach (var row in rows)
{
    if (init)
    {
        init = false;
        continue;
    }
   // Console.WriteLine(row.InnerHtml);
    var columns = row.SelectNodes("//td");
    foreach (var column in columns)
    {
        Console.WriteLine(column.InnerHtml);
    }
}
*/
/*
foreach (var htmlNode in nodes)
{
    Console.WriteLine(htmlNode.ToString());
}
*/

//TODO constructor import
/*
HttpClient client = new HttpClient();


var response = await client.GetAsync($"https://en.wikipedia.org/wiki/List_of_Formula_One_constructors");

var responseContent = await response.Content.ReadAsStringAsync();

if (response.IsSuccessStatusCode)
{
   // Console.WriteLine(responseContent);
}

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

    Console.WriteLine(team+" ["+engin+"] "+country+"    "+firstApperance);
    
   // Console.WriteLine(constructor.InnerHtml);
}

*/


/*
HttpClient client = new HttpClient();


var response = await client.GetAsync($"https://www.goodwood.com/grr/race/modern/2021/7/2022-f1-drivers-and-teams/");

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
        team = string.Join('\0',splittedTeam);
    }
    var driver1 = columns[1].Descendants("p").FirstOrDefault().InnerHtml;
    var driver2 = columns[1].Descendants("p").LastOrDefault().InnerHtml;
    //var firstApperance = columns[4].Descendants("a").LastOrDefault().InnerHtml;

    Console.WriteLine(team + " [ " + driver1 + " | " + driver2 + "    " + " ] ");

    // Console.WriteLine(constructor.InnerHtml);
}
//TODO: calendar fethcing

var client = new HttpClient();


var response = await client.GetAsync("https://en.wikipedia.org/wiki/2022_Formula_One_World_Championship");

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
var round = 1;
foreach (var grandPrix in singleGrandPrixs)
{
    if (init)
    {
        init = false;
        continue;
    }

    var columns = grandPrix.Descendants("td").ToArray();
    if (columns.Length!=3) continue;
    var fullName = columns[1].Descendants("a").LastOrDefault().InnerHtml;
    var city = fullName;
    if (fullName.Contains(','))
    {
        var parted = fullName.Split(',');
        city = parted[0];
    }


    if (city.ToLower() == "Stavelot".ToLower()) city = "Francorchamps";

    Console.WriteLine(round + "   -   " + city);
    round++;
    // Console.WriteLine(constructor.InnerHtml);
}

*/
//TODO: race result fetcher
/*
HttpClient client = new HttpClient();


var response = await client.GetAsync($"https://www.fia.com/events/fia-formula-one-world-championship/season-2022/bahrain-grand-prix/race-classification");

var responseContent = await response.Content.ReadAsStringAsync();

if (response.IsSuccessStatusCode)
{
    //Console.WriteLine(responseContent);
}


HtmlDocument doc = new HtmlDocument();
doc.LoadHtml(responseContent);

var nodes = doc.DocumentNode.Descendants("table").Where(x => x.Attributes["class"].Value == "sticky-enabled").ToList();

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
        var column = driver.Descendants("td").ToArray();
        stringBuilder.Append(column[0].InnerHtml.ToString()+" "+ column[1].InnerHtml.ToString()+" "+column[2].InnerHtml.ToString()+" "+ column[3].InnerHtml.ToString()+" "+ column[4].InnerHtml.ToString());
        stringBuilder.AppendLine();
        Console.WriteLine(stringBuilder.ToString());

    }
    stringBuilder.Append("==================================================================================");
    stringBuilder.AppendLine();

}

Console.WriteLine(stringBuilder.ToString());
*/


/*

HttpClient client = new HttpClient();


var response = await client.GetAsync($"https://en.wikipedia.org/wiki/List_of_Formula_One_circuits");

var responseContent = await response.Content.ReadAsStringAsync();

if (response.IsSuccessStatusCode)
{
    //Console.WriteLine(responseContent);
}

HtmlDocument doc = new HtmlDocument();
doc.LoadHtml(responseContent);

var tables = doc.DocumentNode.Descendants("table").ToArray();
var table = tables[2];

var rows = table.Descendants("tr").ToList();
var init = true;
foreach (var htmlNode in rows)
{
    if (init)
    {
        init = false;
        continue;
    }
    var columns = htmlNode.Descendants("td").ToArray();
    if (columns.Length < 4)
    {
        var defaultColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(htmlNode.InnerHtml);
        Console.ForegroundColor = defaultColor;
        continue;
    }
    var circuit = columns[0].Descendants("a").FirstOrDefault().InnerHtml;
    var location = columns[4].Descendants("a").FirstOrDefault().InnerHtml;
    var country = columns[4].Descendants("a").LastOrDefault().InnerHtml;
    var race = columns[6].Descendants("a").FirstOrDefault().InnerHtml;
  //  var length = double.Parse(columns[5].Descendants("a").FirstOrDefault().InnerHtml.Split(' ')[0]);
    Console.WriteLine(circuit + " - " + location + " - " + country + " - " + race);
}
Console.WriteLine(table.InnerHtml);



*/



HttpClient client = new HttpClient();


var response = await client.GetAsync($"https://www.fia.com/events/fia-formula-one-world-championship/season-2022/bahrain-grand-prix/race-classification");

var responseContent = await response.Content.ReadAsStringAsync();

if (response.IsSuccessStatusCode)
{
    //Console.WriteLine(responseContent);
}


HtmlDocument doc = new HtmlDocument();
doc.LoadHtml(responseContent);

var nodes = doc.DocumentNode.Descendants("table").Where(x => x.Attributes["class"].Value == "sticky-enabled").ToList();

StringBuilder stringBuilder = new StringBuilder();
foreach (var htmlNode in nodes)
{
    var tableType = htmlNode.Descendants("th").Where(x => x.Attributes["class"].Value == "table-head").SingleOrDefault();
    Console.WriteLine(tableType.InnerHtml);

    if (tableType.InnerHtml.ToLower() == "RACE - BEST SECTOR TIMES".ToLower() ||
      //  tableType.InnerHtml.ToLower() == "RACE - PIT STOP - SUMMARY".ToLower() ||
        tableType.InnerHtml.ToLower() == "QUALIFYING - BEST SECTOR TIMES".ToLower() ||
        tableType.InnerHtml.ToLower() == "QUALIFYING - MAXIMUM SPEEDS".ToLower() ||
        tableType.InnerHtml.ToLower() == "RACE - MAXIMUM SPEEDS".ToLower())
    {
        continue;
    }
    if (tableType.InnerHtml.ToLower() == "RACE - PIT STOP - SUMMARY".ToLower())
    {

        var tableContent = htmlNode.Descendants($"tbody").SingleOrDefault();
        var drivers = tableContent.Descendants("tr").Skip(1).ToList();
        var position = 0;
        foreach (var driver in drivers)
        {
            position++;
            var column = driver.Descendants("td").ToArray();
            var pos = position;
            var name = column[1].InnerHtml.ToString();
            var stops = int.Parse(column[3].InnerHtml.ToString());
            var rawTime = column[4].InnerHtml.ToString();
            if (rawTime.Contains(':'))
            {
                rawTime = "0:" + rawTime;
            }
            else
            {

                rawTime = "0:0:" + rawTime;
            }
            var totalTime = TimeSpan.Parse(rawTime);
            var report = $"{pos}. {name}    -     stops={stops}     [totalTime:{totalTime}]";
            Console.WriteLine(report);
            if (position >= 20)
            {
                break;
            }
        }
    }

}


Console.WriteLine("press any key to close...");

Console.ReadLine();