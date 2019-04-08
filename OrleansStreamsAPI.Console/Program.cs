using Microsoft.Owin.Hosting;
using System.Net.Http;

namespace OrleansStreamsAPI.Console
{
    class Program
    {
        static void Main() 
        { 
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress)) 
            {
                System.Console.WriteLine($"Web API listening on {baseAddress}");
                System.Console.ReadLine(); 
            } 
        } 
    }
}