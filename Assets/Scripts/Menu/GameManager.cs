using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] private GameObject _gameOverCanvas; // canvas to restart the game (button)
    [SerializeField] private GameObject nextSceneText; // text between maps
    [SerializeField] private AudioSource _audioSource; 
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private Transform player; // Reference to player object
    [SerializeField] private Transform finishLine; // Reference to finish on the map
    [SerializeField] private string nextLevelName;
    [SerializeField] private GameObject menu;
    private bool menuActive;
 
    private bool hasReachedFinishLine = false;

    private void Awake() {
        if(instance==null) {
            instance=this;
        }
        Time.timeScale = 1f;
    }

// Throws ArgumentNullException after the start due to disabled GameOverCanvas UI, game works fine though
    public void GameOver() {
        if(_gameOverCanvas != null ) {
            _gameOverCanvas.SetActive(true);
        }
        Time.timeScale = 0f;
    }
// Loading actual scene another time
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
// Loading first map (second scene)
    public void NewGame() {
        SceneManager.LoadScene(nextLevelName);
    }

// Loading next map / pause menu
    private void Update() {

        if(Input.GetKeyDown(KeyCode.Escape)) { 
            if(menu != null) { 
                Pause();
            }
        }

        if (!hasReachedFinishLine && player.position.x >= finishLine.position.x) {
            hasReachedFinishLine = true;
            _audioSource.clip = _audioClip;
            _audioSource.volume = 0.7f;
            _audioSource.Play();
            // nextSceneText.SetActive(true);
            // Couroutine neccessary to wait a bit before changing the map
            StartCoroutine(TransitionToNextLevel());
        }
    }

    private IEnumerator TransitionToNextLevel() {
        if(nextLevelName != "EndGame") {
            nextSceneText.SetActive(true);
        }
        // Wait 1.35s before chaning the map
        yield return new WaitForSeconds(1.35f);
        // Load next level
        SceneManager.LoadScene(nextLevelName);
        nextSceneText.SetActive(false);
        hasReachedFinishLine = false;
    }

    public void Pause() {
        menuActive = !menuActive;
        // Finding all audios ingame
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audioSources)
        {
            // mute every sound found ingame
            audio.mute = menuActive;
        }
        menu.SetActive(menuActive);
        Time.timeScale = menuActive ? 0 : 1;
    }


    public void Menu() {
        SceneManager.LoadScene("StartGame");
    }


    public void Exit() {
        Application.Quit();
    }

    public void Reset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
