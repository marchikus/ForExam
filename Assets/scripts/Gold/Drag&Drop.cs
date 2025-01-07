using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Vector3 initialPosition;
    private Image image;
    private GameModel model;
    private Canvas _canvas;
    private Transform originalParent;

    public event System.Action <GameModel> OnGoldStateUpdated;

    public void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
    }
    public void Initialize(GameModel gameModel)
    {
        if (gameModel != null)
        {
            model = gameModel;
            model.OnGoldChanged += UpdateGoldState;
        }
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        initialPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        transform.SetParent(_canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta/ _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        if (!IsOverlappingWithGoldField())
        {
            rectTransform.anchoredPosition = initialPosition;
        }

        transform.SetParent(originalParent, false);
    }

    private bool IsOverlappingWithGoldField()
    {

        Vector2 worldPosition = rectTransform.position;
        Collider2D[] colliders = Physics2D.OverlapPointAll(worldPosition);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("DropZone"))
            {
                ProcessGold();
                return true;
            }
        }
        return false;
    }

    private void ProcessGold()
    {
        if (model != null)
        {
            model.AddGold();
            model.AddScore();
            Debug.Log("+1 Золото");

            ClickableCell clickableCell = originalParent.GetComponent<ClickableCell>();

            if (clickableCell != null)
            {
                clickableCell.SetGolded(false);
            }
            else
            {
                Debug.LogWarning("папы ClickableCell нету");
            }

            Destroy(gameObject);
        }
    }

    private void UpdateGoldState(GameModel updatedModel)
    {
        if (updatedModel.GetGoldCount())
        {
            OnGoldStateUpdated?.Invoke(updatedModel);
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