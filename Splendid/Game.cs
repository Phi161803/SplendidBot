using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splendid
{
    class Game
    {
        public Game(string a)
        {
            playCt = 1;
            players = new Player[1] { new Player(a) };
            alwaysDo(7);
        } //debug only
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
            cmd();
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
                    hold = r.Next(j+1, (((4-i)*10)));
                    temp = shuf[i][j];
                    shuf[i][j] = shuf[i][hold];
                    shuf[i][hold] = temp;
                }
            }
            
        }
        private void fieldPlay()
        {
            for(int i = 4; i >= 0; i--)
            {
                if (i >= 4 - playCt)
                {
                    field[3, i] = shuf[3][i];
                }
                else
                {
                    field[3, i] = 10;
                }
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

        private void cmd()
        {
            Console.WriteLine("\"token [color] [color]\" or \"token [color] [color] [color]\" to take tokens");
            Console.WriteLine("\"buy [card]\" to buy a card");
            Console.WriteLine("\"reserve [card]\" to reserve a card and receive a gold token");
            Console.WriteLine("Cards follow the following format:");
            Console.WriteLine("A1 | A2 | A3 | A4");
            Console.WriteLine("B1 | B2 | B3 | B4");
            Console.WriteLine("C1 | C2 | C3 | C4");
            Console.WriteLine("\"field\" to display the field and available cards");
            Console.WriteLine("\"commands\" to display this list again");
        }
        private void showAll()
        {
            for (int i = 0; i < playCt; i++)
            {
                players[i].listAll(1);
            }
            Console.Write("Available tokens: ");
            getTokens();
        }

        public void Move(Player them)
        {
            Console.WriteLine();
            bool turnDone = false;
            for (; ; ) // the move
            {
                displayField();
                Console.WriteLine("{0}, it's your turn!", them.name);
                them.listAll();
                string entry = Console.ReadLine();
                entry = entry.ToLower();
                
                if (entry.Length > 3 && entry.Substring(0,3) == "buy")
                {
                    turnDone = buyCard(entry.Substring(4), them);
                }
                else if (entry.Length > 4 && entry.Substring(0, 5) == "take")
                {
                    turnDone = tokenGet(entry.Substring(5), them);
                }
                else if (entry.Length > 5 && entry.Substring(0, 5) == "token")
                {
                    turnDone = tokenGet(entry.Substring(6), them);
                }
                else if (entry.Length > 4 && entry.Substring(0, 5) == "field")
                {
                    displayField();
                    continue;
                }
                else if (entry.Length > 6 && entry.Substring(0, 7) == "command")
                {
                    cmd();
                    continue;
                }
                else if (entry.Length > 7 && entry.Substring(0,7) == "reserve")
                {
                    turnDone = reserveCard(entry.Substring(8), them);
                }
                else
                {
                    Console.WriteLine("I don't know what command that was");
                    continue;
                }
                if (turnDone)
                {
                    break;
                }
                
            }

            for(int i = 0; i < 5; i++)
            {
                bool nobleGet = true;
                for (int j = 0; j < 5; j++)
                {
                    if (Deck[3][field[3, i]].cost[j] > them.stuff[1, j])
                    {
                        nobleGet = false;
                    }
                }
                if (nobleGet)
                {
                    them.cardTake(Deck[3][field[3, i]]);
                    Console.WriteLine("You got this Noble:");
                    Deck[3][field[3, i]].readOut();
                    field[3, i] = 10;
                    break;
                }
            } // noble check
            if (them.prestige >= 15)
            {
                if(winner == null || winner.prestige < them.prestige)
                {
                    winner = them;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("{0} has enough points to win! Everyone else has until the end of this round to beat {1} Prestige!", winner.name, winner.prestige);
                    Console.ResetColor();
                }
            } //win check
        }
        public bool Round()
        {
            for (int i = 0; i < playCt; i++)
            {
                Move(players[i]);
            }
            Console.Clear();
            return EndRound();
        }
        public bool EndRound()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("End of Round {0}", rounds++);
            if (winner == null)
            {
                showAll();
                Console.WriteLine("\n---------------------------------------------------");
                Console.WriteLine();
                return false;
            }
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("{0} has won the game with {1} Prestige!", winner.name, winner.prestige);
            Console.ResetColor();
            return true;
        }
        public void displayField()
        {
            /*Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", Deck[3][field[3, 0]].lines[0], Deck[3][field[3, 1]].lines[0], Deck[3][field[3, 2]].lines[0], Deck[3][field[3, 3]].lines[0], Deck[3][field[3, 4]].lines[0]);
            for(int i = 2; i < 6; i++)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", Deck[3][field[3, 0]].lines[i], Deck[3][field[3, 1]].lines[i], Deck[3][field[3, 2]].lines[i], Deck[3][field[3, 3]].lines[i], Deck[3][field[3, 4]].lines[i]);
            }*/
            for(int i = 0; i < 6; i++)
            {
                if (i == 1) continue;
                for(int j = 0; j < 5; j++)
                {
                    Deck[3][field[3, j]].readLine(i);
                    if(j != 4)
                    {
                        Console.Write("       ");
                    }
                }
                Console.WriteLine();
            } // the nobles
            for(int i = 2; i >= 0; i--)
            {
                Console.Write("\n".PadRight(17));
                for(int j = 1; j < 5; j++)
                {
                    Console.Write(((char)('C'-i)).ToString().PadLeft(24));
                    Console.Write(j);
                }
                Console.WriteLine();
                //Console.WriteLine("\n\t\t\t\t\t{0}1\t\t\t{0}2\t\t\t{0}3\t\t\t{0}4", (char)('C'-i));
                for (int j = 0; j < 6; j++)
                {
                    if (j == 2)
                    {
                        string temp = $"/{field[i, 0]}";
                        Console.Write("[".PadRight(8) + temp.PadRight(8) + "]".PadRight(8));
                        //Console.WriteLine("[\t/{0}\t]\t\t{1}\t{2}\t{3}\t{4}", field[i, 0], Deck[i][field[i, 1]].lines[j], Deck[i][field[i, 2]].lines[j], Deck[i][field[i, 3]].lines[j], Deck[i][field[i, 4]].lines[j]);

                    }
                    else
                    {
                        Console.Write("[".PadRight(16) + "]".PadRight(8));
                        //Console.WriteLine("[\t\t]\t\t{0}\t{1}\t{2}\t{3}", Deck[i][field[i, 1]].lines[j], Deck[i][field[i, 2]].lines[j], Deck[i][field[i, 3]].lines[j], Deck[i][field[i, 4]].lines[j]);
                    }
                    for(int k = 1; k < 5; k++)
                    {
                        Console.Write("        ");
                        Deck[i][field[i, k]].readLine(j);
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.Write("Available tokens: ");
            getTokens();
            Console.WriteLine();
        }

        static public int colorGrab(string str)
        {
            str = str.ToLower();
            if(str == "white")
            {
                return 0;
            }
            else if (str == "blue")
            {
                return 1;
            }
            else if (str == "green")
            {
                return 2;
            }
            else if (str == "red" || str == "red ")
            {
                return 3;
            }
            else if (str == "black")
            {
                return 4;
            }
            else if (str == "gold")
            {
                return 5;
            }
            else
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
                    Console.Write("There are no more tokens of that color! Available tokens are: ");
                    getTokens();
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
        } //implemented
        public bool buyCard(string inp, Player them)
        {
            string set = inp.Substring(0, 1);
            int[] num = new int[2];
            num[1] = 5;
            num[0] = 'c' - set.ToCharArray()[0];
            int.TryParse(inp.Substring(1, 1), out num[1]);
            Card temp;
            if (num[1] > 0 && num[1] < 5)
            {
                if (num[0] >= 0 && num[0] <= 2 && field[num[0], num[1]] < ((4 - num[0])*10))
                {
                    temp = Deck[num[0]][field[num[0], num[1]]];
                }
                else if(inp.Substring(0, 1) == "r" && num[1] <= them.res)
                {
                    temp = them.reserved[num[1] - 1];
                }
                else
                {
                    Console.WriteLine("That's not a card!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("That's not a card!");
                return false;
            }
            // end selecting, begin buying
            //buyCheck(temp, them);

            int[] cardCost = them.cardCost(temp);
            if(them.stuff[0,5] < cardCost[5])
            {
                Console.WriteLine("You don't have enough tokens to buy that card!");
                return false;
            }
            Console.Write("You want to buy this card from ");
            if(num[0] >= 0)
            {
                Console.Write("the field:\n\n");
            }
            else
            {
                Console.Write("your reserves:\n\n");
            }
            temp.readOut();
            Console.WriteLine();
            Console.Write("This will cost you {0} tokens", cardCost.Sum());
            if (cardCost[5] > 0)
            {
                Console.Write(" including {0} gold token", cardCost[5]);
                if(cardCost[5] > 1)
                {
                    Console.Write("s");
                }
            }
            Console.Write(". Are you sure you want to buy this card? [Y/N]\n");
            string tempy = "n";
            tempy = Console.ReadLine().ToLower();
            if (tempy.Length == 0 || tempy[0] != 'y')
            {
                return false;
            }
            //end buying, begin take/draw
            them.cardTake(temp);
            for(int i = 0; i < 6; i++)
            {
                while (cardCost[i] > 0)
                {
                    dropToken(i, them);
                    cardCost[i]--;
                }
            }
            Console.WriteLine("You now have {0}", them.getTokens());
            Console.WriteLine("You now have {0}", them.getCards());
            if (num[0] >= 0)
            {
                drawCard(num[0], num[1]);
            }
            else
            {
                for (; num[1] < 3; num[1]++)
                {
                    them.reserved[num[1] - 1] = them.reserved[num[1]];
                }
                them.reserved[2] = new Card(2);
                them.res--;
            }
            return true;
        } //implemented
        public bool reserveCard(string inp, Player them)
        {
            if(them.res == 3)
            {
                Console.WriteLine("You can't reserve any more cards until you buy one you've already reserved!");
                return false;
            }
            Card temp;
            if (inp.Substring(1, 1) == "1" || inp.Substring(1, 1) == "2" || inp.Substring(1, 1) == "3" || inp.Substring(1, 1) == "4")
            {
                if (inp.Substring(0, 1) == "a")
                {
                    Console.WriteLine("You reserved this card!");
                    temp = Deck[2][field[2, int.Parse(inp.Substring(1, 1))]];
                    Deck[2][field[2, int.Parse(inp.Substring(1, 1))]].readOut();
                    field[2, int.Parse(inp.Substring(1, 1))] = shuf[2][--field[2,0]];
                }
                else if (inp.Substring(0, 1) == "b")
                {
                    Console.WriteLine("You reserved this card!");
                    temp = Deck[1][field[1, int.Parse(inp.Substring(1, 1))]];
                    Deck[1][field[1, int.Parse(inp.Substring(1, 1))]].readOut();
                    field[1, int.Parse(inp.Substring(1, 1))] = shuf[1][--field[1, 0]];
                }
                else if (inp.Substring(0, 1) == "c")
                {
                    Console.WriteLine("You reserved this card!");
                    temp = Deck[0][field[0, int.Parse(inp.Substring(1, 1))]];
                    Deck[0][field[0, int.Parse(inp.Substring(1, 1))]].readOut();
                    field[0, int.Parse(inp.Substring(1, 1))] = shuf[0][--field[0, 0]];
                }
                else
                {
                    Console.WriteLine("That's not a card!");
                    return false;
                }
            }
            else if(inp.Substring(1, 1) == "0")
            {
                if (inp.Substring(0, 1) == "a")
                {
                    Console.WriteLine("You reserved this card!");
                    temp = Deck[2][shuf[2][--field[2, 0]]];
                    Deck[2][shuf[2][field[2,0]]].readOut();
                }
                else if (inp.Substring(0, 1) == "b")
                {
                    Console.WriteLine("You reserved this card!");
                    temp = Deck[1][shuf[1][--field[1, 0]]];
                    Deck[1][shuf[1][field[1, 0]]].readOut();
                }
                else if (inp.Substring(0, 1) == "c")
                {
                    Console.WriteLine("You reserved this card!");
                    temp = Deck[0][shuf[0][--field[0, 0]]];
                    Deck[0][shuf[0][field[0, 0]]].readOut();
                }
                else
                {
                    Console.WriteLine("That's not a card!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("That's not a card!");
                return false;
            }
            Console.Write("You want to reserve this card from the field:\n\n");
            temp.readOut();
            Console.WriteLine();
            Console.Write("This will also get you one gold token. Is this what you want? [Y/N]");
            string tempy = "n";
            tempy = Console.ReadLine().ToLower();
            if (tempy.Length == 0 || tempy[0] != 'y')
            {
                return false;
            }
            them.reserved[them.res++] = temp;
            if (tokens[5] > 0)
            {
                addToken(5, them);
            }
            tokenMaxCheck(them);
            Console.WriteLine();
            Console.WriteLine("{0}'s reserved cards:", them.name);
            them.displayRes();
            Console.WriteLine();
            displayField();
            return true;
        } //implemented
        
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
                    Console.WriteLine("You have discarded a {0} token. You now have {1} tokens", sel, --sum);
                }
                else
                {
                    Console.WriteLine("You don't have any {0} tokens to discard!", sel);
                }
            }
        } //implemented
        public int buyCheck(Card cd, Player them)
        {
            int gold = 0;
            for (int i = 0; i < 5; i++)
            {
                gold += cd.cost[i] - (them.stuff[0, i] + them.stuff[1, i]);
            }
            return gold;
        } //defunct

        private void getTokens()
        {
            //string outie = $"{tokens[0]} white tokens, {tokens[1]} blue tokens, {tokens[2]} green tokens, {tokens[3]} red tokens, {tokens[4]} black tokens, and {tokens[5]} gold tokens";
            //return outie;
            Console.Write("{0} white tokens, ", tokens[0]);
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write("{0} blue tokens", tokens[1]);
            Console.ResetColor();
            Console.Write(", ");
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write("{0} green tokens", tokens[2]);
            Console.ResetColor();
            Console.Write(", ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write("{0} red tokens", tokens[3]);
            Console.ResetColor();
            Console.Write(", {0} black tokens, and ", tokens[4]);
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write("{0} gold tokens", tokens[5]);
            Console.ResetColor();
            Console.WriteLine();
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