using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // See on nupu PLAY jaoks, mis suunab meid esimesele Scene-le.
        SceneManager.LoadScene(1);

    }

    public void QuitGame()
    {
        // See on nupu Quit jaoks, mis panem meie mängu kinni.
        Application.Quit();

    }
}
