using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button returnMenu;
    [SerializeField] private Button quitButton;
    public void MainMenu()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Button);
        Data.Instance.Reset();
        SceneManager.LoadScene("00 Menu");
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Button);
        Application.Quit();
    }
}
