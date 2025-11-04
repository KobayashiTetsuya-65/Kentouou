using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditButton : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("Credit");
    }
}