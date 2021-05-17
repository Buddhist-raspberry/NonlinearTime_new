using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gun : Weapon
{
    public int bulletAmount = 6;

    protected override void Init(){
        weaponType = WeaponType.GUN;
    }

    public void Shoot(Vector3 pos,Quaternion rot, bool isEnemy)
    {
        if (reloading)
            return;

        if (bulletAmount <= 0)
            return;

        if(!playerController.weapon == this)
            bulletAmount--;

        GameObject bullet = Instantiate(playerController.bulletPrefab, pos, rot);

        if (GetComponentInChildren<ParticleSystem>() != null)
            GetComponentInChildren<ParticleSystem>().Play();

        if(playerController.weapon == this)
            StartCoroutine(Reload());

        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .01f, 10, 90, false, true).SetUpdate(true);

        if(playerController.weapon == this)
            transform.DOLocalMoveZ(-.1f, .05f).OnComplete(()=>transform.DOLocalMoveZ(0,.2f));
    }

}
