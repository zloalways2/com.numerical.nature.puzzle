using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text level_text,time_text, moves_text;
    public Text winmoves_text,wintime_text;
    public GameObject[] stars;
    public Image[] units_images;
    GameUnit[][] units = new GameUnit[4][];
    Vector2Int free_cell_pos;
    Vector2[][] positions = new Vector2[4][];
    int moves;
    public GameObject winUI, gameUI;
    int[,] levels = { { 11, 10, 14, 0, 2, 4, 9, 8, 1, 5, 13, 7, 3, 6, 15 }, { 11, 14, 5, 4, 10, 7, 3, 6, 1, 8, 9, 13, 0, 12, 15 }, { 5, 1, 12, 14, 10, 8, 11, 6, 7, 9, 2, 3, 4, 0, 15 }, { 3, 8, 4, 13, 14, 1, 0, 11, 12, 10, 7, 5, 9, 2, 15 }, { 6, 3, 0, 9, 8, 5, 13, 11, 12, 14, 7, 2, 10, 4, 15 }, { 6, 10, 5, 0, 1, 7, 14, 13, 3, 12, 11, 2, 8, 9, 15 }, { 2, 1, 6, 11, 9, 13, 3, 12, 7, 14, 4, 5, 8, 0, 15 }, { 0, 4, 8, 14, 5, 6, 2, 12, 3, 10, 7, 13, 1, 9, 15 }, { 11, 6, 10, 5, 14, 3, 4, 8, 7, 2, 13, 9, 12, 1, 15 }, { 11, 8, 6, 10, 1, 5, 2, 14, 12, 0, 13, 4, 9, 7, 15 }, { 14, 12, 11, 2, 6, 13, 10, 8, 7, 5, 1, 9, 4, 0, 15 }, { 6, 3, 11, 0, 7, 9, 14, 4, 10, 5, 12, 13, 1, 2, 15 }, { 9, 5, 11, 1, 12, 10, 6, 2, 3, 13, 7, 0, 4, 14, 15 }, { 13, 10, 12, 5, 9, 11, 2, 8, 4, 7, 0, 1, 6, 14, 15 }, { 8, 14, 2, 12, 0, 6, 1, 13, 10, 11, 3, 5, 9, 7, 15 } };
    //random shuffles generation
    /*void Gen()
    {
        string ans = "{";
        for (int g = 0; g < 15; ++g)
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < 16; ++i)
                indices.Add(i);
            int free_cell = Random.Range(0,15);
            indices.RemoveAt(free_cell);
            List<int> shuffle = new List<int>();
            while (indices.Count > 0)
            {
                int rem = Random.Range(0, indices.Count - 1);
                shuffle.Add(indices[rem]);
                indices.RemoveAt(rem);
            }
            string shfl = "";
            for(int f=0;f<15;++f)
            {
                shfl += shuffle[f];
                if (f != 14)
                    shfl += ",";
            }
            ans += "{" + shfl + "}";
            if(g!=14)
                ans+=",";
        }
        ans += "}";
        print(ans);
    }*/
    void Start(){
        //Gen();
        TimeManager.Init();
        level_text.text = "Level " + (DataTransfer.SceneIndex+1);
        Vector2 origin = units_images[0].rectTransform.position;
        Vector2 dist = new Vector2(units_images[1].rectTransform.position.x - units_images[0].transform.position.x,
                units_images[4].rectTransform.position.y - units_images[0].transform.position.y);
        for(int i = 0; i < 4; ++i){
            positions[i] = new Vector2[4];
            for (int j = 0; j < 4; ++j)
            {
                positions[i][j] = origin+new Vector2(dist.x*j,dist.y*i);
            }
        }
        for (int i = 0; i < 4; ++i)
            units[i] = new GameUnit[4];
        for (int i = 0; i < 15; ++i)
        {
            int shuffle_x = levels[DataTransfer.SceneIndex,i] % 4;
            int shuffle_y = levels[DataTransfer.SceneIndex, i] / 4;
            var unit_class = units_images[i].GetComponent<GameUnit>();
            unit_class.index = i+1;
            units[shuffle_y][shuffle_x] = unit_class;
            unit_class.pos = new Vector2Int(shuffle_x, shuffle_y);
        }
        for(int i = 0; i < 4; ++i)
        {
            for(int j = 0; j < 4; ++j)
            {
                if (units[i][j] == null)
                {
                    free_cell_pos = new Vector2Int(j, i);
                }
            }
        }
        UpdateRectTransforms();
    }
    void UpdateRectTransforms()
    {
        for (int i = 0; i < 4; ++i)
        {
            for(int j = 0; j < 4; ++j)
            {
                if (free_cell_pos != new Vector2Int(j, i))
                {
                    units[i][j].UpdatePos(positions[units[i][j].pos.y][units[i][j].pos.x]);
                }
            }
        }
    }
    public void Click(Vector2Int pos)
    {
        if (pos.x != free_cell_pos.x && pos.y != free_cell_pos.y)
            return;
        Vector2Int next_freepos=free_cell_pos;
        if (pos.x == free_cell_pos.x)
        {
            if (free_cell_pos.y > pos.y)
            {
                for(int i = free_cell_pos.y; i - 1 >= pos.y; --i)
                {
                    units[i - 1][pos.x].pos.y++;
                    units[i][pos.x] = units[i-1][pos.x];
                    units[i - 1][pos.x] = null;
                    next_freepos = new Vector2Int(pos.x, i - 1);
                }
            }
            if(free_cell_pos.y < pos.y)
            {
                for (int i = free_cell_pos.y; i + 1 <= pos.y; ++i)
                {
                    units[i + 1][pos.x].pos.y--;
                    units[i][pos.x] = units[i + 1][pos.x];
                    units[i + 1][pos.x] = null;
                    next_freepos = new Vector2Int(pos.x, i + 1);
                }
            }
        }else if (pos.y == free_cell_pos.y)
        {
            if (free_cell_pos.x > pos.x)
            {
                for (int j = free_cell_pos.x; j - 1 >= pos.x; --j)
                {
                    units[pos.y][j-1].pos.x++;
                    units[pos.y][j] = units[pos.y][j-1];
                    units[pos.y][j-1] = null;
                    next_freepos = new Vector2Int(j-1, pos.y);
                }
            }
            if (free_cell_pos.x < pos.x)
            {
                for (int j = free_cell_pos.x; j + 1 <= pos.x; ++j)
                {
                    units[pos.y][j + 1].pos.x--;
                    units[pos.y][j] = units[pos.y][j + 1];
                    units[pos.y][j + 1] = null;
                    next_freepos = new Vector2Int(j + 1, pos.y);
                }
            }
        }
        free_cell_pos = next_freepos;
        UpdateRectTransforms();
        moves++;
        moves_text.text = moves.ToString();
        int[] state = new int[15];
        if (free_cell_pos == new Vector2Int(3, 3)) {
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                    if (i != 3 || j != 3)
                        state[i * 4 + j] = units[i][j].index;
            bool is_sorted = true;
            for(int i = 0; i < 14; ++i)
                is_sorted = is_sorted && (state[i + 1] > state[i]);
            if (is_sorted)
            {
                winUI.SetActive(true);
                gameUI.SetActive(false);
                var time = TimeManager.Get();
                int stars_count = 1;
                if (time.minutes * 60 + time.seconds <= 6 * 60)
                    stars_count++;
                if (time.minutes * 60 + time.seconds <= 4 * 60)
                    stars_count++;
                PlayerPrefs.SetInt("level" + DataTransfer.SceneIndex,
                    Mathf.Max(stars_count,PlayerPrefs.GetInt("level" + DataTransfer.SceneIndex)));
                if (DataTransfer.SceneIndex < 14)
                    PlayerPrefs.SetInt("level" + (DataTransfer.SceneIndex+1),
                        Mathf.Max(0, PlayerPrefs.GetInt("level" + (DataTransfer.SceneIndex+1))));
                for (int i = 0; i < stars_count; ++i)
                {
                    stars[i].SetActive(stars[i]);
                }
                wintime_text.text = time.minutes.ToString() + ":" + time.seconds.ToString();
                winmoves_text.text = moves.ToString();
            }
        }
    }
    void Update()
    {
        var time=TimeManager.Get();
        time_text.text = time.minutes.ToString() + ":" + time.seconds.ToString();
    }
}
