using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class GlobalTimeController : MonoBehaviour
{

    public static GlobalTimeController instance;

    public bool action;

    public bool isEnabled=false;

    private Clock rootClock;


    void Awake()
    {
        instance = this;
    }
    void Start() {
        rootClock = Timekeeper.instance.Clock("Root");
        if(isEnabled){
            Time.timeScale  = 1;
            rootClock.localTimeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnabled){
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            float time = (x != 0 || y != 0) ? 1f : .03f;
            float lerpTime = (x != 0 || y != 0) ? .05f : .5f;

            time = action ? 1 : time;
            lerpTime = action ? .1f : lerpTime;


            Time.timeScale = Mathf.Lerp(Time.timeScale, time, lerpTime);
            rootClock.localTimeScale= Mathf.Lerp(Time.timeScale, time, lerpTime);
        }



    }
    public void SetEnabled(){
        isEnabled = true;
    }
    public void Pause(){

    }

    public void setAction(bool _action){
        action = _action;
    }


}
