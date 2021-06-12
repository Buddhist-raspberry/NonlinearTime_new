using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PowercellStory : StoryItemBase
{
    public GameObject player;
    public PlayableDirector director;
    public GameObject powercell;
    protected override void executeEvent()
    {
        Debug.Log(text);
        hasTriggered = true;
        director.Play();
        InvokeRepeating("gameStart",0,0.1f);
    }
    void gameStart(){
        if(director.state==PlayState.Paused){
            player.GetComponent<PlayerController>().isEnabled = true;
            player.GetComponent<PlayerController>().ChangeUseStatus(PlayerController.UseStatus.CONTROL);
            player.GetComponent<GlobalTimeController>().isEnabled = true;
            StopAllCoroutines();
            powercell.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
