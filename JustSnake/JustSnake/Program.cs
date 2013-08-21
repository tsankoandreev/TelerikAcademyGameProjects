﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustSnake
{
    struct Position
    {
        public int row;
        public int col;
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferHeight = Console.WindowHeight;
            Random randomNumbersGenerator = new Random();
            
            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;

            int lastFoodTime = 0;
            int foodDissapearTime = 10000;
            Position[] directions = new Position[]
            {
                new Position(0,1),//right
                new Position(0,-1),//left
                new Position(1,0),//down
                new Position(-1,0),//up
            };

            int sleepTime = 100;
            int direction = right;
            Position food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight), randomNumbersGenerator.Next(0, Console.WindowWidth));
            lastFoodTime = Environment.TickCount;
            Console.SetCursorPosition(food.col, food.row);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write('@');


            Queue<Position> snakeElements = new Queue<Position>();
            for (int i = 0; i < 5; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }
            foreach (Position position in snakeElements)
            {
                Console.SetCursorPosition(position.col, position.row);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write('*');
            }

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if(direction!=left)direction = right;
                    }
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        if (direction!=right) direction = left;
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if(direction!=up)direction = down;
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if(direction!=down)direction = up;
                    }
                }


                Position snakeHead = snakeElements.Last();
                Position nextDirection = directions[direction];
                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row, snakeHead.col + nextDirection.col);
                
                if (snakeNewHead.row<0||snakeNewHead.col<0||snakeNewHead.row>=Console.WindowHeight||snakeNewHead.col>=Console.WindowWidth||snakeElements.Contains(snakeNewHead))
                {
                    //Console.SetCursorPosition(((Console.BufferWidth / 2)-7), ((Console.BufferHeight) / 2)-2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("GAME OVER!");
                    //Console.SetCursorPosition(((Console.BufferWidth / 2)-10), ((Console.BufferHeight) / 2) - 1);
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine("Your points are: {0}",snakeElements.Count);
                    return;
                }
                //old head
                Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("*");
                //new head
                snakeElements.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (direction == right) Console.Write(">");
                if (direction == left) Console.Write("<");
                if (direction == up) Console.Write("^");
                if (direction == down) Console.Write("v");

                if (snakeNewHead.col == food.col && snakeNewHead.row == food.row)
                {
                    do
                    {
                        food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight), randomNumbersGenerator.Next(0, Console.WindowWidth));
                    
                    } while (snakeElements.Contains(food));
                    lastFoodTime = Environment.TickCount;
                    Console.SetCursorPosition(food.col, food.row);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write('@');
                    sleepTime--;
                }
                else
                {
                    Position last= snakeElements.Dequeue();
                    Console.SetCursorPosition(last.col, last.row);
                    Console.Write(" ");
                }

                if (Environment.TickCount-lastFoodTime>=foodDissapearTime)
                {

                    Console.SetCursorPosition(food.col, food.row);
                    Console.Write(" ");
                    do
                    {
                        food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight), randomNumbersGenerator.Next(0, Console.WindowWidth));
                    
                    } while (snakeElements.Contains(food));
                    lastFoodTime = Environment.TickCount;
                }

                //foreach (Position position in snakeElements)
                //{
                //    Console.SetCursorPosition(position.col, position.row);
                //    Console.Write('*');
                //}
                Console.SetCursorPosition(food.col, food.row);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write('@');
                Thread.Sleep(sleepTime);
            }
        }
    }
}
