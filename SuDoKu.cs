using System;
using System.Collections.Generic;

namespace SuDoKu
{
    class SuDoKu
    {
        private static int[,] board2 = new int[9, 9]{
                { 5, 3, 0,   0, 7, 0,   0, 0, 0},
                { 6, 0, 0,   1, 9, 5,   0, 0, 0},
                { 0, 9, 8,   0, 0, 0,   0, 6, 0},

                { 8, 0, 0,   0, 6, 0,   0, 0, 3},
                { 4, 0, 0,   8, 0, 3,   0, 0, 1},
                { 7, 0, 0,   0, 2, 0,   0, 0, 6},

                { 0, 6, 0,   0, 0, 0,   2, 8, 0},
                { 0, 0, 0,   4, 1, 9,   0, 0, 5},
                { 0, 0, 0,   0, 8, 0,   0, 7, 9}
            };
        private static List<int>[,] board3 = new List<int>[9, 9];

        static void Main(string[] args)
        {
            //2D board


            //Input new board
            /*
            Console.WriteLine("Enter the sudoku board from left to right up to down (for empty enter 0): ");
            for(int r=0; r<9; r++)
            {
                Console.WriteLine("Line " + (r + 1) + " : ");
                for (int c = 0; c < 9; c++)
                {
                    Console.Write("\n" + (c + 1) + " : ");
                    board[r,c] = int.Parse(Console.ReadLine());
                }
            }
            */

            //Create 3D array for posibilities
            for (int i = 0; i < 9; i++)
                for (int k = 0; k < 9; k++)
                    board3[i, k] = new List<int>();
            for(int a=0; a<9; a++)
            {
                for(int b=0; b<9; b++)
                {
                    if (board2[a, b] != 0)
                        board3[a, b].Add(board2[a, b]);
                    else
                    {
                        board3[a, b] = GetPossibilities(a,b);
                        
                    }
                }
            }

            bool updated = false;

            do
            {
                updated = false;
                for (int i = 0; i < 9; i++)
                    for (int k = 0; k < 9; k++)
                    {
                        int length = board3[i, k].Count;
                        if (length > 1)
                        {
                            board3[i, k] = GetPossibilities(i, k);
                            if (length != board3[i, k].Count)
                                updated = true;
                        }
                    }
            } while (updated);

            Print3Dboard();
            
            Console.Read();
        }

        public static void Print3Dboard()
        {
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0)
                    Console.WriteLine();
                for (int k = 0; k < 9; k++)
                {
                    if (k % 3 == 0)
                        Console.Write("   ");
                    Console.Write(" {");
                    for (int n = 0; n < board3[i, k].Count; n++)
                        Console.Write(board3[i, k][n] + ",");
                    Console.WriteLine("}");
                }
            }
        }

        public static List<int> GetPossibilities(int r, int c)
        {
            List<int> possibilities = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int i = 0; i < 9; i++)
                if(i != r)
                    possibilities.Remove(board2[i, c]);
            for (int i = 0; i < 9; i++)
                if(i != c)
                    possibilities.Remove(board2[r, i]);

            for (int m = (r / 3) * 3; m < (r / 3) * 3 + 3; m++)
                for (int n = (c / 3) * 3; n < (c / 3) * 3 + 3; n++)
                    if (n != c && m != r)
                        possibilities.Remove(board2[m, n]);

            if (possibilities.Count == 1)
                board2[r, c] = possibilities[0];

            return possibilities;
        }
    }
}
