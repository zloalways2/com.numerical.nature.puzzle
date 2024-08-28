using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource musicsource, soundssource;
    public void PlaySound(AudioClip sound)
    {
        soundssource.PlayOneShot(sound);
    }
    private void Load()
    {
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("music", 7);
        }
        if (!PlayerPrefs.HasKey("sounds"))
        {
            PlayerPrefs.SetInt("sounds", 7);
        }
        UpdateMusicVolume(PlayerPrefs.GetInt("music"));
        UpdateSoundsVolume(PlayerPrefs.GetInt("sounds"));
    }
    public void UpdateMusicVolume(int v)
    {
        PlayerPrefs.SetInt("music", v);
        if (v != 0)
        {
            mixer.SetFloat("music", Mathf.Log10(v * 0.1f) * 20);
        }
        else
            mixer.SetFloat("music", -80);
    }
    public void UpdateSoundsVolume(int v)
    {
        PlayerPrefs.SetInt("sounds", v);
        if (v != 0)
        {
            mixer.SetFloat("sounds", Mathf.Log10(v * 0.1f) * 20);
        }
        else
            mixer.SetFloat("sounds", -80);
    }
    private void Start()
    {
        Load();
        var mviews = FindObjectsOfType<MusicView>(true);
        foreach (var viewobj in mviews) {
            var view = viewobj.GetComponent<MusicView>();
            if (view.type == MusicViewType.Music)
                view.Init(this, PlayerPrefs.GetInt("music"));
            if (view.type == MusicViewType.Sounds)
                view.Init(this, PlayerPrefs.GetInt("sounds"));
        }
    }
}
