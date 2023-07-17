using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Scenes
    public void LoadLevel(int levelNumber) {
        switch(levelNumber) {
            case 1:
            SceneManager.LoadScene("Level_01");
                break;
            //if no level has been found, do noting.
            default:
                Debug.Log("Level not found");
                break;
        }
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame() {
        Application.Quit();
    }
    #endregion
}
