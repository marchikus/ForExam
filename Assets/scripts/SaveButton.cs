using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    [SerializeField] private DataBase dataBase;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameView gameView;
    [SerializeField] private GameObject saveButton;
    
    public void saveScore()
    {
        dataBase.AddPlayer(inputField.text, GameModel.ScoreCount);
        gameView.LoadLeaderboard();
        saveButton.SetActive(false);
    }

}
