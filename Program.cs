using System;
using System.Collections.Generic;

namespace BlackJack
{
    class Program
    {
        static User Dealer = new User();
        static User Player = new User();
        static void Main(string[] args)
        {
            
            Console.WriteLine("Welcome to Blackjack!");
            Console.WriteLine("Dealer stands on 17!");
            Console.WriteLine();

            var playing = true;
            while (playing)
            {

            Reset();

            CreateDeck(Player.Deck);
            CreateDeck(Dealer.Deck);

            Console.WriteLine("Dealer Draws");
            GetDealerCard();
            Console.WriteLine("?");
            Console.WriteLine();
            Console.WriteLine("Player Draws:");
            GetPlayerCard();
            GetPlayerCard();
            Console.WriteLine("Player Count: " + Player.CardCount);

             var PlayerPlaying = true;

                if (Player.CardCount == 21)
                {
                    Console.WriteLine("Black Jack!");
                    PlayerPlaying = false;
                }
                
                while (PlayerPlaying)
                {
                    Console.WriteLine("Hit or Stand?");
                    string userinput = Console.ReadLine();
                    userinput = userinput.ToLower();
                    
                    if (userinput == "hit")
                    {
                        GetPlayerCard();
                        if (Player.CardCount > 21)
                        {
                            Console.WriteLine("Bust!");
                            PlayerPlaying = false;
                        Player.Bust = true;
                        }
                    Console.WriteLine("Player Count is at: " + Player.CardCount);
                }
                    else if (userinput == "stand")
                    {
                        PlayerPlaying = false;
                    }
                    else if (userinput == "deck")
                        {
                            for (int i = 0; i < Player.Deck.Length; i++)
                            {
                                 Console.WriteLine(Player.Deck[i]);
                            }
                        
                   
                        }
                
                    else
                        {
                            Console.WriteLine("Enter a Valid Input!");
                        }
                }

            ShowFace(Dealer.Deck[0]);   
            GetDealerCard();
            Console.WriteLine("Dealer Count: " + Dealer.CardCount);


            var DealerPlaying = true;
            if (Dealer.CardCount > 16)
                {
                    DealerPlaying = false;
                }
            else if (Player.Bust)
            {
                DealerPlaying = false;
                
            }

            while (DealerPlaying)
            {
                if (Dealer.CardCount <= 16)
                {
                    GetDealerCard();
                    Console.WriteLine("Dealer Count: " + Dealer.CardCount);
                    if (Dealer.CardCount > 21)
                    {
                        Console.WriteLine("Dealer Bust");
                        DealerPlaying = false;
                        Dealer.Bust = true;
                          
                    }
                }
                else if (Dealer.CardCount >= 17)
                {
                    DealerPlaying = false;
                }
                
            }

            if (Dealer.Bust)
                {
                    Console.WriteLine("Player Wins!");
                    Player.WinCount++;
                }
            else if (Player.Bust)
                {
                    Console.WriteLine("House Wins!");
                    Dealer.WinCount++;
                }
            else
                {
                    CheckWin();
                }



            Console.WriteLine("Play another round y?");

            if (Console.ReadLine() != "y")
            {
                playing = false;
            }
        }
            Console.WriteLine();
            Console.WriteLine("Player Wins: " + Player.WinCount);
            Console.WriteLine("House Wins: " + Dealer.WinCount);
            Console.WriteLine("Thanks for Playing!");

        }

