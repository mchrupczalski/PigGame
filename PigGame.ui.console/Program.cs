using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PigGame.lib;
using PigGame.lib.Factories;
using PigGame.lib.Models;
using PigGame.lib.Validators;

namespace PigGame.ui.console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var host = new BootStrapper().Bootstrap();
            var game = ActivatorUtilities.CreateInstance<ConsoleGameService>(host.Services);

            game.Run();
        }
    }
}