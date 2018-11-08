
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

            var url = "http://www.juanjuankan.com";


            // 1级页面
            var Dom = new HtmlParser().Parse(Common.HttpGet(url));

            var list1 = Dom.QuerySelector(".main").QuerySelector("ul").QuerySelectorAll("li");

            Dictionary<string, string> Dic = new Dictionary<string, string>();

            Console.WriteLine(" 1 级页面开始 .... ");
 
            foreach (var item in list1)
            {
                var Title = item.QuerySelector("a").GetAttribute("title");
                var href = item.QuerySelector("a").GetAttribute("href");

                Console.WriteLine($" Title {Title} Href  {href}");

                Dic.Add(Title,href); 

            }



            // 2级页面
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" 2级页面开始... ");
            Console.WriteLine();

            foreach (var item in Dic.ToList())
            { 

                 var Dom2 = new HtmlParser().Parse(Common.HttpGet(url+item.Value));

                 var list2 = Dom2.QuerySelector(".playlist").QuerySelectorAll("li");

                 Console.WriteLine($" 当前 {item.Key}  共 {list2.Count()}集 ");


                Console.WriteLine($" 进入到 {item.Key} 播放页面开始 .... ");

                foreach (var item1 in list2)
                { 
                   //Console.WriteLine($" 当前 {item.Key}  第 { list2.ToList().IndexOf(item1) + 1 }集 ");

                    var href = item1.QuerySelector("a").GetAttribute("href");


                    var Dom3 = new HtmlParser().Parse(Common.HttpGet(url +  href));

                    Console.WriteLine(" 进入到播放器页面开始..... ");

                    var list3 = Dom3.QuerySelector(".player").QuerySelector("script").TextContent.Replace("var VideoInfoList=", "").Replace("m3u8云播$$","").Replace("$m3u8","").Replace('"',' ').Split('#');

                    foreach (var item3 in list3)
                    {
                        
                        Console.WriteLine($"{item.Key}  {item3}" );

                    }



                }  

            }  


            Console.ReadKey();

            //for (int i = 1; i <= 200; i++)
            //{
            //    var Dom = new HtmlParser().Parse(Common.HttpGet(url+i));

            //    var listitem = Dom.QuerySelectorAll(".post_item").ToList();

            //    foreach (var item in listitem)
            //    {
            //        var Title = item.QuerySelector("h3").QuerySelector("a").TextContent;

            //        var href = item.QuerySelector("h3").QuerySelector("a").GetAttribute("href");

            //        var ddom = new HtmlParser().Parse(Common.HttpGet(href));

            //        var nickname = ddom.QuerySelector("#blog-news").OuterHtml;


            //        Console.WriteLine(Title);

            //        Console.WriteLine("/n/r");

            //        Console.WriteLine(nickname);
            //    }


            //}

            Console.WriteLine("处理完成");
          
            Console.ReadKey();


        }

    }
}
