using System;
using System.Threading;
using Microsoft.Owin.Hosting;

namespace Api
{
    class Program
    {
        private const string BaseAddress = "http://*:5000";

        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>(url: BaseAddress))
            {
                Console.WriteLine("Application started...");
                string readVal = Console.ReadLine();
                while (string.IsNullOrEmpty(readVal))
                {
                    Thread.Sleep(5000);
                    readVal = Console.ReadLine();
                }
            }
        }
    }
}
