using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : StoryItemBase
{
    public string nextLevel = "Level_01";

    protected override void executeEvent()
    {
        Debug.Log(text);
        SceneManager.LoadScene(nextLevel);
    }
}
