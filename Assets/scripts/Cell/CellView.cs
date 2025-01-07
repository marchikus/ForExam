using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField] private Button cellButton;
    [SerializeField] private Image cellImage;

    private CellModel model;
    public void Initialize(CellModel cellModel)
    {
        model = cellModel;
        model.OnDepthChanged += UpdateCellState;
        cellButton.onClick.AddListener(OnClickHandler);

        if(model.Depth == 0)
        {
            DisableCell();
        }
    }

    private void OnClickHandler()
    {
        model.DecreaseDepth();
    }

    private void UpdateCellState(CellModel updatedModel)
    {
        if (updatedModel.IsDepleted())
        {
            DisableCell();
        }
    }

    private void DisableCell()
    {
        cellImage.enabled = false; 
    }

    private void OnDestroy()
    {
        model.OnDepthChanged -= UpdateCellState;
    }
}
