using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenThirdDoorStory : StoryItemBase
{
    public Animator DoorController;
    protected override void executeEvent()
    {
        DoorController.SetBool("isOpen",true);
        Debug.Log(text);
    }
}
