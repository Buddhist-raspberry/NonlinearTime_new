using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance { get; protected set; }
    bool isStart = false;
    bool isEnd = false;
    bool isPause = false;
    public GameObject gamingUI;
    public GameObject gameOverUI;
    public GameObject gamePauseUI;

    public GameObject volume_gaming;
    public GameObject volume_dead;
    public GameObject player;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (isStart&&!isPause&&!isEnd)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                gamePauseUI.SetActive(true);
                gamingUI.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GamePause();
            }
        }
        else if(isStart&&isPause&&!isEnd)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                gamingUI.SetActive(true);
                gamePauseUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameResume();
            }
        }

    }
    public void Die()
    {
        GameOver();
        GlobalTimeController.instance.Pause();
        gameOverUI.SetActive(true);
        gamePauseUI.SetActive(false);
        gamingUI.SetActive(false);
        volume_gaming.SetActive(false);
        volume_dead.SetActive(true);
    }
    public void GameStart()
    {
        gameOverUI.SetActive(false);
        gamePauseUI.SetActive(false);
        gamingUI.SetActive(true);
        isStart = true;
        isEnd = isPause = false;
        volume_gaming.SetActive(true);
        volume_dead.SetActive(false);
        Debug.Log("GameStart!");
    }

    public void GameRestart()
    {
        Debug.Log("重新开始");
        SceneManager.LoadScene("Level1");
    }
    public void GameOver()
    {
        GamePause();
        isEnd = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GamePause()
    {
        isPause = true;
        player.GetComponent<PlayerController>().isEnabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<GlobalTimeController>().Pause();
        player.GetComponent<CharacterController>().enabled = false;
        Debug.Log("GamePause!");
    }
    public void GamePauseUI(){
        gamePauseUI.SetActive(true);
        gamingUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GamePause();
    }
    public void GameResumeUI(){
        gamingUI.SetActive(true);
        gamePauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameResume();
    }
    public void GameResume()
    {
        isPause = false;
        player.GetComponent<PlayerController>().isEnabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<GlobalTimeController>().SetEnabled();
        player.GetComponent<CharacterController>().enabled = true;
        Debug.Log("GameResume!");
    }

    public void BacktoMainMenu()
    {
        gamePauseUI.SetActive(false);
        SceneManager.LoadScene("Menu_Scene");
    }
}
