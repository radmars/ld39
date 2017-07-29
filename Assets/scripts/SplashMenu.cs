using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("the greatest scene");
        }
    }
}
