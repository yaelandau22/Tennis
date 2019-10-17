
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    


    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PlayGame()
    {
        GetComponent<MainMenu>().enabled = false;
        //SceneManager.LoadScene.GetComponentInParent<MainMenu>.enabled = false;
        SceneManager.LoadScene(1);
        

    }
}
