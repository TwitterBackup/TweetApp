using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using TwitterBackup.Services.TwitterAPI;

namespace TwitterBackup.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ts = new TwitterService();
            var sN = new List<string>() { "Twitain" }; //"bbcnews", "bbcbreaking", "bbcworld", "bbcarabic", "alarabiya", "cnn", "cnnbrk", "cnnarabic", "reuters", "skynews", "skynewsarabia", "washingtonpost", "ap", "guardian", "nytimes", "time", "wsj", "vgnett", "dagbladet", "Aftenposten", "nrknyheter", "tv2nyhetene", "morgenbladet"
            var result = ts.GetTweetsJson(sN[0].ToString());

            Console.WriteLine(result);
            
            BuildWebHost(args).Run();


        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }


}
