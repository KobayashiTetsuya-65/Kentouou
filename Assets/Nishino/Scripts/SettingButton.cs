using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingButton : MonoBehaviour
{
    public void StartBtn(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}