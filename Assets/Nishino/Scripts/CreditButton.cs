using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditButton : MonoBehaviour
{
    [SerializeField] GameObject configPanel;

    public void startBtn()
    {
        configPanel.SetActive(true);
    }

    public void ShowConfigPanel()
    {
        configPanel.SetActive(true);
    }

    public void HideConfigPanel()
    {
        configPanel.SetActive(false);
    }
}