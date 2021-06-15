using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMagicRingStory : StoryItemBase
{

    // Update is called once per frame
    protected override void executeEvent()
    {
        Debug.Log(text);
    }
}
