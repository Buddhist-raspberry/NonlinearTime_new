using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Magic : MonoBehaviour
{
    [Header("Bools")]
    public bool active = true;

    [Header("Values")]
    public float scaleSpeed = 0.1f;
    public float minScale = 0.5f;
    public float maxScale = 3.0f;
    [Header("Magic")]
    public LayerMask magicLayer;

    private float originalScale;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale =transform.localScale;
        if( Input.GetAxis("Mouse ScrollWheel")<0){
            scale *=(1.0f-scaleSpeed);
        }
        else if(Input.GetAxis("Mouse ScrollWheel")>0){
            scale *=(1.0f+scaleSpeed);
        }
        if(scale.x <minScale*originalScale){
            scale = Vector3.one*minScale*originalScale;
        }
        else if(scale.x>maxScale*originalScale){
            scale = Vector3.one*maxScale*originalScale;
        }
        transform.DOScale(scale,0.1f);

        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
        if(Physics.Raycast(ray, out hit,5, magicLayer))
        {
            // Debug.Log(hit.transform.name);
            Debug.DrawLine(ray.origin, hit.point, Color.red); 
            transform.position = hit.point;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if ( 1 << other.gameObject.layer == LayerMask.GetMask("Enemy"))
        {
            Debug.Log(other.gameObject.name);
            other.GetComponent<Enemy>().SpeedDown();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( 1 << other.gameObject.layer == LayerMask.GetMask("Enemy"))
        {
            other.GetComponent<Enemy>().RecoverTimeScale();
        }
    }
}
