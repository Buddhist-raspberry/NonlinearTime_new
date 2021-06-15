using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPillsStory : StoryItemBase
{
    // Start is called before the first frame update
    protected override void executeEvent()
    {
        Debug.Log(text);
    }
}
