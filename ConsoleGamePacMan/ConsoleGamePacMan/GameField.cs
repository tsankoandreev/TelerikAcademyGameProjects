using System;
using System.Collections.Generic;
using System.IO;

class GameField
{
    /// <summary>
    /// Structure to keep positions on the game field
    /// Used for PacMan and Monsters
    /// </summary>
    public struct FieldPoint
    {
        public int x;
        public int y;

        public FieldPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    // game field
    private char[,] fieldMatrix;
    // game data
    private int points = 0;
    private int lives = 5;
    private int level = 0;
    // symbols
    private char pacManSymbol = (char)3;
    private char monsterSymbol = (char)4;

    private int fieldWidth = 0;
    private int fieldHeight = 0;
    private List<FieldPoint> monstersStartPositions = new List<FieldPoint>();
    private FieldPoint pacManStartPosition = new FieldPoint();

    public GameField(int width, int height)
    {
        this.fieldWidth = width;
        this.fieldHeight = height;
        fieldMatrix = new char[width, height];
    }

    /// <summary>
    /// Load level from text file
    /// </summary>
    /// <param name="level">Level to load</param>
    public void LoadLevel(int level)
    {
        this.level = level;
        string levelFilename = string.Format(@"Levels\Level-{0}.txt", level);

        StreamReader levelFileSR = File.OpenText(levelFilename);
        while (!levelFileSR.EndOfStream)
        {
            for (int y = 0; y < fieldHeight; y++)
            {

                string line = levelFileSR.ReadLine();
                for (int x = 0; x < fieldWidth && x < line.Length; x++)
                {
                    fieldMatrix[x, y] = line[x];
                }
            }
        }
        levelFileSR.Close();

        // After load search field for start positions of PacMan and Monsters and clear them from field
        InitPacManStartPosition();
        InitMonstersStartPositions();
    }

    /// <summary>
    /// Search for PacMan symbol in the field and
    /// remember start position in pacManStartPosition variable
    /// </summary>
    private void InitPacManStartPosition()
    {
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //TODO search field for PacMan and set it in pacManStartPosition
        for (int y = 0; y < fieldHeight; y++)
        {
            for (int x   = 0; x < fieldWidth; x++)
            {
                if (fieldMatrix[x, y] == '☺')
                {
                    pacManStartPosition.x = x;
                    pacManStartPosition.y = y;
                }
            }
        }
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }

    /// <summary>
    /// Search for monster symbol in the field and
    /// remember available start position for new monsters in monstersStartPositions list
    /// </summary>
    private void InitMonstersStartPositions()
    {
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //TODO search field for monsters and their positions in monstersStartPositions and clear them from field
        for (int y = 0; y < fieldHeight; y++)
        {
            for (int x = 0; x < fieldWidth; x++)
            {
                if (fieldMatrix[x, y] == '☻')
                {
                    monstersStartPositions.Add(new FieldPoint(x, y));
                }
            }
        }
        monstersStartPositions.Clear();
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    }

    /// <summary>
    /// Get Random one of start positions for monster
    /// </summary>
    /// <returns>Random one of start positions of monster</returns>
    public FieldPoint GetNewMonsterStartPosition()
    {
        //TODO return random one of monstersStartPositions
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        Random randomGenerator = new Random();
        int randomMonster = randomGenerator.Next(0, monstersStartPositions.Count);

        return new FieldPoint(monstersStartPositions[randomMonster].x,monstersStartPositions[randomMonster].y);
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    }

    /// <summary>
    /// Used on start level or after die to get start position of PacMan
    /// </summary>
    /// <returns>Start position of PacMan</returns>
    public FieldPoint GetPacManStartPosition()
    {
        return pacManStartPosition;
    }

    /// <summary>
    /// Clear console and display field - used on start new game/level
    /// </summary>
    public void DisplayField()
    {
        Console.Clear();
        for (int y = 0; y < fieldHeight; y++)
        {
            for (int x = 0; x < fieldWidth; x++)
            {
                Console.Write(fieldMatrix[x, y]);
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Display Game Data - LIVES, POINTS, LEVEL
    /// </summary>
    public void DisplayGameData()
    {
        ConsoleColor defaultColor = Console.ForegroundColor;
        Console.SetCursorPosition(fieldWidth + 1, 1);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("LIVES");
        Console.SetCursorPosition(fieldWidth + 1, 2);
        Console.ForegroundColor = ConsoleColor.Green;
        for (int i = 0; i < lives; i++)
        {
            Console.Write(pacManSymbol + " ");
        }
        Console.SetCursorPosition(fieldWidth + 1, 4);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("POINTS");
        Console.SetCursorPosition(fieldWidth + 1, 5);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(points);
        Console.SetCursorPosition(fieldWidth + 1, 7);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("LEVEL");
        Console.SetCursorPosition(fieldWidth + 1, 8);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(level);
        Console.SetCursorPosition(fieldWidth + 1, 22);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("ESC for Exit");
        Console.ForegroundColor = defaultColor;
        Console.SetCursorPosition(0, fieldHeight);
    }

    /// <summary>
    /// Indexer to game field
    /// </summary>
    /// <param name="x">X position of indexed element</param>
    /// <param name="y">Y position of indexed element</param>
    /// <returns>Value in game field on position x, y</returns>
    public char this[int x, int y]
    {
        get
        {
            return this.fieldMatrix[x, y];
        }
        set
        {
            this.fieldMatrix[x, y] = value;
        }
    }

    /// <summary>
    /// Take one live
    /// </summary>
    /// <returns>true if there is lives, false if no live to take</returns>
    public bool TakeOneLive()
    {
        if (lives > 0)
        {
            lives--;
            return true;
        }
        return false;
    }
}
