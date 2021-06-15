using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject player;
    [Header("Slider")]
    public Slider hpSilder;
    public Slider mpAccSilder;
    public Slider mpDecSilder;
    [Header("UseStatus")]
    public GameObject Icon_Control;
    public GameObject Icon_Magic;
    public GameObject Icon_Weapon;

    PlayerProperty playerProperty;
    PlayerController playerController;
    void Start()
    {
        playerProperty = player.GetComponent<PlayerProperty>();
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        hpSilder.value = (float)playerProperty.getPlayHP() / playerProperty.maxHP;
        mpAccSilder.value = (float)playerProperty.getPlayAccMP() / playerProperty.maxAccMP;
        mpDecSilder.value = (float)playerProperty.getPlayDecMP() / playerProperty.maxDecMP;
        
        switch(playerController.currentUseStatus){
            case PlayerController.UseStatus.CONTROL:
                Icon_Control.SetActive(true);
                Icon_Magic.SetActive(false);
                Icon_Weapon.SetActive(false);
            break;
            case PlayerController.UseStatus.MAGIC:
                Icon_Control.SetActive(false);
                Icon_Magic.SetActive(true);
                Icon_Weapon.SetActive(false);
            break;
            case PlayerController.UseStatus.WEAPON:
                Icon_Control.SetActive(false);
                Icon_Magic.SetActive(false);
                Icon_Weapon.SetActive(true);
            break;
        }

    }
}
