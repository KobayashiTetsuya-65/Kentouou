using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("Main");
    }
}