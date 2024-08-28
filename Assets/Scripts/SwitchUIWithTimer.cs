using UnityEngine;

public class SwitchUIWithTimer : MonoBehaviour
{
    public GameObject prevPanel, currentPanel;
    public void Click(bool stop)
    {
        if (stop)
        {
            TimeManager.Stop();
        }
        else
        {
            TimeManager.Continue();
        }
        currentPanel.SetActive(false);
        prevPanel.SetActive(true);
    }
}
