/* 
 * Main Program  Version 1.0  22/2/2015
 * Description: Main Program of wumpus game.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication3
{
    class Program 
    {
        static void Main(string[] args)
        {

            TextReader tr;       
            Room[] roomArray = new Room[10];   

            tr = new StreamReader("textData.txt");  // Text file inserted

            int dataLines = Convert.ToInt32(tr.ReadLine());
            for (int i = 0; i < dataLines; i++)
            {
                string line = tr.ReadLine();
                string[] aArray = new string[3];   
                aArray[0] = line.Substring(0, 2);
                aArray[1] = line.Substring(2, 8);
                aArray[2] = line.Substring(10);

                // trim whitespaces 
                aArray[0] = aArray[0].Trim();
                aArray[1] = aArray[1].Trim();
                aArray[2] = aArray[2].Trim();

                Room Object = new Room(aArray[0], aArray[1], aArray[2]);
                roomArray[i] = Object;

            }

            tr.Close();
           

            // rooms for wumpus, spiders and pit.
            Random random = new Random();
            var val = Enumerable.Range(2, 10).OrderBy(x => random.Next()).ToArray();
            string wumpusRoom = val[0].ToString();
            string pitRoom = val[1].ToString();
            string spiderRoom1 = val[2].ToString();
            string spiderRoom2 = val[3].ToString();

            int numberOfarrows = 3;       // No. of arrows in game specified.
            Room presentRoom = new Room();  // New object of class Room containing current room information.
            presentRoom = roomArray[0];

            // using infinite loop to play game
            while (true)      
            {
                // Welcome message at beginning of game.
                Console.WriteLine("**^^^***HUNT THE WUMPUS!!**^^^***\n");
                Console.WriteLine("You are in room {0}", presentRoom.RoomNumber);
                Console.WriteLine("{0} arrows left", numberOfarrows);
                Console.WriteLine(presentRoom.RoomDescription);
                Console.WriteLine("Tunnels to rooms {0}", presentRoom.AdjacentRooms);

                // Displaying hints
                DisplayingHints(wumpusRoom, pitRoom, spiderRoom1, spiderRoom2, presentRoom);


                do  
                {
                    Console.WriteLine("(M)ove or (S)hoot");  // Asks user to move or shoot in a room.
                    string userInput = Console.ReadLine();

                    if (userInput.Equals("m")) 
                    {
                        Console.WriteLine("Which room?");    
                        string moveUserToRoom = Console.ReadLine().Trim();
                        if (presentRoom.AdjacentRooms.Contains(moveUserToRoom))
                        {
                            if (moveUserToRoom.Equals(wumpusRoom))
                            {
                                Console.WriteLine("Game Over. You have been killed\n");
                                Console.ReadKey();
                                System.Environment.Exit(0);
                            }
                            else if (moveUserToRoom.Equals(pitRoom))
                            {
                                Console.WriteLine("Game Over. You fell in pit\n");
                                Console.ReadKey();
                                System.Environment.Exit(0);
                            }
                            else if (moveUserToRoom.Equals(spiderRoom1))
                            {
                                Console.WriteLine("Game Over. You have been stunk by spider\n");
                                Console.ReadKey();
                                System.Environment.Exit(0);
                            }
                            else if (moveUserToRoom.Equals(spiderRoom2))
                            {
                                Console.WriteLine("Game Over. You have been stunk by spider\n");
                                Console.ReadKey();
                                System.Environment.Exit(0);
                            }
                            else
                            {
                                // When user enters correct room.
                                Room newRoom = Array.Find(roomArray, e => e.RoomNumber.Equals(moveUserToRoom));
                                presentRoom = newRoom;
                                Console.WriteLine("You are in room {0}", presentRoom.RoomNumber);
                                Console.WriteLine("{0} arrows left", numberOfarrows);
                                Console.WriteLine(presentRoom.RoomDescription);

                                // Display hints
                                DisplayingHints(wumpusRoom, pitRoom, spiderRoom1, spiderRoom2, presentRoom);
                            }
                        }
                        else
                        {
                            // when user enters wrong room.
                            Console.WriteLine("Sorry! You cant get there from here");
                            Console.WriteLine("There are tunnels to rooms " + presentRoom.AdjacentRooms);
                        }

                    }
                    else if (userInput.Equals("s"))   // Shoot.
                    {
                        Console.WriteLine("Which room to go?");   // Asks user which room to enter.
                        string userShoots = Console.ReadLine().Trim();

                        if (presentRoom.AdjacentRooms.Contains(userShoots))
                        {
                            if ((userShoots.Equals(wumpusRoom)))
                            {
                                // user shot at wumpus room
                                Console.WriteLine("You Win..Wumpus killed.!");
                                Console.ReadKey();
                                System.Environment.Exit(0);
                            }
                            else
                            {
                                // arrows reduced
                                numberOfarrows--;  
                                if (numberOfarrows > 0)
                                {
                                    // User shot in wrong room.
                                    Console.WriteLine("Your arrow goes down the tunnel and is lost. You missed");
                                    Console.WriteLine("You in room {0}", presentRoom.RoomNumber);
                                    Console.WriteLine("{0} arrows left", numberOfarrows);
                                    Console.WriteLine(presentRoom.RoomDescription);
                                    Console.WriteLine("There are tunnels to rooms {0}", presentRoom.AdjacentRooms);
                                    Console.WriteLine("The tunnels are " + presentRoom.AdjacentRooms);

                                    // Displaying hints
                                    DisplayingHints(wumpusRoom, pitRoom, spiderRoom1, spiderRoom2, presentRoom);
                                }
                                else
                                {
                                    // Arrows finished
                                    Console.WriteLine("Your arrow goes down the tunnel and is lost. You missed");
                                    Console.WriteLine("No arrows left dimwit!!  Game Over  Press any key to exit.!");
                                    Console.ReadKey();
                                    System.Environment.Exit(0);

                                }
                            }

                        }

                        else
                        {
                            // When user shot somewhere else
                            Console.WriteLine("Sorry shooting in this dorection not allowed");
                            Console.WriteLine("You are in room {0}", presentRoom.RoomNumber);
                            Console.WriteLine("{0} arrows left", numberOfarrows);
                            Console.WriteLine(presentRoom.RoomDescription);
                            Console.WriteLine("There are tunnels to rooms {0}", presentRoom.AdjacentRooms);
                            Console.WriteLine("The tunnels are " + presentRoom.AdjacentRooms);

                            // Displaying hints
                            DisplayingHints(wumpusRoom, pitRoom, spiderRoom1, spiderRoom2, presentRoom);

                        }
                    }

                    else
                    {
                        // Wrong keyword entered
                        Console.WriteLine("Invalid Key pressed..Try Again.!");
                    }

                }

                while (true); // infinite loop
            }

        }

        // Display Hints Method
        private static void DisplayingHints(string wR, string pR, string sR1, string sR2, Room pRoom)
        {
            if (pRoom.AdjacentRooms.Contains(wR))
            {
                Console.WriteLine("Wumpus smell!");
            }
            if (pRoom.AdjacentRooms.Contains(pR))
            {
                Console.WriteLine("Dank odor smell");
            }
            if (pRoom.AdjacentRooms.Contains(sR1))
            {
                Console.WriteLine("Faint clicking noise");
            }
            if (pRoom.AdjacentRooms.Contains(sR2))
            {
                Console.WriteLine("Faint clicking noise");
            }
        }

    }

}
