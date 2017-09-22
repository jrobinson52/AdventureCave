using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class Program
    {
        static int boardSize = 10;
        static char[,] board = new char[boardSize, boardSize * 2];
        //static char[,] visible = new char[10, 10]; 

        static Random rand = new Random();
        static bool died = false;
        static int playerX = 0;
        static int playerY = 0;
        static int treasures = 0;

        /* Legend
         * M = monster
         * O = chest
         * X = wall
         */


        static void Main(string[] args)
        {
            /*      reads arrow key input
             *      
                if (Console.ReadKey().Key == ConsoleKey.UpArrow)
                    Console.Write("Up");            
            */


            fillBoard();

            while (treasures > 0 && !died) // game loop
            {
                drawBoard();

                Move();

                Baddies();
                if (board[playerY, playerX] == 'M')
                    died = true;

            }
            if (board[playerY, playerX] == 'M')
                Console.WriteLine("You Lose");
            else
                Console.WriteLine("You win");

        }

        static private void fillBoard()
        {
            for (int y = 0; y < boardSize; y++)
                for (int x = 0; x < boardSize * 2; x++) //*2 to make board square
                {
                    int R = rand.Next(100);
                    if (R < 4)
                        board[y, x] = 'M'; //places monster in place on board
                    else if (R < 8)
                    {
                        board[y, x] = 'O'; //creates chest
                        treasures++;
                    }
                    else if (R < 50)
                        board[y, x] = 'X'; //creates wall
                    else board[y, x] = ' '; //empty room

                }


        }

        static private void drawBoard()
        {
            Console.Clear();

            Console.Title = "Adventure";
            Console.WriteLine("Welcome to Adventure! Beware or some shit...");

            for (int y = 0; y < boardSize + 2; y++)
            {
                for (int x = 0; x < boardSize * 2; x++) //+2 for border
                {
                    if (y == 0 || y == boardSize + 1)
                        Console.Write("-");
                    else if (playerX == x && playerY == y - 1)
                        Console.Write('^');
                    else
                        Console.Write(board[y - 1, x]);

                }

                Console.WriteLine();

            }
            Console.Write($"Treasures: {treasures}");

        }

        private static void Move()
        {
            bool moved = false;
            while (!moved)
            {
                ConsoleKey k = Console.ReadKey().Key;
                switch (k)
                {
                    case ConsoleKey.UpArrow:
                        playerY--;
                        if (playerY < 0)
                            playerY = 0;
                        if (board[playerY, playerX] == 'X')
                        {
                            board[playerY, playerX] = ' ';
                            playerY++;
                        }
                        moved = true;
                        break;
                    case ConsoleKey.DownArrow:
                        playerY++;
                        if (playerY >= boardSize)
                            playerY = boardSize - 1;
                        if (board[playerY, playerX] == 'X')
                        {
                            board[playerY, playerX] = ' ';
                            playerY--;
                        }
                        moved = true;
                        break;
                    case ConsoleKey.LeftArrow:
                        playerX--;
                        if (playerX < 0)
                            playerX = 0;
                        if (board[playerY, playerX] == 'X')
                        {
                            board[playerY, playerX] = ' ';
                            playerX++;
                        }
                        moved = true;
                        break;
                    case ConsoleKey.RightArrow:
                        playerX++;
                        if (playerX >= boardSize * 2)
                            playerX = boardSize * 2 - 1;
                        if (board[playerY, playerX] == 'X')
                        {
                            board[playerY, playerX] = ' ';
                            playerX--;
                        }
                        moved = true;
                        break;


                }





            }


            if (board[playerY, playerX] == 'O') //capturing treasures
            {
                board[playerY, playerX] = ' ';
                treasures--;
            }
            else if (board[playerY, playerX] == 'M') //capturing treasures
                died = true;





        }

        private static void Baddies()
        {

            for (int y = 0; y < boardSize; y++)
                for (int x = 0; x < boardSize * 2; x++) //*2 to make board square
                    if (board[y, x] == 'M')
                    {
                        int count = 0;
                        while (board[y, x] == 'M' && count < 10)
                        {
                            int l = rand.Next(4);
                            switch (l)              //prevent walking through walls and chests
                            {
                                case 0: //up
                                    if (y > 0)
                                        if (board[y - 1, x] != 'X' && board[y - 1, x] != 'O')
                                        {
                                            board[y - 1, x] = 'N';
                                            board[y, x] = ' ';
                                        }
                                        else
                                            board[y, x] = 'M';
                                    break;

                                case 1: //down
                                    if (y < boardSize - 1)
                                        if (board[y + 1, x] != 'X' && board[y + 1, x] != 'O')
                                        {
                                            board[y + 1, x] = 'N';
                                            board[y, x] = ' ';
                                        }
                                        else
                                            board[y, x] = 'M';
                                    break;

                                case 2: //left
                                    if (x > 0)
                                        if (board[y, x - 1] != 'X' && board[y, x - 1] != 'O')
                                        {
                                            board[y, x - 1] = 'N';
                                            board[y, x] = ' ';
                                        }
                                        else
                                            board[y, x] = 'M';
                                    break;

                                case 3: //right
                                    if (x < boardSize * 2 - 1)
                                        if (board[y, x + 1] != 'X' && board[y, x + 1] != 'O')
                                        {
                                            board[y, x + 1] = 'N';
                                            board[y, x] = ' ';
                                        }
                                        else
                                            board[y, x] = 'M';
                                    break;

                            }
                            count++;
                        }
                    }

            for (int y = 0; y < boardSize; y++)
                for (int x = 0; x < boardSize * 2; x++) //*2 to make board square
                    if (board[y, x] == 'N')
                        board[y, x] = 'M'; //turns moved enemy back to M



        }








    }
}
