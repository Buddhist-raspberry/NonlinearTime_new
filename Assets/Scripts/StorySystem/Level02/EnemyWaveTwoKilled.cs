using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveTwoKilled : StoryItemBase
{
    public Animator CaptionController;
    // Start is called before the first frame update
    protected override void executeEvent()
    {
        CaptionController.SetTrigger("activeNext");
        Debug.Log(text);
    }
}
