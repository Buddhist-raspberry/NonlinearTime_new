using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKeyStory : StoryItemBase
{
    protected override void executeEvent()
    {
        Debug.Log(text);
    }
}
