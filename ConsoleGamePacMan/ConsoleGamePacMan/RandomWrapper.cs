using System;

public class RandomWrapper
{
    private static Random random = null;

    public static Random RandomClass
    {
        get
        {
            if (random == null)
            {
                random = new Random(DateTime.Now.Millisecond);
            }
            return random;
        }
    }
}
