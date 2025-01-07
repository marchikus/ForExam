using TMPro;
using UnityEngine;

public class GameView : MonoBehaviour
{
    private GameModel model;
    [SerializeField] private GameObject TopScore;
    [SerializeField] private TMP_Text[] playerNames;
    [SerializeField] private TMP_Text[] playerScores;
    [SerializeField] private DataBase dataBase;

    public void Initialize(GameModel gameModel)
    {
        if (gameModel == null)
        {
            return;
        }

        model = gameModel;
        model.OnGoldChanged += UpdateGoldState;
    }

    public void LoadLeaderboard()
    {
        var topPlayers = dataBase.GetTopPlayers(5);

        for (int i = 0; i < topPlayers.Count; i++)
        {
            if (i < playerNames.Length)
            {
                playerNames[i].text = topPlayers[i].Name;
                playerScores[i].text = topPlayers[i].Score.ToString();
            }
        }
    }

    private void UpdateGoldState(GameModel gameModel)
    {
        if (GameModel.GoldCount == GameModel.MaxGold)
        {
            TopScore.SetActive(true);
           // saveButton.SetActive(true);
            LoadLeaderboard();
        }
    }

    private void OnDestroy()
    {
        if (model != null)
        {
            model.OnGoldChanged -= UpdateGoldState;
        }
    }
}
