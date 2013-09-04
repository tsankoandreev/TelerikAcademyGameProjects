using System;

public class Monster
{
    // Current Monster position, color
    private GameField.FieldPoint monsterPos;
    private ConsoleColor monsterColor = ConsoleColor.Red;
    private DateTime lastMoveDateTime = DateTime.Now;

    private char movingDirection = ' '; //U - for moving up, D - down, L - left, R - right

    public Monster(GameField gameField, ConsoleColor color)
    {
        this.monsterColor = color;
        ResetMonster(gameField, false);
    }

    /// <summary>
    /// Reset monster to random start position for monster
    /// Used when first create of monster and when monster eat PacMan
    /// </summary>
    public void ResetMonster(GameField gameField, bool eraseOldMonster)
    {
        if (eraseOldMonster)
        {
            EraseMonster(gameField);
        }
        monsterPos = gameField.GetNewMonsterStartPosition();
    }

    /// <summary>
    /// Move Monster
    /// </summary>
    public void MoveMonster(GameField gameField)
    {
        // Do not move if time from last move is less than 150 milliseconds
        TimeSpan timeElapsedFromLastMove = DateTime.Now - lastMoveDateTime;
        if (timeElapsedFromLastMove.TotalMilliseconds < 150)
        {
            return;
        }
        lastMoveDateTime = DateTime.Now;

        //in feature here will be more movement methods
        //for example BFS method to find fast way to player :)
        EraseMonster(gameField);
        MoveRandom(gameField);
        DisplayMonster();
    }

    /// <summary>
    /// Erase Monster
    /// </summary>
    public void EraseMonster(GameField gameField)
    {
        Console.SetCursorPosition(monsterPos.x, monsterPos.y);
        gameField.DisplayFieldAt(monsterPos.x, monsterPos.y);
    }

    /// <summary>
    /// Display Monster
    /// </summary>
    public void DisplayMonster()
    {
        Console.SetCursorPosition(monsterPos.x, monsterPos.y);
        Console.ForegroundColor = this.monsterColor;
        Console.Write("☻");
    }

    /// <summary>
    /// Check if monster over PacMan
    /// </summary>
    /// <param name="pacMan">PacMan object</param>
    /// <returns>true if monster is over PacMan, else returns false</returns>
    public bool IsMonsterOverPacMan(PacMan pacMan)
    {
        if (monsterPos.x == pacMan.pacManPos.x && monsterPos.y == pacMan.pacManPos.y)
        {
            return true;
        }
        return false;
    }

    private void MoveRandom(GameField gameField)
    {
        //TODO choose random direction: U,D,L,R
        //TODO check if Monster can move in selected direction if cannot choose again
        //TODO if can move 1. Erase previous position, 2. Change coordinates to new position, 3. Display Monster on new position
        if (!this.CanMove(gameField) || RandomWrapper.RandomClass.Next(101) > 90)
        {
            bool foundDirection = false;
            while (!foundDirection)
            {
                switch (RandomWrapper.RandomClass.Next(5))
                {
                    case 1: // move U
                        if (monsterPos.y > 0 && gameField[monsterPos.x, monsterPos.y - 1] != '#')
                        {
                            movingDirection = 'U';
                            foundDirection = true;
                        }
                        break;
                    case 2: // move D
                        if (monsterPos.y < gameField.GetHeight - 1 && gameField[monsterPos.x, monsterPos.y + 1] != '#')
                        {
                            movingDirection = 'D';
                            foundDirection = true;
                        }
                        break;
                    case 3: // move L
                        if (monsterPos.x > 0 && gameField[monsterPos.x - 1, monsterPos.y] != '#')
                        {
                            movingDirection = 'L';
                            foundDirection = true;
                        }
                        break;
                    case 4: // move R
                        if (monsterPos.x < gameField.GetWidth - 1 && gameField[monsterPos.x + 1, monsterPos.y] != '#')
                        {
                            movingDirection = 'R';
                            foundDirection = true;
                        }
                        break;
                }
            }
        }
        switch (movingDirection)
        {
            case 'U':
                monsterPos.y--;
                break;
            case 'D':
                monsterPos.y++;
                break;
            case 'L':
                monsterPos.x--;
                break;
            case 'R':
                monsterPos.x++;
                break;
        }
    }

    private bool CanMove(GameField gameField)
    {
        switch (movingDirection)
        {
            case 'U': // move U
                if (monsterPos.y > 0 && gameField[monsterPos.x, monsterPos.y - 1] != '#')
                {
                    return true;
                }
                break;
            case 'D': // move D
                if (monsterPos.y < gameField.GetHeight - 1 && gameField[monsterPos.x, monsterPos.y + 1] != '#')
                {
                    return true;
                }
                break;
            case 'L': // move L
                if (monsterPos.x > 0 && gameField[monsterPos.x - 1, monsterPos.y] != '#')
                {
                    return true;
                }
                break;
            case 'R': // move R
                if (monsterPos.x < gameField.GetWidth - 1 && gameField[monsterPos.x + 1, monsterPos.y] != '#')
                {
                    return true;
                }
                break;
        }
        return false;
    }

}
