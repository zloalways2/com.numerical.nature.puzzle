using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameObject loadingScreen;
    public void Click()
    {
        loadingScreen.SetActive(true);
        if (DataTransfer.SceneIndex < 14)
        {
            DataTransfer.SceneIndex++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
