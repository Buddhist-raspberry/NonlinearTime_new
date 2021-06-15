using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKeyStory : StoryItemBase
{
    public Animator CaptionController;
    protected override void executeEvent()
    {
        CaptionController.SetTrigger("activeNext");
        Debug.Log(text);
    }
}
