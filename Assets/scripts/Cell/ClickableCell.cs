using UnityEngine;
using UnityEngine.UI;

public class ClickableCell : MonoBehaviour
{
    public event System.Action<Vector3> OnCellClicked;
    public event System.Action OnShovelStateUpdated;

    private static ShovelTexter shovelTexter;
    private Button button;
    private GameModel model;
    private bool golded = false;
     
    public void Initialize(GameModel gameModel)
    {
        model = gameModel;
        model.OnShovelChanged += UpdateShovelState;
    }

    private void Awake()
    {
        shovelTexter = FindObjectOfType<ShovelTexter>();
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClickHandler);
        }
    }

    private void OnClickHandler()
    {

        if (GameModel.Shovel > 0)
        {
            OnCellClicked?.Invoke(transform.position);
            ProcessShovel();
            UpdateShovelText();
        }
    }

    public void SetGolded(bool state)
    {
        golded = state;

        // Управляем состоянием кнопки
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.interactable = !state; // Активируем кнопку, если golded = false
            Debug.Log($"Button interactable set to: {!state}");
        }
        else
        {
            Debug.LogWarning("Button component not found on ClickableCell!");
        }

        Debug.Log($"golded set to: {state}");
    }

    private void ProcessShovel()
    {
        if (model != null)
        {
            model.UseShovel();
        }
    }
    private void UpdateShovelState(GameModel updatedModel)
    {
        if (updatedModel.GetShovelCount())
        {
            OnShovelStateUpdated?.Invoke();
        }
    }
    
    private void UpdateShovelText()
    {
        shovelTexter.UpdateShovelText();
    }

    public void DisableButton()
    {
        if (button != null)
        {
            button.interactable = false; // Отключает возможность нажимать на кнопку
        }
    }

    private void OnDestroy()
    {
        model.OnShovelChanged -= UpdateShovelState;
    }
}
