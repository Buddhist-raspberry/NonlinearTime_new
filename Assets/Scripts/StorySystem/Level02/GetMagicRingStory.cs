using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMagicRingStory : StoryItemBase
{

    public Animator CaptionController;
    // Update is called once per frame
    protected override void executeEvent()
    {
        CaptionController.SetTrigger("activeNext");
        Debug.Log(text);
    }
}
