using System;

public class PacMan
{
    // Current PacMac position, color
    public GameField.FieldPoint pacManPos;
    private ConsoleColor pacManColor = ConsoleColor.Green;
    private DateTime lastMoveDateTime = DateTime.Now;

    public char MovingDirection { get; set; } //U - for moving up, D - down, L - left, R - right, space for not moving

    public char NextMovingDirection { get; set; } //U - for moving up, D - down, L - left, R - right, space for no next moving

    public PacMan(GameField gameField)
    {
        ResetPacMan(gameField, false);
    }

    /// <summary>
    /// Reset PacMan to start position for PacMan
    /// Used when first create of PacMan and when monster eat PacMan
    /// </summary>
    public void ResetPacMan(GameField gameField, bool eraseOldPacman)
    {
        if (eraseOldPacman)
        {
            Console.SetCursorPosition(pacManPos.x, pacManPos.y);
            Console.Write(' ');
        }
        MovingDirection = 'L'; // start moving direction will be left
        NextMovingDirection = ' ';
        pacManPos = gameField.GetPacManStartPosition();
    }

    /// <summary>
    /// Move PackMac
    /// </summary>
    public void MovePacMan(GameField gameField)
    {
        // Do not move if time from last move is less than 150 milliseconds
        TimeSpan timeElapsedFromLastMove = DateTime.Now - lastMoveDateTime;
        if (timeElapsedFromLastMove.TotalMilliseconds < 150)
        {
            return;
        }
        lastMoveDateTime = DateTime.Now;

        GameField.FieldPoint newPos = GetNextPosByMovingDirection(gameField, NextMovingDirection);
        if (NextMovingDirection != ' ' && gameField[newPos.x, newPos.y] != '#' && gameField[newPos.x, newPos.y] != '-')
        {
            MovingDirection = NextMovingDirection;
            NextMovingDirection = ' ';
        }
        if (MovingDirection != ' ')
        {
            newPos = GetNextPosByMovingDirection(gameField, MovingDirection);
            if (gameField[newPos.x, newPos.y] == '#' || gameField[newPos.x, newPos.y] == '-')
            {
                MovingDirection = NextMovingDirection;
                NextMovingDirection = ' ';
                if (MovingDirection != ' ')
                {
                    newPos = GetNextPosByMovingDirection(gameField, MovingDirection);
                    if (gameField[newPos.x, newPos.y] == '#' || gameField[newPos.x, newPos.y] == '-')
                    {
                        MovingDirection = ' ';
                    }
                }
            }
        }
        if (MovingDirection != ' ')
        {
            Console.SetCursorPosition(pacManPos.x, pacManPos.y);
            Console.Write(' ');

            pacManPos = newPos;
            gameField.EatAt(pacManPos);

            Console.SetCursorPosition(pacManPos.x, pacManPos.y);
            Console.ForegroundColor = this.pacManColor;
            Console.Write("☺");
        }
        //TODO check if PacMan can move in movingDirection
        //TODO if can move 1. Erase previous position, 2. Change coordinates to new position, 3. Display PacMan on new position
    }

    private GameField.FieldPoint GetNextPosByMovingDirection(GameField gameField, char movingDirection)
    {
        GameField.FieldPoint newPos = new GameField.FieldPoint(pacManPos.x, pacManPos.y);
        switch (movingDirection)
        {
            case 'U':
                newPos.y--;
                if (newPos.y < 0)
                {
                    newPos.y = gameField.GetHeight - 1;
                }
                break;
            case 'D':
                newPos.y++;
                if (newPos.y > gameField.GetHeight - 1)
                {
                    newPos.y = 0;
                }
                break;
            case 'L':
                newPos.x--;
                if (newPos.x < 0)
                {
                    newPos.x = gameField.GetWidth - 1;
                }
                break;
            case 'R':
                newPos.x++;
                if (newPos.x > gameField.GetWidth - 1)
                {
                    newPos.x = 0;
                }
                break;
        }
        return newPos;
    }
}
