using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
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