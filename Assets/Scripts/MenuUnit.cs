using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUnit : MonoBehaviour{
    public int indice;
    public GameObject[] yellowstars;
    public GameObject unlockedUI;
    public bool is_clickable;
    public GameObject lockedUI;
    public GameObject loadingScreen;
    void Start()
    {
        if (!PlayerPrefs.HasKey("level" + indice))
            PlayerPrefs.SetInt("level" + indice, indice == 0 ? 0 : -1);
        int r = PlayerPrefs.GetInt("level" + indice);
        if (r >= 0)
        {
            unlockedUI.SetActive(true);
            lockedUI.SetActive(false);
            for (int i = 0; i < 3; ++i)
                yellowstars[i].SetActive(false);
            for (int i = 0; i < r; ++i)
                yellowstars[i].SetActive(true);
            is_clickable = true;
        }
    }
    public void Click()
    {
        if (!is_clickable)
            return;
        loadingScreen.SetActive(true);
        DataTransfer.SceneIndex = indice;
        SceneManager.LoadScene(2);//game
    }
}
