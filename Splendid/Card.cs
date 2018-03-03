using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splendid
{
    class Card
    {
        public Card(string inp)
        {
            string[] read = inp.Split('\t');
            color = read[0];
            prestige = int.Parse(read[1]);
            cost = new int[5];
            for(int i = 0; i < 5; i++)
            {
                cost[i] = int.Parse(read[i+2]);
            }
            outputs(0);
        }
        public Card(int a)
        {
            lines = new string[6];
            color = "Nil";
            lines[0] = lines[1] = lines[2] = lines[3] = lines[4] = lines[5] = "";
            lines[a] = "X";
            cost = new int[5] { 100, 100, 100, 100, 100 };
        } // empty card

        public void outputs()
        {
            lines = new string[6];
            lines[0] = $"[{prestige} Pr\t{color}\t]";
            int count = 5;
            if(cost[4] > 0)
            {
                lines[count--] = $"[{cost[4]} Blk\t\t]";
            }
            if (cost[3] > 0)
            {
                lines[count--] = $"[{cost[3]} Red\t\t]";
            }
            if (cost[2] > 0)
            {
                lines[count--] = $"[{cost[2]} Grn\t\t]";
            }
            if (cost[1] > 0)
            {
                lines[count--] = $"[{cost[1]} Blu\t\t]";
            }
            if (cost[0] > 0)
            {
                lines[count--] = $"[{cost[0]} Wht\t\t]";
            }
            while (count > 0)
            {
                lines[count--] = "[\t\t]";
            }
                
        }
        public void outputs(int a)
        {
            lines = new string[5];
            int count = 4;
            if (cost[4] > 0)
            {
                lines[count--] = $"{cost[4]}Blk";
            }
            if (cost[3] > 0)
            {
                lines[count--] = $"{cost[3]}Red";
            }
            if (cost[2] > 0)
            {
                lines[count--] = $"{cost[2]}Grn";
            }
            if (cost[1] > 0)
            {
                lines[count--] = $"{cost[1]}Blu";
            }
            if (cost[0] > 0)
            {
                lines[count--] = $"{cost[0]}Wht";
            }
            while (count >= 0)
            {
                lines[count--] = "";
            }
        }
        public void readOut()
        {
            for(int i = 0; i < 6; i++)
            {
                if (color == "Noble" && i == 1)
                {
                    continue;
                }
                readLine(i);
                Console.WriteLine();
            }
        }
        /*public void readOut(int a)
        {
            for (int i = 0; i < 6; i++)
            {
                if (color == "Noble" && i == 1)
                {
                    continue;
                }
                readLine(i);
            }
        }*/

        public void readLine(int a) // always prints 17 characters
        {
            if(color == "Nil")
            {
                Console.Write("[".PadRight(8) + lines[a].PadRight(8));
            }
            else if (a == 0)
            {
                string temp = $"[{prestige} Pr";
                Console.Write(temp.PadRight(8));
                if(color == "Red ")
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                }
                else if (color == "Blue")
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                }
                else if (color == "Green")
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                }
                Console.Write(color.PadRight(8));
                Console.ResetColor();
            }
            else if(lines[a-1] == "")
            {
                Console.Write("[".PadRight(16));
            }
            else
            {
                Console.Write("[" + lines[a - 1][0] + " ");
                if(lines[a-1].Substring(1) == "Red")
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                }
                else if (lines[a - 1].Substring(1) == "Blu")
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                }
                else if (lines[a - 1].Substring(1) == "Grn")
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                }
                Console.Write(lines[a - 1].Substring(1));
                Console.ResetColor();
                Console.Write("          ");
            }
            Console.Write("]");
        }
        public string color { get; private set; }
        public int prestige { get; private set; }
        public int[] cost { get; private set; }
        public string[] lines { get; private set; }
    }
}