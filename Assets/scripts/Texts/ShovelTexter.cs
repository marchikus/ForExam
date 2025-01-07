using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShovelTexter : MonoBehaviour
{
    SaveData saveData;
    [SerializeField] private Text ShovelText;
    private int ShovelCount;

    private void Start()
    {
        HaveSave();
        ShovelText.text = $"{ShovelCount}";
    }

    public void HaveSave()
    {
        saveData = LoadSavedGameData();

        if (saveData != null)
        {
            ShovelCount = saveData.shovels;
        }
        else { ShovelCount = GameModel.Shovel; }

    }

    public void UpdateShovelText()
    {
        ShovelCount--;
        ShovelText.text = $"{ShovelCount}";
    }

    private SaveData LoadSavedGameData()
    {
        return new Saver().LoadGame();
    }
}
