using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Magic : MonoBehaviour
{
    [Header("Bools")]
    public bool active = true;
    public bool isPlacing = true;

    [Header("Magic")]
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    [Header("Value")]
    public float maxPlacedDistance = 10.0f;
    public float speedDownTimeScale = 0.3f;     //减速程度
    public float usableTime = 3.0f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlacing){
            RaycastHit hit;
            Ray ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
            if(Physics.Raycast(ray, out hit,5, groundLayer))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red); 
                transform.position = hit.point;
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (1 << other.gameObject.layer == enemyLayer.value)
        {
            float currentTimeScale = other.GetComponent<Enemy>().GetLocalTimeScale();
            if(currentTimeScale<0){
                currentTimeScale = 0;
            }
            else{
                currentTimeScale*=speedDownTimeScale;
            }
            other.GetComponent<Enemy>().SpeedDown(currentTimeScale);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (1 << other.gameObject.layer == enemyLayer.value)
        {
            other.GetComponent<Enemy>().RecoverTimeScale();
        }
    }
    public void SetPlacing(bool _isPlacing){
        isPlacing = _isPlacing;
        if(!isPlacing){
            Invoke("destroySelf",usableTime);
        }
    }
    void destroySelf(){
        Destroy(gameObject);
    }
}
