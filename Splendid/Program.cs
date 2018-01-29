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
            while (true)
            {
                gamePlay();
                Console.WriteLine("Play again?");
                if(Console.ReadLine().ToLower()[0] == 'y')
                {
                    continue;
                }
                break;
            }
            
        }

        static void gamePlay()
        {
            string[] players = new string[4];
            Game gameInProg;
            for (; ; )
            {
                Console.WriteLine("Enter Player One:");
                players[0] = Console.ReadLine();
                if (players[0] == "start")
                {
                    Console.WriteLine("Please choose a different name");
                    continue;
                }
                break;
            } //p1 name
            for(; ; )
            {
                Console.WriteLine("Enter Player Two:");
                players[1] = Console.ReadLine();
                if(players[1] == players[0] || players[1] == "start")
                {
                    Console.WriteLine("Please choose a different name");
                    continue;
                }
                break;
            } //p2 name
            if (players[1] == "admin")
            {
                gameInProg = new Game(players[0]);
            }
            else
            {
                for (; ; )
                {
                    Console.WriteLine("Enter Player Three or type \"start\":");
                    players[2] = Console.ReadLine();
                    if (players[2] == players[0] || players[2] == players[1])
                    {
                        Console.WriteLine("Please choose a different name");
                        continue;
                    }
                    break;
                } //p3 name
                if (players[2] == "start")
                {
                    gameInProg = new Game(players[0], players[1]);
                }
                else
                {
                    
                    for (; ; )
                    {
                        Console.WriteLine("Enter Player Four or type \"start\":");
                        players[3] = Console.ReadLine();
                        if (players[3] == players[0] || players[3] == players[1] || players[3] == players[2])
                        {
                            Console.WriteLine("Please choose a different name");
                            continue;
                        }
                        break;
                    } //p4 name
                    if (players[3] == "start")
                    {
                        gameInProg = new Game(players[0], players[1], players[2]);
                    }
                    else
                    {
                        gameInProg = new Game(players[0], players[1], players[2], players[3]);
                    }
                }
            }
            Console.WriteLine("Beginning Game:");
            while (!gameInProg.Round());
            Console.WriteLine("The winner is {0}!!!", gameInProg.winner.name);
        }
    }
}
