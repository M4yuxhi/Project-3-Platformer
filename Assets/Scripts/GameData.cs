[System.Serializable]

public class GameData
{
    public int maxGoldCoinCount;
    public int lastLevelIndex;

    public GameData(int maxGoldCoinCount, int lastLevelIndex)
    {
        this.maxGoldCoinCount = maxGoldCoinCount;
        this.lastLevelIndex = lastLevelIndex;
    }
}
