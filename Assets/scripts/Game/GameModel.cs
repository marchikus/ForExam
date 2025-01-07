using UnityEngine;

public class GameModel
{
    private SaveData savedData;
    public static int Shovel = MaxShovel;
    public static int GoldCount = 0;
    public int GoldCost = 50;
    public int ShovelCost = 100;
    public static int ScoreCount = 0;

    public const int MaxShovel = 10;
    public const int MaxGold = 4;

    public event System.Action<GameModel> OnShovelChanged;
    public event System.Action<GameModel> OnGoldChanged;
    public event System.Action<GameModel> OnScoreChanged;

    public void UseShovel()
    {
        if (Shovel > 0)
        {
            Shovel--;
            OnShovelChanged?.Invoke(this);
        }
    }

    public void AddGold()
    {
        if (GoldCount < MaxGold)
        {
            GoldCount++;
            OnGoldChanged?.Invoke(this);
            Debug.Log("Gold added. New GoldCount: " + GoldCount);
        }
    }

    public void AddScore()
    {
        ScoreCount += GoldCost;
        Debug.Log($"+{GoldCount}, Score = {ScoreCount}");
        if (GoldCount == MaxGold)
        {
            ScoreCount += (ShovelCost * Shovel);
            Debug.Log($"Score = {ScoreCount}");
        }
        OnScoreChanged?.Invoke(this);
    }

    public static void ResetValues()
    {
        Shovel = MaxShovel;
        GoldCount = 0;
        ScoreCount = 0;
    }

    public void HaveSave()
    {
        savedData = LoadSavedGameData();

        if (savedData != null)
        {
            Shovel = savedData.shovels;
            GoldCount = savedData.collectedGold;
            ScoreCount = savedData.Score;
        }

        OnShovelChanged?.Invoke(this); // Уведомляем подписчиков о восстановленных значениях
        OnGoldChanged?.Invoke(this);
        OnScoreChanged?.Invoke(this);
    }

    private SaveData LoadSavedGameData()
    {
        return new Saver().LoadGame();
    }

    public bool GetShovelCount() => Shovel == 0;
    public bool GetGoldCount() => GoldCount == MaxGold;
}