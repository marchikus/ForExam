using UnityEngine;
using UnityEngine.UI;

public class GoldTexter : MonoBehaviour
{
    private SaveData saveData;
    public static GoldTexter instance;
    public Text GoldText;
    private GameModel model;
    private int goldAmount;

    public void Initialize(GameModel gameModel)
    {
        if (gameModel == null)
        {
            return;
        }

        model = gameModel;
        model.OnGoldChanged += UpdateGoldText;

        if (GoldText != null)
        {
            GoldText.text = $"{GameModel.GoldCount}/{GameModel.MaxGold}";
        }
    }

    private void Start()
    {
        HaveSave();

        GoldText.text = $"{goldAmount}/{GameModel.MaxGold}";
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdateGoldText(GameModel model)
    {

        goldAmount = GameModel.GoldCount;

        if (GoldText != null)
        {
            GoldText.text = $"{goldAmount}/{GameModel.MaxGold}";
        }
    }

    public void HaveSave()
    {
        saveData = LoadSavedGameData();

        if (saveData != null)
        {
            goldAmount = saveData.collectedGold;
        }
        else
        {
            goldAmount = 0;
        }
    }

    private SaveData LoadSavedGameData()
    {
        return new Saver().LoadGame();
    }

    private void OnDestroy()
    {
        if (model != null)
        {
            model.OnGoldChanged -= UpdateGoldText;
        }
    }
}
