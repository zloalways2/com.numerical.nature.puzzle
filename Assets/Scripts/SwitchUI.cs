using UnityEngine;

public class SwitchUI : MonoBehaviour
{
    public GameObject prevPanel,currentPanel;
    public void Click()
    {
        currentPanel.SetActive(false);
        prevPanel.SetActive(true);
    }
}
