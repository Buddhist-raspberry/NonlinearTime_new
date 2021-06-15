using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFirstDoorStory : StoryItemBase
{
    public Animator DoorController;
    public Animator HeatController;
    
    protected override void executeEvent(){
        DoorController.SetBool("isOpen",true);
        HeatController.SetBool("isActive",true);
        Debug.Log(text);
    }
}
