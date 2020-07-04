using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private AudioSource audioSource;

    [HideInInspector]
    public static bool isGamePaused = false;

    public GameObject pauseUIPanel;
    public GameObject gameUIPanel;

    public AudioClip pauseClip;

    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    public void Pause()
    {
        this.audioSource.PlayOneShot(this.pauseClip);
        this.gameUIPanel.SetActive(false);
        this.pauseUIPanel.SetActive(true);
        Time.timeScale = 0f;

        isGamePaused = true;
    }

    public void Resume()
    {
        this.gameUIPanel.SetActive(true);
        this.pauseUIPanel.SetActive(false);
        Time.timeScale = 1f;

        isGamePaused = false;
    }

    public void Restart()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}