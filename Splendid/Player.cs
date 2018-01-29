using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splendid
{
    class Player
    {
        public Player(string n)
        {
            name = n;
            reserved = new Card[3] { new Card(2), new Card(2), new Card(2) };
        }
        public string getTokens() //for the love of fuck make a proper parser later
        {
            string outie = $"{stuff[0, 0]} white tokens, {stuff[0, 1]} blue tokens, {stuff[0, 2]} green tokens, {stuff[0, 3]} red tokens, {stuff[0, 4]} black tokens, and {stuff[0, 5]} gold tokens";
            return outie;
        }
        public string getCards()
        {
            string outie = $"{stuff[1, 0]} white cards, {stuff[1, 1]} blue cards, {stuff[1, 2]} green cards, {stuff[1, 3]} red cards, {stuff[1, 4]} black cards, and {stuff[1, 5]} nobles";
            return outie;
        }
        public int tokenSum()
        {
            return stuff[0, 0] + stuff[0, 1] + stuff[0, 2] + stuff[0, 3] + stuff[0, 4] + stuff[0, 5];
        }
        public void displayRes()
        {
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("{0}\t{1}\t{2}", reserved[0].lines[i], reserved[1].lines[i], reserved[2].lines[i]);
            }
        }
        public int[] cardCost(Card cd)
        {
            int[] costArr = new int[6];
            for(int i = 0; i < 5; i++)
            {
                costArr[i] = cd.cost[i] - stuff[1, i];
            }
            for (int i = 0; i < 5; i++)
            {
                if(costArr[i] > stuff[0, i])
                {
                    costArr[5] += (costArr[i] - stuff[0, i]);
                    costArr[i] = stuff[0, i];
                }
            }
            return costArr;
        }
        public void cardTake(Card cd)
        {
            int col = Game.colorGrab(cd.color);
            if(cd.color.ToLower() == "noble")
            {
                col = 5;
            }
            stuff[1, col]++;
            prestige += cd.prestige;
        }

        public void listAll()
        {
            Console.WriteLine("You have {0}", getTokens());
            Console.WriteLine("You have {0}", getCards());
            Console.WriteLine("You have {0} Prestige and are {1} away from winning", prestige, 15-prestige);
            Console.WriteLine("You are reserving these cards:");
            displayRes();
        }
        public void listAll(int a)
        {
            Console.WriteLine("{0} has {1}", name, getTokens());
            Console.WriteLine("{0} has {1}", name, getCards());
            Console.WriteLine("{0} is reserving these cards:", name);
            displayRes();
        }

        public int[,] stuff = new int[2,6]; //first row 0-5 is tokens, second 0-4 is cards, second 5 is nobles
        public Card[] reserved;
        public int res;
        public int prestige { get; private set; }
        public string name { get; private set; }
    }
}
