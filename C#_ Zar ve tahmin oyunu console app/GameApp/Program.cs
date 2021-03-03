using System;

namespace GameApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int credit = 500;

            Console.WriteLine($"Hi there,\nWelcome to the Elf Inn Casino!\nYou have {credit} credits.\nType\n\tD to play dice game,\n\tG to play guess game,\n\tE to exit.");

            string answer = "";
            do
            {
                Console.WriteLine("\nType : ");
                answer = Console.ReadLine();
            } while (answer != "D" && answer != "G" && answer != "E");

            switch (answer)
            {
                case "G":
                    goto guessgame;
                case "D":
                    goto dicegame;
                case "E":
                    goto finish;
            }

        //LOBBY
        lobby:
            Console.WriteLine($"Going back to lobby...\n\nHi there,\nYou have {credit} credits\nType\n\tD to play dice game,\n\tG to play guess game,\n\tE to exit.");
            
            do
            {
                Console.WriteLine("\nType : ");
                answer = Console.ReadLine();
            } while (answer != "D" && answer != "G" && answer != "E");

            switch (answer)
            {
                case "D":
                    goto dicegame;
                case "G":
                    goto guessgame;
                case "E":
                    goto finish;
            }

        // GUESS GAME INTRODUCTION
        guessgame:
            Console.WriteLine("Hello.\n");
        up:
            Console.WriteLine("S:Start, L:Lobby, I:Game Info");
            do
            {
                Console.WriteLine("\nType : ");
                answer = Console.ReadLine();
            } while (answer != "S" && answer != "L" && answer != "I");

            switch (answer)
            {
                case "S":
                    goto guessgamestart;
                case "L":
                    goto lobby;
                case "I":
                    Console.WriteLine("\nIn this game house will pick a random number between 0 and X.\nYou will give a guess in that range and place your bet.\nIf you guess it right you will win * ******credits prize.\nOtherwise the house will tell you if the number is higher or lower than your\nguess and you will be able to place your bet again with that information.\n");
                    goto up;
            }

        // GUESS GAME START
        guessgamestart:
            Random rand = new Random();
            int winNumber = rand.Next(201)*5, guessBet = 0, guessNumber = 0, minBet = 10;
            string guess = "";
            Console.WriteLine("House picked a number between 0 and 1000\nWin prize is 1000 credits\n");
            do
            {
                Console.WriteLine("Give a guess or type E to end or type L to lobby : ");
                guess = Console.ReadLine();
                if(Int32.TryParse(guess, out guessNumber))
                {
                    Console.WriteLine($"Your guess is {guessNumber}\n");
                    if (credit <= 0)
                    {
                        Console.WriteLine("Credit Finish.");
                        goto finish;
                    }
                    do
                    {
                        do
                        {
                            Console.WriteLine($"(min bet:{minBet}, balance:{credit})\nPlace your bet:");
                            guess = Console.ReadLine();
                        } while (!Int32.TryParse(guess, out guessBet));
                    } while (guessBet < minBet);
                    

                    if(credit <= guessBet)
                    {
                        do
                        {
                            Console.WriteLine("Not enough credits or All credits, do you want to ALL IN? (Y/N)");
                            answer = Console.ReadLine();
                        } while (answer != "Y" && answer != "N");
                        switch (answer)
                        {
                            case "Y":
                                guessBet = credit;
                                break;
                            case "N":
                                goto lobby;
                        }
                    }

                    credit -= guessBet;
                    minBet += 10;

                    if (winNumber < guessNumber)
                        Console.WriteLine("LO");
                    else if (winNumber > guessNumber)
                        Console.WriteLine("HI");
                    else
                    {
                        credit += 1000;
                        Console.WriteLine("*** Correct Guess ***\nYou win 1000 credits!\nCongrats!\n\nAnother round ?\nS : Start, L: Lobby, I: Game Info");
                    }
                    if (credit <= 0)
                    {
                        Console.WriteLine("Credit Finish.");
                        goto finish;
                    }
                }
            } while (guess != "E" && guess != "L");
            if (guess == "E")
                goto finish;
            else if (guess == "L")
                goto lobby;

        //DICE GAME
        dicegame:
            int bet = 0, i = 0;
            int[,] zarlar = new int[2, 2];
            Random rand2 = new Random();
            Console.WriteLine("Hello,\nIn the dice game the house and you roll 2 dice in a round.\nWho is going to roll first will change after every round.\n\nTo place a bet type the number or type L to go back to thelobby: ");
            do
            {
        up1:
                if (i != 0)
                    Console.WriteLine("Another round?\nTo place a bet type the number or type L to go back to the lobby: ");
                Console.WriteLine("\nType : ");
                answer = Console.ReadLine();
                if (Int32.TryParse(answer, out bet))
                {
                    if (bet > credit)
                    {
                        if (credit == 0)
                        {
                            Console.WriteLine("Credit Finish.");
                            goto finish;
                        }
                        Console.WriteLine("Insufficient credits, try another");
                        goto up1;
                    }
                    credit -= bet;
                    zarlar[0, 0] = rand2.Next(1, 7);
                    zarlar[0, 1] = rand2.Next(1, 7);
                    zarlar[1, 0] = rand2.Next(1, 7);
                    zarlar[1, 1] = rand2.Next(1, 7);
                    Console.WriteLine($"Bets closed\nHouse rolled {zarlar[0, 0]} , {zarlar[0, 1]}");
                    Console.WriteLine($"You rolled {zarlar[1, 0]} , {zarlar[1, 1]}");
                    if(zarlar[0, 0] + zarlar[0, 1] > zarlar[1, 0] + zarlar[1, 1])
                    {
                        Console.WriteLine("--- House Wins ---");
                    }else if(zarlar[0, 0] + zarlar[0, 1] < zarlar[1, 0] + zarlar[1, 1])
                    {
                        Console.WriteLine("*** You Win ***");
                        credit += bet * 2;
                    }
                    else
                    {
                        Console.WriteLine("___ Tied Game ___");
                        credit += bet;
                    }
                    Console.WriteLine($"You have {credit} credits.\n");
                    i = 1;
                }
            } while (answer != "L");
            goto lobby;

        finish:
            Console.WriteLine("Exiting . . .");
        }
    }
}
