using UnityEngine;

public class CellModel
{
    private GameSettings settings;
    public GameObject CellGameObject { get; set; }
    public int Depth { get; set; }
    public int MaxDepth { get; private set; }
    public event System.Action<CellModel> OnDepthChanged;

    public CellModel(GameSettings gameSettings)
    {
        settings = gameSettings;
        MaxDepth = settings.maxDepth;
        Depth = MaxDepth;
    }

    public void SetDepth(int depth)
    {
        Depth = depth;
    }

    public void DecreaseDepth()
    {
        if (GameModel.Shovel > 0)
        {
            if (Depth > 0)
            {
                Depth--;
                OnDepthChanged?.Invoke(this);
            }
        }
        else
        {
            Debug.Log("no shovels");
        }
    }

    public bool IsDepleted() => Depth == 0;
}
