using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyWaveStory : StoryItemBase
{
    public Animator doorContrloer;
    public PlayableDirector EnemyWaveTimeline;
    public EnemyWave enemyWave;
    protected override void executeEvent()
    {
        Debug.Log(text);
        doorContrloer.SetBool("isOpen",true);
        EnemyWaveTimeline.Play();
        enemyWave.SetEnabled(false);
        InvokeRepeating("animDetect",0,0.1f);
    }

    void animDetect(){
        if(EnemyWaveTimeline.state != PlayState.Playing){
            gameObject.SetActive(false);
            enemyWave.SetEnabled(true);
            CancelInvoke();
        }
    }

}
