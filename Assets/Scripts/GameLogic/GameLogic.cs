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
    public GameObject gameOverUI;
    public GameObject gameRestartUI;
    public GameObject postprocessing;

    private Volume volume_gaming;
    private Volume volume_dead;
    
    void Awake() {
        volume_gaming = postprocessing.GetComponents<Volume>()[0];
        volume_dead = postprocessing.GetComponents<Volume>()[1];
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        GameStart();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Die()
    {
        GameOver();
        GlobalTimeController.instance.Pause();
        gameOverUI.SetActive(true);
        gameRestartUI.SetActive(true);
        volume_gaming.weight = 0;
        volume_dead.weight = 1.0f;
    }
    public void GameStart()
    {
        gameOverUI.SetActive(false);
        gameRestartUI.SetActive(false);
        isStart = true;
        isEnd = isPause = false;
        volume_gaming.weight = 1.0f;
        volume_dead.weight = 0;
    }

    public void GameRestart()
    {
        gameOverUI.SetActive(false);
        gameRestartUI.SetActive(false);
        isStart = true;
        isEnd = isPause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        Debug.Log("重新开始");
        SceneManager.LoadScene("Level1");
        volume_gaming.weight = 1.0f;
        volume_dead.weight = 0;
    }
    public void GameOver()
    {
        isEnd = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
