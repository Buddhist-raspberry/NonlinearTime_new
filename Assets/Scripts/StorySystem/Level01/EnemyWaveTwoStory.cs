using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyWaveTwoStory : StoryItemBase
{
    public PlayableDirector enemyWaveTwoTimeline;
    public EnemyWave enemyWaveTwo;
    public Animator doorController;
    protected override void executeEvent()
    {
        Debug.Log(text);
        enemyWaveTwoTimeline.Play();
        doorController.SetBool("isOpen",false);
        InvokeRepeating("checkAnim",0,0.1f);
    }
    void checkAnim(){
        if(enemyWaveTwoTimeline.state == PlayState.Paused){
            enemyWaveTwo.SetEnabled(true);
            gameObject.SetActive(false);
            CancelInvoke();
        }
    }
}
