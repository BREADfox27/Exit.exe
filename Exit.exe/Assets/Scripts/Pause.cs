using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    public GameObject optionsmenu;
    public GameObject escIcon;

    public void Mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        optionsmenu.SetActive(true);
    }

    public void Return()
    {
        optionsmenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
