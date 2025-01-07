using System.Collections.Generic;
using UnityEngine;

public class InitSave : MonoBehaviour
{
    SaveData saveLoad;
    [SerializeField] Saver saver;
    [SerializeField] private GridManager gridManager;
    private List<CellModel> cellModels;

    private void OnEnable()
    {
        saver.LoadGame();
    }

    private void Start()
    {
        LoadCellModels();
    }

    private void LoadCellModels()
    {
        cellModels = gridManager.GetCellModels();
    }

    public SaveData CreateSaveLoad()
    {
        saveLoad = new SaveData
        {
            shovels = GameModel.Shovel,
            collectedGold = GameModel.GoldCount,
            ShovelsText = GameModel.Shovel,
            cells = new List<CellData>(),
            Score = GameModel.ScoreCount
        };

        foreach (var cellModel in cellModels)
        {
            bool hasGold = false;

            if (cellModel.CellGameObject != null)
            {
                foreach (Transform child in cellModel.CellGameObject.transform)
                {
                    if (child.CompareTag("Gold"))
                    {
                        hasGold = true;
                        break;
                    }
                }
            }

            CellData cellData = new CellData(cellModel.Depth)
            {
                gold = hasGold
            };

            saveLoad.cells.Add(cellData);
        }

        return saveLoad; 
    }

    private void OnApplicationQuit()
    {
        saver.SaveGame(CreateSaveLoad());
    }
}