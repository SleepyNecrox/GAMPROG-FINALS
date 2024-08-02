using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button returnMenu;
    [SerializeField] private Button quitButton;
    private bool isCooldown = false;
    public static bool isPaused = false;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        Debug.Log("Start");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            if(!isCooldown && !playerMovement.isDead)
            {
                if (isPaused)
                {
                    ResumeGame();
                    StartCoroutine(Wait());
                }
                else
                {
                    PauseGame();
                    StartCoroutine(Wait());
                }
            }
            
        }
    }

    public void PauseGame()
    {
       AudioManager.Instance.PlaySFX(AudioManager.Instance.Button);
       pauseMenu.SetActive(true);
       Time.timeScale = 0f;
       isPaused = true;
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
    }

    public void ResumeGame()
    {
       AudioManager.Instance.PlaySFX(AudioManager.Instance.Button);
       pauseMenu.SetActive(false);
       Time.timeScale = 1f;
       isPaused = false;
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Data.Instance.Reset();
        SceneManager.LoadScene("00 Menu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    internal IEnumerator Wait()
    {
        isCooldown = true;
        yield return new WaitForSeconds(0.25f);
        isCooldown = false;
    }
}
