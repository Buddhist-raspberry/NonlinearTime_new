using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DoorStory : StoryItemBase
{
    public Animator doorContrloer;
    protected override void executeEvent()
    {
        Debug.Log(text);
        doorContrloer.SetBool("isOpen",true);
    }

}
