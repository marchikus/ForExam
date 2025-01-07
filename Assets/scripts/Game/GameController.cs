using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private ClickableCell clickableCell;
    [SerializeField] private GoldSpawn goldSpawner;
    [SerializeField] private Saver saver;
    private GameModel model;

    private void Awake()
    { 
        model = new GameModel();
        model.HaveSave();
        if(model.GetGoldCount())
        {
            saver.DeleteSaveFile();
            GameModel.ResetValues();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }

    public GameModel Model => model;

    private void Start()
    {
        if (clickableCell != null && goldSpawner != null)
        {
            clickableCell.OnCellClicked += goldSpawner.SpawnGold;
        }
    }

}
