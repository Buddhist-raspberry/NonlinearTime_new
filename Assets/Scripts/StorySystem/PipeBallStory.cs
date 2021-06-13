using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PipeBallStory : StoryItemBase
{
    public Animator captionContrloer;
    protected override void executeEvent()
    {
        Debug.Log(text);
        if(captionContrloer.GetCurrentAnimatorStateInfo(0).IsName("Empty")){
            captionContrloer.Play("PipeBall");
        }
    }

}
