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

        }
        public string getTokens() //for the love of fuck make a proper parser later
        {
            string outie = $"{stuff[0, 0]} white tokens, {stuff[0, 1]} blue tokens, {stuff[0, 2]} green tokens, {stuff[0, 3]} red tokens, {stuff[0, 4]} black tokens, and {stuff[0, 5]} gold tokens";
            return outie;
        }
        public int tokenSum()
        {
            return stuff[0, 0] + stuff[0, 1] + stuff[0, 2] + stuff[0, 3] + stuff[0, 4] + stuff[0, 5];
        }

        public int[,] stuff = new int[2,6]; //first row 0-5 is tokens, second 0-4 is cards, second 5 is nobles
        public Card[] reserved;
        public int prestige { get; private set; }
        public string name { get; private set; }
    }
}
