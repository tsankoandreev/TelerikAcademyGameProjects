using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustCars
{
    struct Object
    {
        public int x;
        public int y;
        public char c1;
        public char c2;
        public ConsoleColor color;
    }
    class JustCars
    {
        static void PrintOnPossition(int x, int y, char c1, char c2, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write("{0}{1}", c1, c2);
        }
        static void PrintStringOnPossition(int x, int y, string str, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }
        static void Main()
        {

            int playField = 10;
            int livesCount = 5;
            Console.BufferHeight = Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth = 25;
            Object userCar = new Object();
            userCar.x = 2;
            userCar.y = Console.WindowHeight - 1;
            userCar.c1 = 'T';
            userCar.c2 = 'T';
            userCar.color = ConsoleColor.Yellow;
            Random randomGenerator = new Random();
            List<Object> objects = new List<Object>();
            
            while (true)
            {
                bool hitted = false;
                int chance = randomGenerator.Next(0, 100);
                if (chance < 10)
                {
                    Object newObject = new Object();
                    newObject.color = ConsoleColor.Magenta;
                    newObject.x = randomGenerator.Next(0, playField);
                    newObject.y = 0;
                    newObject.c1 = '[';
                    newObject.c2 = ']';
                    objects.Add(newObject);
                }
                else if(chance<70)
                {
                    Object newCar = new Object();
                    newCar.color = ConsoleColor.Green;
                    newCar.x = randomGenerator.Next(0, playField);
                    newCar.y = 0;
                    newCar.c1 = '(';
                    newCar.c2 = ')';
                    objects.Add(newCar);
                }
                else
                {
                        
                }


                //move our car
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                    //while (Console.KeyAvailable)
                    //{
                    //    Console.ReadKey(true);
                    //}
                    if (pressedKey.Key == ConsoleKey.LeftArrow)
                    {
                        if (userCar.x - 1 >= 0)
                        {
                            userCar.x -= 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.RightArrow)
                    {
                        if (userCar.x + 1 < playField)
                        {
                            userCar.x += 1;
                        }
                    }
                }
                //move cars
                List<Object> newList = new List<Object>();
                for (int i = 0; i < objects.Count; i++)
                {
                    Object oldObject = objects[i];
                    Object newObject = new Object();
                    newObject.x = oldObject.x;
                    newObject.y = oldObject.y + 1;
                    newObject.c1 = oldObject.c1;
                    newObject.c2 = oldObject.c2;
                    newObject.color = oldObject.color;
                    if ((newObject.c1=='('&& newObject.c2==')') && newObject.y == userCar.y && (newObject.x == userCar.x || newObject.x + 1 == userCar.x + 1 || newObject.x + 1 == userCar.x || newObject.x == userCar.x + 1))
                    {
                        livesCount--;
                        hitted = true;
                        if (livesCount <= 0)
                        {
                            PrintStringOnPossition(8, 9, "GAME OVER", ConsoleColor.Red);
                            PrintStringOnPossition(8, 13, "Press any key to continue...", ConsoleColor.Red);
                            Console.ReadLine();
                            return;
                        }
                    }
                    else if ((newObject.c1 == '[' && newObject.c2 == ']') && newObject.y == userCar.y && (newObject.x == userCar.x || newObject.x + 1 == userCar.x + 1 || newObject.x + 1 == userCar.x || newObject.x == userCar.x + 1))
                    {
                        livesCount++;
                    }
                    if (newObject.y < Console.WindowHeight)
                    {
                        newList.Add(newObject);
                    }

                }
                objects = newList;
                //check hit
                //clear console
                Console.Clear();
                //redraw playfield

                if (hitted)
                {
                    PrintOnPossition(userCar.x, userCar.y, 'X', 'X', ConsoleColor.Red);
                    objects.Clear();
                }
                else
                {
                    PrintOnPossition(userCar.x, userCar.y, userCar.c1, userCar.c2, userCar.color);
                }
                foreach (Object car in objects)
                {
                    PrintOnPossition(car.x, car.y, car.c1, car.c2, car.color);
                }
                //draw info
                PrintStringOnPossition(8, 4, "Lives: " + livesCount, ConsoleColor.White);
                //slow down program
                Thread.Sleep(500);
            }
        }
    }
}
