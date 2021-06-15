using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDisplay : StoryItemBase
{ 
    public GameObject door;
    public BGM bgm;

    protected override void executeEvent()
    {
        Debug.Log(text);
        door.SetActive(true);
        bgm.win();
    }
}
