using UnityEngine;
using UnityEngine.UI;

public class ScoreTexter : MonoBehaviour
{
    private SaveData saveData;
    public static ScoreTexter instance;
    public Text ScoreText;
    private GameModel model;
    private int scoreAmount;

    public void Initialize(GameModel gameModel)
    {
        if (gameModel == null)
        {
            return;
        }

        model = gameModel;
        model.OnScoreChanged += UpdateScoreText;

        if (ScoreText != null)
        {
            ScoreText.text = $"Score:{GameModel.ScoreCount}";
        }
    }

    private void Start()
    {
        HaveSave();

        ScoreText.text = $"Score:{scoreAmount}";
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdateScoreText(GameModel model)
    {
        Debug.Log("ֲûחמג");
        scoreAmount = GameModel.ScoreCount;

        if (ScoreText != null)
        {
            ScoreText.text = $"Score:{GameModel.ScoreCount}";
        }
    }

    public void HaveSave()
    {
        saveData = LoadSavedGameData();

        if (saveData != null)
        {
            scoreAmount = saveData.Score;
        }
        else
        {
            scoreAmount = 0;
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
            model.OnScoreChanged -= UpdateScoreText;
        }
    }
}
