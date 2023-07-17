using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void Play() {
        GameObject.FindObjectOfType<GameManager>().LoadLevel(01);
    }

    public void Quit() {
        GameObject.FindObjectOfType<GameManager>().ExitGame();
    }
}
