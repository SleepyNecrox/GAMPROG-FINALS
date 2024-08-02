using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
     void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Button);
        SceneManager.LoadScene("0 Prototype");
    }

    public void Quit()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Button);
        Application.Quit();
    }
}
