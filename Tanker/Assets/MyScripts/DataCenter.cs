using UnityEngine;

public class DataCenter
{
    public static int score = 0;
    public static int playerLife = 3;
    public static int level = 1;
    public static int playerLifeMax = 3;
    
    public static void ResetData()
    {
        DataCenter.score = 0;
        DataCenter.playerLife = DataCenter.playerLifeMax;
        DataCenter.level = 1;
    }
}
