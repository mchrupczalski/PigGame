using System;
using DryIoc;
using PigGame.lib;

namespace PigGame.ui.console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var b = new BootStrapper().Bootstrap();
            var game = b.Resolve<Game>();

            var consoleGame = new ConsoleGame(game);
            consoleGame.Play();
            
            Console.ReadKey();
            // setup game
            // add players
            // start game
            // show player name, ask for action
            // do move
            // show result
            // next player
            // loop
        }
    }
}