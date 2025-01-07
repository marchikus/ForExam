using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int shovels;
    public int collectedGold;
    public int Score;
    public int ShovelsText;
    public List<CellData> cells;
}

[System.Serializable]

public class CellData
{
    public int depth;
    public bool gold;

    public CellData(int depth)
    {
        this.depth = depth;
        this.gold = false;
    }
}
