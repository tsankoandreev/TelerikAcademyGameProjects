using System;
using System.Collections.Generic;

class ConsoleGamePacMan
{
    static void Main()
    {
        // This size was choosen to fit in 1024x768 screen size
        Console.SetWindowSize(40, 24);
        Console.Clear();
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        GameField gameField = new GameField(25, 23);
        gameField.LoadLevel(1);
        gameField.DisplayField();
        gameField.DisplayGameData();

        PacMan pacMan = new PacMan(gameField);
        List<Monster> monsters = new List<Monster>();
        monsters.Add(new Monster(gameField, ConsoleColor.Red));
        monsters.Add(new Monster(gameField, ConsoleColor.Magenta));
        monsters.Add(new Monster(gameField, ConsoleColor.Yellow));
        monsters.Add(new Monster(gameField, ConsoleColor.Cyan));

        for (; ; )
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Escape:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        return;
                    case ConsoleKey.LeftArrow:
                        pacMan.MovingDirection = 'L';
                        break;
                    case ConsoleKey.RightArrow:
                        pacMan.MovingDirection = 'R';
                        break;
                    case ConsoleKey.UpArrow:
                        pacMan.MovingDirection = 'U';
                        break;
                    case ConsoleKey.DownArrow:
                        pacMan.MovingDirection = 'D';
                        break;
                }
            }
            pacMan.MovePacMan();
            foreach (Monster monster in monsters)
            {
                monster.MoveMonster();
                if (monster.IsMonsterOverPacMan(pacMan))
                {
                    if (gameField.TakeOneLive())
                    {
                        pacMan.ResetPacMan();
                        foreach (var monsterToReset in monsters)
                        {
                            monsterToReset.ResetMonster();
                        }
                        break;
                    }
                    else
                    {
                        //TODO GAME OVER
                    }
                }
            }
        }

    }
}
