using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKeyStory : StoryItemBase
{
    public Animator CaptionController;
    public GameObject FilesInstance;
    protected override void executeEvent()
    {
        CaptionController.SetTrigger("activeNext");
        GameObject.Destroy(FilesInstance);
        Debug.Log(text);
    }
}
