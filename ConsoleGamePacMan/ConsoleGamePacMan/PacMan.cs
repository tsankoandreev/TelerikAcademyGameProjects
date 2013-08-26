using System;

class PacMan
{
    // Current PacMac position, color
    private GameField.FieldPoint pacManPos;
    private ConsoleColor pacManColor = ConsoleColor.Green;
    private DateTime lastMoveDateTime = DateTime.Now;

    public char MovingDirection { get; set; } //U - for moving up, D - down, L - left, R - right

    public PacMan(GameField gameField)
    {
        ResetPacMan();
    }

    /// <summary>
    /// Reset PacMan to start position for PacMan
    /// Used when first create of PacMan and when monster eat PacMan
    /// </summary>
    public void ResetPacMan()
    {
        MovingDirection = 'L'; // start moving direction will be left
        //TODO get start position for PacMan using method GetPacManStartPosition of GameField
    }

    /// <summary>
    /// Move PackMac
    /// </summary>
    public void MovePacMan()
    {
        // Do not move if time from last move is less than 150 milliseconds
        TimeSpan timeElapsedFromLastMove = DateTime.Now - lastMoveDateTime;
        if (timeElapsedFromLastMove.Milliseconds < 150)
        {
            return;
        }
        lastMoveDateTime = DateTime.Now;

        //TODO check if PacMan can move in movingDirection
        //TODO if can move 1. Erase previous position, 2. Change coordinates to new position, 3. Display PacMan on new position
    }
}
