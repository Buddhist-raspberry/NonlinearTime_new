using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    public static MagicController instance;
    [Header("Prefab")]
    public GameObject magicPrefab;
    [Header("Layer")]
    public LayerMask magicLayer;
    private bool canMagic = true;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)&&canMagic)
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,10, magicLayer))
            {
                canMagic = false;
            }
        }
    }
}
