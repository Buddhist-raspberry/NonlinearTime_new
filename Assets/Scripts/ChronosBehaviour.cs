using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class ChronosBehaviour : MonoBehaviour
{
    public Timeline time
    {
        get
        {
            return GetComponent<Timeline>();
        }
    }
    public LocalClock localClock
    {
        get
        {
            return GetComponent<LocalClock>();
        }
    }

    public void _setSpeed(float _timeScale)
    {
        if (_timeScale == 1) _RecoverSpeed();
        if (_timeScale > 1) _SpeedUp(_timeScale);
        else if (_timeScale < 1 && _timeScale >= 0) _SpeedDown(_timeScale);
        else if (_timeScale < 0) _InvertSpeed();
    }

    //加速
    protected void _SpeedUp(float _timeScale){
        if(_timeScale>1.0f&&_timeScale<=11.0f){
            localClock.localTimeScale = _timeScale;
        }
    }
    //减速
    protected void _SpeedDown(float _timeScale){
        if(_timeScale>= 0&&_timeScale<1.0f){
            localClock.localTimeScale = _timeScale;
        }
    }
    //反向速度
    protected void _InvertSpeed(){
        localClock.localTimeScale = -1.0f;
    }
    //恢复正常速度
    public void _RecoverSpeed(){
        localClock.localTimeScale = 1;
    }
    public float GetLocalTimeScale(){
        return localClock.localTimeScale;
    }
}
