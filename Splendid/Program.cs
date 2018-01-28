using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splendid
{
    class Program
    {
        static void Main(string[] args)
        {
            gamePlay();
        }

        static void gamePlay()
        {
            Random r = new Random();
            //Game gameInProg = new Game("Player 1", "Player 2", "Player 3", "Player 4");
            Game gameInProg = new Game("Player 1");
            gameInProg.displayField();
            Console.WriteLine("Beginning Game:");
            while (!gameInProg.Round());
            Console.WriteLine("The winner is {0}!!!", gameInProg.winner.name);
        }
    }
}
