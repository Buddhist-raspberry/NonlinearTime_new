using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryItem : StoryItemBase
{


    protected override void executeEvent()
    {
        Debug.Log(text);
    }
}
