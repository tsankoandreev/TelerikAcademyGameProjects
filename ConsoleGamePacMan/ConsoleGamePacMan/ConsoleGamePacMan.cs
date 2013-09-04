using System;
using System.Collections.Generic;

public class ConsoleGamePacMan
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    static void Main()
    {
        // This size was choosen to fit in 1024x768 screen size
        Console.SetWindowSize(40, 24);
        Console.Clear();
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        int level = 1;
        GameField gameField = new GameField(25, 23);
        gameField.LoadLevel(level);
        gameField.DisplayField();
        gameField.DisplayGameData();

        PacMan pacMan = new PacMan(gameField);
        List<Monster> monsters = new List<Monster>();
        monsters.Add(new Monster(gameField, ConsoleColor.Red));
        monsters.Add(new Monster(gameField, ConsoleColor.Red));
        monsters.Add(new Monster(gameField, ConsoleColor.Red));
        monsters.Add(new Monster(gameField, ConsoleColor.Red));

        GameState gameState = GameState.WaitingToStart;
        int counterDown = 4;

        gameField.DisplayWaitingToStart();

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
                        if (pacMan.MovingDirection == ' ')
                        {
                            pacMan.MovingDirection = 'L';
                        }
                        else
                        {
                            pacMan.NextMovingDirection = 'L';
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (pacMan.MovingDirection == ' ')
                        {
                            pacMan.MovingDirection = 'R';
                        }
                        else
                        {
                            pacMan.NextMovingDirection = 'R';
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (pacMan.MovingDirection == ' ')
                        {
                            pacMan.MovingDirection = 'U';
                        }
                        else
                        {
                            pacMan.NextMovingDirection = 'U';
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (pacMan.MovingDirection == ' ')
                        {
                            pacMan.MovingDirection = 'D';
                        }
                        else
                        {
                            pacMan.NextMovingDirection = 'D';
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        if (gameState == GameState.WaitingToStart)
                        {
                            counterDown = 4;
                            gameState = GameState.Starting;
                        }
                        break;
                }
            }
            if (gameState == GameState.WaitingToStart)
            {
                continue;
            }
            if (gameState == GameState.Starting)
            {
                gameField.DisplayStarting(ref counterDown, level);
                if (counterDown == 0)
                {
                    gameField.DisplayField();
                    gameField.DisplayGameData();
                    gameState = GameState.Playing;
                }
                continue;
            }
            if (gameState == GameState.GameOver)
            {
                continue;
            }
            if (gameState == GameState.Win)
            {
                continue;
            }
            pacMan.MovePacMan(gameField);
            foreach (Monster monster in monsters)
            {
                if (monster.IsMonsterOverPacMan(pacMan))
                {
                    if (gameField.TakeOneLive())
                    {
                        pacMan.ResetPacMan(gameField, true);
                        foreach (var monsterToReset in monsters)
                        {
                            monsterToReset.ResetMonster(gameField, true);
                        }
                        break;
                    }
                    else
                    {
                        gameField.DisplayGameOver();
                        gameState = GameState.GameOver;
                        continue;
                    }
                }
                monster.MoveMonster(gameField);
                if (monster.IsMonsterOverPacMan(pacMan))
                {
                    if (gameField.TakeOneLive())
                    {
                        pacMan.ResetPacMan(gameField, true);
                        foreach (var monsterToReset in monsters)
                        {
                            monsterToReset.ResetMonster(gameField, true);
                        }
                        break;
                    }
                    else
                    {
                        gameField.DisplayGameOver();
                        gameState = GameState.GameOver;
                        continue;
                    }
                }
            }
            if (gameField.EatCount <= 0)
            {
                level++;
                if (level > 2)
                {
                    gameField.DisplayWinner();
                    gameState = GameState.Win;
                    continue;
                }

                gameField.LoadLevel(level);
                gameField.DisplayField();
                gameField.DisplayGameData();

                pacMan = new PacMan(gameField);
                monsters.Clear();
                monsters.Add(new Monster(gameField, ConsoleColor.Red));
                monsters.Add(new Monster(gameField, ConsoleColor.Red));
                monsters.Add(new Monster(gameField, ConsoleColor.Red));
                monsters.Add(new Monster(gameField, ConsoleColor.Red));

                counterDown = 4;
                gameState = GameState.Starting;
            }
        }

    }

    enum GameState
    {
        WaitingToStart,
        Playing,
        Pause, //not implemented
        Starting,
        GameOver,
        Win
    }
}
