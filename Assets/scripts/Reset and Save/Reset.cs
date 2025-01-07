using UnityEngine;
using UnityEngine.SceneManagement;

public class Reseter : MonoBehaviour
{
    [SerializeField] private GameObject TopScore;
    [SerializeField] private Saver saver;
    [SerializeField] private GameSettings gameSettings;

    public void ResetButton()
    {
        if (TopScore != null)
        {
            TopScore.SetActive(false);
        }

        saver.DeleteSaveFile();
        GameModel.ResetValues();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
