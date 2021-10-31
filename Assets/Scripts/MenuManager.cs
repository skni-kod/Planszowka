using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject przejscie;
    private string sceneName;
    public void ChangeScene(string name)
    {
        sceneName = name;
        przejscie.SetActive(true);
        Invoke("ChangeScene2", 2f);
    }
    private void ChangeScene2()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
