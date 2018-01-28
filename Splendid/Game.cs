using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splendid
{
    class Game
    {
        public Game(string a, string b)
        {
            playCt = 2;
            players = new Player[2] { new Player(a), new Player(b) };
            alwaysDo(4);
        } //2p
        public Game(string a, string b, string c)
        {
            playCt = 3;
            players = new Player[3] { new Player(a), new Player(b), new Player(c) };
            alwaysDo(5);
        } //3p
        public Game(string a, string b, string c, string d)
        {
            playCt = 4;
            players = new Player[4] { new Player(a), new Player(b), new Player(c), new Player(d) };
            alwaysDo(7);
        } //4p
        private void alwaysDo(int a)
        {
            for (int i = 0; i < 5; i++)
            {
                tokens[i] = a;
            }
            tokens[5] = 5;
            winner = null;
            rounds = 1;
            deckBuild();
            deckShuffle();
            fieldPlay();
        }  //constructor supplement

        private void deckBuild()
        {
            string line;
            int count = 0;
            Deck = new Card[4][];
            Deck[0] = new Card[41]; //tier 1
            System.IO.StreamReader file = new System.IO.StreamReader(@"..\..\Resources\GreenTier.txt");
            while ((line = file.ReadLine()) != null)
            {
                Deck[0][count++] = new Card(line);
            }
            file.Close();
            count = 0;
            Deck[1] = new Card[31]; //tier 1
            file = new System.IO.StreamReader(@"..\..\Resources\YellowTier.txt");
            while ((line = file.ReadLine()) != null)
            {
                Deck[1][count++] = new Card(line);
            }
            file.Close();
            count = 0;
            Deck[2] = new Card[21]; //tier 1
            file = new System.IO.StreamReader(@"..\..\Resources\BlueTier.txt");
            while ((line = file.ReadLine()) != null)
            {
                Deck[2][count++] = new Card(line);
            }
            file.Close();
            count = 0;
            Deck[3] = new Card[11]; //tier 1
            file = new System.IO.StreamReader(@"..\..\Resources\Nobles.txt");
            while ((line = file.ReadLine()) != null)
            {
                Deck[3][count++] = new Card(line);
            }
            Deck[0][40] = Deck[1][30] = Deck[2][20] = new Card(2);
            Deck[3][10] = new Card(3);
            file.Close();
        }
        private void deckShuffle()
        {
            Random r = new Random();
            int hold, temp;
            shuf = new int[4][];
            shuf[0] = new int[40];
            shuf[1] = new int[30];
            shuf[2] = new int[20];
            shuf[3] = new int[10];
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < ((4 - i) * 10); j++)
                {
                    shuf[i][j] = j;
                }
                for (int j = 0; j < (((4 - i) * 10) - 1) ; j++)
                {
                    hold = r.Next(j+1, (((4-i)*10)-1));
                    temp = shuf[i][j];
                    shuf[i][j] = shuf[i][hold];
                    shuf[i][hold] = temp;
                }
            }
            
        }
        private void fieldPlay()
        {
            for(int i = 4; i >= 4 - playCt; i--)
            {
                field[3, i] = shuf[3][i];
            }
            field[2, 0] = 20;
            field[1, 0] = 30;
            field[0, 0] = 40;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 1; j < 5; j++)
                {
                    drawCard(i, j);
                }
            }
        }
        private void drawCard(int row, int space)
        {
            if (field[row, 0] > 0)
            {
                field[row, space] = shuf[row][--field[row, 0]];
            } else
            {
                field[row, space] = (4-row)*10;
            }
        }

        public void Move(Player them)
        {
            Console.WriteLine();
            bool turnDone = false;
            for (; ; ) // the move
            {
                Console.WriteLine("{0}, it's your turn!", them.name);
                string entry = Console.ReadLine();
                entry.ToLower();
                if(entry.Length > 3 && entry.Substring(0,3) == "buy")
                {
                    turnDone = getCard(entry.Substring(4), them);
                }
                else if (entry.Length > 5 && entry.Substring(0, 5) == "token")
                {
                    turnDone = tokenGet(entry.Substring(6), them);

                }
                else if (entry.Length > 7 && entry.Substring(0,7) == "reserve")
                {
                    turnDone = reserveCard(entry.Substring(8), them);
                }
                if (turnDone)
                {
                    break;
                }
                
            }


            for(; ;) // noble check, currently unused
            {
                break;
            }
        }
        public bool Round()
        {
            for (int i = 0; i < playCt; i++)
            {
                Move(players[i]);
            }
            return EndRound();
        }
        public bool EndRound()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("End of Round {0}", rounds++);
            for(int i = 0; i < playCt; i++)
            {
                Console.WriteLine("{0} has {1}", players[i].name, players[i].getTokens());
            }
            Console.WriteLine("Available tokens: {0}", getTokens());
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine();
            if (winner == null)
            {
                return false;
            }
            return true;
        }
        public void displayField()
        {
            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", Deck[3][field[3, 0]].lines[0], Deck[3][field[3, 1]].lines[0], Deck[3][field[3, 2]].lines[0], Deck[3][field[3, 3]].lines[0], Deck[3][field[3, 4]].lines[0]);
            for(int i = 2; i < 6; i++)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", Deck[3][field[3, 0]].lines[i], Deck[3][field[3, 1]].lines[i], Deck[3][field[3, 2]].lines[i], Deck[3][field[3, 3]].lines[i], Deck[3][field[3, 4]].lines[i]);
            }
            for(int i = 2; i >= 0; i--)
            {
                Console.WriteLine();
                for (int j = 0; j < 6; j++)
                {
                    if (j == 2)
                    {
                        Console.WriteLine("[\t/{0}\t]\t{1}\t{2}\t{3}\t{4}", field[i, 0], Deck[i][field[i, 1]].lines[j], Deck[i][field[i, 2]].lines[j], Deck[i][field[i, 3]].lines[j], Deck[i][field[i, 4]].lines[j]);

                    }
                    else
                    {
                        Console.WriteLine("[\t\t]\t{0}\t{1}\t{2}\t{3}", Deck[i][field[i, 1]].lines[j], Deck[i][field[i, 2]].lines[j], Deck[i][field[i, 3]].lines[j], Deck[i][field[i, 4]].lines[j]);
                    }
                }
            }
        }

        public int colorGrab(string str)
        {
            str.ToLower();
            if(str == "white"){
                return 0;
            }
            else if (str == "blue"){
                return 1;
            }
            else if (str == "green"){
                return 2;
            }
            else if (str == "red"){
                return 3;
            }
            else if (str == "black"){
                return 4;
            }
            else if (str == "gold")
            {
                return 5;
            } else
            {
                return 6;
            }
        }

        public bool tokenGet(string inp, Player them)
        {
            string[] temp = inp.Split(' ');
            if (temp.Length > 1 && temp[0] == temp[1])
            {
                int num = colorGrab(temp[0]);
                if (num > 4)
                {
                    Console.WriteLine("Invalid Token Color");
                    return false;
                }
                else if(tokens[num] < 4)
                {
                    Console.WriteLine("Cannot take two tokens from a stack of less than four");
                    return false;
                }

                addToken(num, them);
                addToken(num, them);
                Console.WriteLine("You grabbed two {0} tokens!", temp[0]);
                Console.WriteLine("You now have {0}", them.getTokens());
            } //2same
            else if(temp.Length <= 2)
            {
                Console.WriteLine("Invalid entry. Syntax must follow the form \"token {color} {color}\" or \"token {color} {color} {color}\"");
                return false;
            }
            else if(temp.Length > 2)
            {
                int[] tempy = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    tempy[i] = colorGrab(temp[i]);
                }
                if (tempy[0] > 4 || tempy[1] > 4 || tempy[2] > 4)
                {
                    Console.WriteLine("Invalid Token Color");
                    return false;
                } else if(tempy[0] == tempy[2] || tempy[1] == tempy[2])
                {
                    Console.WriteLine("Cannot take 2 and 1 tokens on the same turn");
                    return false;
                }
                else if(tokens[tempy[0]] == 0 || tokens[tempy[1]] == 0 || tokens[tempy[2]] == 0)
                {
                    Console.WriteLine("There are no more tokens of that color! Available tokens are: {0}", getTokens());
                    return false;
                }
                addToken(tempy[0], them);
                addToken(tempy[1], them);
                addToken(tempy[2], them);
                Console.WriteLine("You grabbed some {0}, {1}, and {2} tokens!", temp[0], temp[1], temp[2]);
                Console.WriteLine("You now have {0}", them.getTokens());
            } //3diff

            tokenMaxCheck(them);
            return true;
        }
        public bool getCard(string inp, Player them)
        {
            Console.WriteLine("You bought a \"{0}\" card!", inp);
            return true;
        }
        public bool reserveCard(string inp, Player them)
        {
            Console.WriteLine("You reserved a \"{0}\" card!", inp);
            return true;
        }
        public void tokenMaxCheck(Player them)
        {
            int sum = them.tokenSum();
            for (; ; )
            {
                if (sum<=10)
                {
                return;
                }
                string entry;
                int[] temp = new int[6];
                Console.WriteLine("You have more than 10 tokens. Please select a token to discard");
                entry = Console.ReadLine();
                entry.ToLower();
                string[] parse = entry.Split();
                string sel;
                if(parse[0] == "discard")
                {
                    sel = parse[1];
                }
                else
                {
                    sel = parse[0];
                }
                int col = colorGrab(sel);
                if(them.stuff[0,col] > 0)
                {
                    dropToken(col, them);
                    Console.WriteLine("You have discarded a {0} token. You now have {1} tokens", sel, sum-1);
                }
                else
                {
                    Console.WriteLine("You don't have any {0} tokens to discard!", sel);
                }
            }
        }

        private string getTokens()
        {
            string outie = $"{tokens[0]} white tokens, {tokens[1]} blue tokens, {tokens[2]} green tokens, {tokens[3]} red tokens, {tokens[4]} black tokens, and {tokens[5]} gold tokens";
            return outie;
        }
        private void addToken(int tokencol, Player them)
        {
            them.stuff[0, tokencol]++;
            tokens[tokencol]--;
            return;
        }
        private void dropToken(int tokencol, Player them)
        {
            them.stuff[0, tokencol]--;
            tokens[tokencol]++;
            return;
        }

        /*static Deck deckBlue;
        static Deck deckYellow;
        static Deck deckGreen;
        static Deck nobles;
        private static void PopDecks();*/
        public Card[][] Deck { get; private set; }
        public int[][] shuf { get; private set; }

        public int playCt;
        public Player[] players;
        public int[] tokens = new int[6];
        public int[,] field = new int[4, 5];
        public Player winner;
        public int rounds;
    }
}
