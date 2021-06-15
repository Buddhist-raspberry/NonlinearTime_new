using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLabStory : StoryItemBase
{
    // Start is called before the first frame update
    protected override void executeEvent()
    {
        string next_Scene = "Level_02";
        SceneManager.LoadSceneAsync(next_Scene);
    }
}
