using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSecondDoorStory : StoryItemBase
{

    public Animator DoorController;
    public EnemyWave enemyWave;

    // Start is called before the first frame update
    protected override void executeEvent()
    {
        DoorController.SetBool("isOpen",true);
        enemyWave.SetEnabled(true);
    }
}
