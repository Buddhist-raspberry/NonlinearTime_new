using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PowercellStory : StoryItemBase
{
    public GameObject player;
    public PlayableDirector powercellTimeline;
    public PlayableDirector TimeSlowTimeline;
    public GameObject powercell;
    protected override void executeEvent()
    {
        Debug.Log(text);
        hasTriggered = true;
        powercellTimeline.Play();
        InvokeRepeating("gameStart",0,0.1f);
    }
    void gameStart(){
        if(powercellTimeline.state==PlayState.Paused){
            GameLogic.instance.GameResume();
            player.GetComponent<PlayerController>().canControl = true;
            player.GetComponent<PlayerController>().ChangeUseStatus(PlayerController.UseStatus.CONTROL);
            TimeSlowTimeline.Play();
            powercell.SetActive(false);
            gameObject.SetActive(false);
            CancelInvoke();
        }
    }
}
