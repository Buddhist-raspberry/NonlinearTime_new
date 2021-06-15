using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLabStory : StoryItemBase
{
    public string next_level="level_04";
    // Start is called before the first frame update
    protected override void executeEvent()
    {
        string next_Scene = next_level;
        SceneManager.LoadSceneAsync(next_Scene);
    }
}
