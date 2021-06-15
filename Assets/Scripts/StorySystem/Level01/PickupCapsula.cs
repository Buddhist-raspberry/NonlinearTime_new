using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PickupCapsula : StoryItemBase
{
    public PlayableDirector pickUpCapsulaTimeline;

    protected override void executeEvent()
    {
        Debug.Log(text);
        pickUpCapsulaTimeline.Play();
    }
}
