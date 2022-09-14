
using HtmlAgilityPack;
using System;
using System.Collections;
using System.Data.SqlTypes;

static class WebScraber
{
    static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument document = web.Load("https://www.timeanddate.no/vaer/?continent=europe&low=c");
        HtmlNode[] nodes = document.DocumentNode.SelectNodes("//td").ToArray();
        List<string> capitals = new List<string>(); ;
        List<string> temperatures = new List<string>();

        //.Where(x => x.InnerHtml.Contains("Amsterdam"))

        foreach (HtmlNode node in nodes)
        {
            string nodeText = node.InnerText;

            // Catch the case when char lenght is 0
            if (nodeText.Length == 0) { continue; }

            // Test if the first char is upper case, if so extract capital string from node
            if (Char.IsUpper(nodeText.Substring(0, 1), 0))
            {
                string[] words = nodeText.Split(' ');
                capitals.Add(words[0]);
                continue;
            }
            // Test if the first char is int, if so extract the information from node
            if (int.TryParse(nodeText.Substring(0, 1), out int n))
            {
                string temperatureString = string.Empty;

                for (int i = 0; i < nodeText.Length; i++)
                {
                    if (Char.IsDigit(nodeText[i]))
                        temperatureString += nodeText[i];
                }

                temperatureString += " C°";
                temperatures.Add(temperatureString);
                continue;

            }
            continue;
        }


        for (int i = 0; i < capitals.Count; i++)
        {
            Console.WriteLine($"{capitals[i]} {temperatures[i]}");
        }




    }
}

