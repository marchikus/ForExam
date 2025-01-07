using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private RectTransform gridParent;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject goldPrefab;
    [SerializeField] private GameSettings gameSettings;

    private List<CellModel> cellModels = new List<CellModel>();

    private SaveData savedData;

    private void Start()
    {
        savedData = LoadSavedGameData();
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        // Удаление старых клеток
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        cellModels.Clear();

        int rows = gameSettings.gridRows;
        int columns = gameSettings.gridColumns;

        for (int i = 0; i < rows * columns; i++)
        {
            GameObject cell = Instantiate(cellPrefab, gridParent);
            cell.name = $"Cell ({i % columns}, {i / columns})";

            CellModel model = new CellModel(gameSettings);
            model.CellGameObject = cell;

            if (savedData != null && i < savedData.cells.Count)
            {
                int depth = savedData.cells[i].depth;
                model.SetDepth(depth);

                bool hasGold = savedData.cells[i].gold;
                if (hasGold)
                {
                    Vector3 spawnPosition = cell.transform.position;
                    GameObject goldObject = Instantiate(goldPrefab, spawnPosition, Quaternion.identity, cell.transform);
                    ClickableCell clickableCell = cell.GetComponent<ClickableCell>();
                    if (clickableCell != null)
                    {
                        clickableCell.DisableButton();
                    }
                }
            }

            cellModels.Add(model);

            CellView view = cell.GetComponent<CellView>();
            if (view != null)
            {
                view.Initialize(model);
            }
        }
    }

    public void UpdateGridSize(int newRows, int newColumns)
    {
        gameSettings.gridRows = newRows;
        gameSettings.gridColumns = newColumns;
        GenerateGrid();
    }

    public List<CellModel> GetCellModels()
    {
        return cellModels;
    }

    private SaveData LoadSavedGameData()
    {
        return new Saver().LoadGame();
    }
}
