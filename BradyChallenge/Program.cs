using BradyChallenge.Controller;
using BradyChallenge.Services;
using System;

namespace BradyChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            IOService iOService = new IOService();
            GeneratorController generatorController = new GeneratorController(iOService);

            Console.WriteLine("To Close application, press any key");
            Console.ReadKey();
        }

    }   
    
}
