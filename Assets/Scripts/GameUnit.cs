using UnityEngine;
using UnityEngine.UI;

public class GameUnit : MonoBehaviour
{
    GameManager manager;
    public Vector2Int pos;
    public int index;
    Image img;
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }
    public void Click()
    {
        manager.Click(pos);
    }
    public void UpdatePos(Vector2 p)
    {
        if(img==null)
            img = GetComponent<Image>();
        img.rectTransform.position = p;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
