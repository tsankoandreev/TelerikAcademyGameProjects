using System;

class Monster
{
    // Current Monster position, color
    private GameField.FieldPoint monsterPos;
    private ConsoleColor monsterColor = ConsoleColor.Red;
    private DateTime lastMoveDateTime = DateTime.Now;

    public Monster(GameField gameField, ConsoleColor color)
    {
        this.monsterColor = color;
        ResetMonster();
    }

    /// <summary>
    /// Reset monster to random start position for monster
    /// Used when first create of monster and when monster eat PacMan
    /// </summary>
    public void ResetMonster()
    {
        //TODO use GetNewMonsterStartPosition method in GameField
        //TODO set position in monsterPos
    }

    /// <summary>
    /// Move Monster
    /// </summary>
    public void MoveMonster()
    {
        // Do not move if time from last move is less than 150 milliseconds
        TimeSpan timeElapsedFromLastMove = DateTime.Now - lastMoveDateTime;
        if (timeElapsedFromLastMove.Milliseconds < 150)
        {
            return;
        }
        lastMoveDateTime = DateTime.Now;

        //in feature here will be more movement methods
        //for example BFS method to find fast way to player :)
        MoveRandom();
    }

    /// <summary>
    /// Check if monster over PacMan
    /// </summary>
    /// <param name="pacMan">PacMan object</param>
    /// <returns>true if monster is over PacMan, else returns false</returns>
    public bool IsMonsterOverPacMan(PacMan pacMan)
    {
        //TODO check if monster is over PacMan
        return false;
    }

    private void MoveRandom()
    {
        //TODO choose random direction: U,D,L,R
        //TODO check if Monster can move in selected direction if cannot choose again
        //TODO if can move 1. Erase previous position, 2. Change coordinates to new position, 3. Display Monster on new position
    }
}
