using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadCafe()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadCredit()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadMachine()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
   /* public void LoadPause()
    {
        SceneManager.LoadScene(3);
    } */

    public void QuitGame()
    {
        Application.Quit();//konsola yazýyor
        Debug.Log("Oyun kapatýldý.");
    }
}
