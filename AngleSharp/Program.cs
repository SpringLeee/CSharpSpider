
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngleSharp
{
    class Program
    {
        static void Main(string[] args)
        {

            var url = "https://www.cnblogs.com/sitehome/p/";
            
            

            for (int i = 1; i <= 200; i++)
            {
                var Dom = new HtmlParser().Parse(Common.HttpGet(url+i));

                var listitem = Dom.QuerySelectorAll(".post_item").ToList();

                foreach (var item in listitem)
                {
                    var Title = item.QuerySelector("h3").QuerySelector("a").TextContent;

                    var href = item.QuerySelector("h3").QuerySelector("a").GetAttribute("href");

                    var ddom = new HtmlParser().Parse(Common.HttpGet(href));

                    var nickname = ddom.QuerySelector("#blog-news").OuterHtml;
                    

                    Console.WriteLine(Title);

                    Console.WriteLine("/n/r");

                    Console.WriteLine(nickname);
                }


            }

            Console.WriteLine("处理完成");
          
            Console.ReadKey();


        }

    }
}
