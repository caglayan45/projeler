using System;
using System.Threading;

namespace DiceGame
{
    public class Player
    {
        int pos,health;
        public Player()
        {
            this.pos = 0;
            this.health = 100;
        }

        public void ForwardPos(int pos)
        {
            if(pos < 0)
            {
                for (int i = pos; i < 0; i++)
                {
                    this.pos--;
                    Program.MovePlayer(this.pos);
                    Thread.Sleep(150);
                }
            }
            else
            {
                for (int i = 0; i < pos; i++)
                {
                    if (this.pos != 9)
                        this.pos++;
                    else
                        this.pos = 0;
                    Program.MovePlayer(this.pos);
                    Thread.Sleep(150);
                }
            }
        }

        public int GetPos()
        {
            return this.pos;
        }

        public int GetHealth()
        {
            return this.health;
        }

        public bool IsAlive()
        {
            return (this.health > 0) ? true : false;
        }

        public void ApplyDamage(int amount)
        {
            this.health -= amount;
        }

        public void Health(int amount)
        {
            this.health += amount;
        }
    }

    public class Program
    {
        enum TileTypes
        {
            empty,
            food,
            trap,
            goTo,
            goal
        }

        public static void MovePlayer(int pos)
        {
            Console.Clear();
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" ____\n");
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|                                                 ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("|\n");

            Console.ForegroundColor = ConsoleColor.White;
            switch (pos)
            {
                case 0:
                    Console.Write("|<-->                                             ");
                    break;
                case 1:
                    Console.Write("|     <-->                                        ");
                    break;
                case 2:
                    Console.Write("|          <-->                                   ");
                    break;
                case 3:
                    Console.Write("|               <-->                              ");
                    break;
                case 4:
                    Console.Write("|                    <-->                         ");
                    break;
                case 5:
                    Console.Write("|                         <-->                    ");
                    break;
                case 6:
                    Console.Write("|                              <-->               ");
                    break;
                case 7:
                    Console.Write("|                                   <-->          ");
                    break;
                case 8:
                    Console.Write("|                                        <-->     ");
                    break;
                case 9:
                    Console.Write("|                                             <-->");
                    break;
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("|\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|____");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" ____");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" ____|\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        static int RollDice()
        {
            Random rand = new Random();
            return rand.Next(6)+1;
        }

        static void Main(string[] args)
        {
            MovePlayer(0);
            int size = 10;
            ConsoleKeyInfo inputKey;
            TileTypes[] board = new TileTypes[size];
            Player p = new Player();
            Console.WriteLine("Current Helth : {0}", p.GetHealth());

            board[0] = TileTypes.empty;
            board[1] = TileTypes.goTo;//+2
            board[2] = TileTypes.trap;//-20
            board[3] = TileTypes.empty;
            board[4] = TileTypes.goTo;//starta
            board[5] = TileTypes.food;//+20
            board[6] = TileTypes.goTo;//-3
            board[7] = TileTypes.empty;
            board[8] = TileTypes.trap;//-10
            board[9] = TileTypes.goal;

            while (p.IsAlive())
            {
                do
                {
                    Console.WriteLine("\nPlease press Enter for roll.");
                    inputKey = Console.ReadKey();
                } while (inputKey.Key != ConsoleKey.Enter);

                int rolledDice = RollDice();
                p.ForwardPos(rolledDice);
                Console.WriteLine("Rolled : {0}", rolledDice);

                if (board[p.GetPos()] == TileTypes.goal)
                {
                    Console.WriteLine("----Player WINS----");
                    break;
                }
                else if (board[p.GetPos()] == TileTypes.goTo)
                {
                    if (p.GetPos() == 1)
                    {
                        Console.WriteLine("Forwarding +2");
                        Thread.Sleep(2000);
                        p.ForwardPos(2);
                        Console.WriteLine("Empty Tile.");
                    }
                    else if (p.GetPos() == 4)
                    {
                        Console.WriteLine("Forwarding to Start position");
                        Thread.Sleep(2000);
                        p.ForwardPos(-4);
                        Console.WriteLine("Empty Tile.");
                    }
                    else
                    {
                        Console.WriteLine("Forwarding -3");
                        Thread.Sleep(2000);
                        p.ForwardPos(-3);
                        Console.WriteLine("Empty Tile.");
                    }
                }
                else if (board[p.GetPos()] == TileTypes.trap)
                {
                    if (p.GetPos() == 2)
                    {
                        p.ApplyDamage(20);
                        Console.WriteLine("OPS! Trap, -20 damage.");
                        Console.WriteLine("Current Health : {0}", p.GetHealth());
                    }
                    else
                    {
                        p.ApplyDamage(10);
                        Console.WriteLine("OPS! Trap, -10 damage.");
                        Console.WriteLine("Current Health : {0}", p.GetHealth());
                    }
                }
                else if (board[p.GetPos()] == TileTypes.food)
                {
                    p.Health(20);
                    Console.WriteLine("Food, +20 health.");
                    Console.WriteLine("Current Health : {0}", p.GetHealth());
                }
                else if (board[p.GetPos()] == TileTypes.empty)
                {
                    Console.WriteLine("Empty Tile.");
                }
                
            }
            if (!p.IsAlive())
                Console.WriteLine("\n---Game Over, Player Lose.---");
            Console.ReadLine();
        }
    }
}
