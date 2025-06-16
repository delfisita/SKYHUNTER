using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneManagerr : MonoBehaviour
{
    public void PlayLvl()
    {
        SceneManager.LoadScene("Juego");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