        static void CreateDeck(int[] arr) // creates a "deck" of 10 cards between the values of 2 - 14
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = RandomNumber(2, 15);
            }
        }
        public static void Reset() //reset values before a game
        {
            Dealer.CardCount = 0;
            Dealer.CardOrder = 0;
            Dealer.Bust = false;
            Player.CardCount = 0;
            Player.CardOrder = 0;
            Player.Bust = false;

        }
        public static void CheckWin() 
        {
            if (Player.CardCount > Dealer.CardCount)
            {
                Console.WriteLine("Player Wins!");
                Player.WinCount++;
            }
            else if (Player.CardCount < Dealer.CardCount)
            {
                Console.WriteLine("House Wins!");
                Dealer.WinCount++;
            }
            else if (Player.CardCount == Dealer.CardCount)
            {
                Console.WriteLine("Push!");
                Player.WinCount++;
                Dealer.WinCount++;
            }
        }
        
        public static void ShowFace(int x) //shows a face card
        {
            switch (x)
            {
                case 11:
                    Console.WriteLine("J");
                    break;
                case 12:
                    Console.WriteLine("Q");
                    break;
                case 13:
                    Console.WriteLine("K");
                    break;
                case 14:
                    Console.WriteLine("A");
                    break;
                case 1:
                    Console.WriteLine("A");
                    break;
                default:
                    Console.WriteLine(x);
                    break;

            }          
        }

        public static int ConvertTen(int x) //used in GetCard to loop through the array to add the sum
        {
            switch (x)
            {
                case 11:
                    return 10;
                case 12:
                    return 10;
                case 13:
                    return 10;

            }
            return x;
        }
        public static void GetDealerCard() //Dealer Draws a Card
        {

            //all face cards have a value of 10, if not a face card add the normal value
            if (Dealer.Deck[Dealer.CardOrder] == 10 || Dealer.Deck[Dealer.CardOrder] == 11 || Dealer.Deck[Dealer.CardOrder] == 12 || Dealer.Deck[Dealer.CardOrder] == 13)
            {
                Dealer.CardCount += 10;
            }
            else if (Dealer.Deck[Dealer.CardOrder] == 14) // if the card is ACE add 11
            {
                Dealer.CardCount += 11;
            }
            else
            {
                Dealer.CardCount += Dealer.Deck[Dealer.CardOrder]; 
            }

            if (Dealer.CardCount > 21) //If you Have Ace(s) and bust convert to 1 values
            {
                for (int x = 0; x <= Dealer.CardOrder; x++) //convert 14 (Ace) to 1
                {

                    if (Dealer.Deck[x] == 14)
                    {
                        Dealer.Deck[x] = 1;
                    }
                }
                Dealer.CardCount = 0;
                for (int x = 0; x <= Dealer.CardOrder; x++) //loop through the cards and add the total
                {
                    Dealer.CardCount += ConvertTen(Dealer.Deck[x]);
                }
            }

            ShowFace(Dealer.Deck[Dealer.CardOrder]); //checking for a Face card to display it to the user

            Dealer.CardOrder++;
        }
        public static void GetPlayerCard() //Player Draws a card
        {
            

            //all face cards have a value of 10, if not a face card add the normal value
            if (Player.Deck[Player.CardOrder] == 10 || Player.Deck[Player.CardOrder] == 11 || Player.Deck[Player.CardOrder] == 12 || Player.Deck[Player.CardOrder] == 13)
            {
                Player.CardCount += 10;
            }
            else if (Player.Deck[Player.CardOrder] == 14) // if the card is ACE add 11
            {
             Player.CardCount += 11;
                
            }
            else
            {
                Player.CardCount += Player.Deck[Player.CardOrder];
            }
            if (Player.CardCount > 21) //If you Have Ace(s) and bust convert to 1 values
            {
                for (int x = 0; x <= Player.CardOrder; x++) //convert 14 (Ace) to 1
                {

                    if (Player.Deck[x] == 14)
                    {
                        Player.Deck[x] = 1;
                    }
                }
                Player.CardCount = 0;
                for (int x = 0; x <= Player.CardOrder; x++) //loop through the cards and add the total
                {
                    Player.CardCount += ConvertTen(Player.Deck[x]);
                }

            }

            ShowFace(Player.Deck[Player.CardOrder]); //display a face or number card to the user
            

            Player.CardOrder++;

            
        }



        class User
        {
            public int[] Deck = new int[10]; //10 card deck
            public int CardOrder = 0;     //Is a count of what card you will draw
            public int CardCount = 0;     //Total value of your hand, must not exceed 21
            public int WinCount = 0;        //Amount of wins this "user" has
            public bool Bust = false;   //Player is not bust till they are

        }
        static public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

    }
}
