using UnityEngine;
using UnityEngine.UI;
public enum MusicViewType
{
    Music,
    Sounds
}

public class MusicView : MonoBehaviour
{
    public Sprite active_left, inactive_left;
    public Sprite active_center, inactive_center;
    public Sprite active_right, inactive_right;
    public AudioManager am;
    public Image[] body;
    public MusicViewType type;
    int value;
    void Recalc()
    {
        if (type == MusicViewType.Music)
        {
            am.UpdateMusicVolume(value);
        }
        if (type == MusicViewType.Sounds)
        {
            am.UpdateSoundsVolume(value);
        }
        for (int i = 0; i < body.Length; ++i)
        {
            Sprite s;
            if (i == 0)
                s = inactive_left;
            else if (i == body.Length - 1)
                s = inactive_center;
            else
                s = inactive_right;
            body[i].sprite = s;
        }
        for (int i = 0; i < value; ++i)
        {
            Sprite s;
            if (i == 0)
                s = active_left;
            else if (i == body.Length - 1)
                s = active_right;
            else
                s = active_center;
            body[i].sprite = s;
        }
    }
    public void Increase()
    {
        value = Mathf.Min(body.Length, value+1);
        Recalc();
    }
    public void Decrese()
    {
        value = Mathf.Max(0, value - 1);
        Recalc();
    }
    public void Init(AudioManager amanager,int v)
    {
        am = amanager;
        value = v;
        Recalc();
    }
}
