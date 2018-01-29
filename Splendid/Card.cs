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
            outputs();
        }
        public Card(int a)
        {
            lines = new string[6];
            lines[0] = lines[1] = lines[2] = lines[3] = lines[4] = lines[5] = "[\t\t]";
            lines[a] = "[\tX\t]";
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
                lines[count--] = "[\t|\t]";
            }
                
        }
        public void readOut()
        {
            for(int i = 0; i < 6; i++)
            {
                Console.WriteLine(lines[i]);
            }
            //Console.Write("{0}\t{1}\n{2} {3} {4} {5} {6}", color, prestige, cost[0], cost[1], cost[2], cost[3], cost[4]);
        }
        public void readOut(int a)
        {
            for (int i = 0; i < 6; i++)
            {
                if (i == 1)
                {
                    continue;
                }
                Console.WriteLine(lines[i]);
            }
        }

        public string color { get; private set; }
        public int prestige { get; private set; }
        public int[] cost { get; private set; }
        public string[] lines { get; private set; }
    }
}
