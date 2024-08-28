using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public void Click(int scene_index)
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(scene_index);
    }
}
