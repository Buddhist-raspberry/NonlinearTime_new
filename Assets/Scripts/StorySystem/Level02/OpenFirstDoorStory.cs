using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFirstDoorStory : StoryItemBase
{
    public Animator DoorController;
    public Animator HeatController;
    public Animator CaptionController;
    
    protected override void executeEvent(){
        DoorController.SetBool("isOpen",true);
        HeatController.SetBool("isActive",true);
        CaptionController.SetTrigger("activeNext");
        Debug.Log(text);
    }
}
