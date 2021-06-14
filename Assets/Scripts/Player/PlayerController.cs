using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public static PlayerController instance;
    [Header("Status")]
    public float charge;
    public bool canShoot = true;
    public bool canMagic = true;
    public bool isEnabled = false;
    public bool action;
    public enum UseStatus{
        CONTROL,WEAPON,MAGIC
    };
    private UseStatus currentUseStatus = UseStatus.WEAPON;

    private int MagicLeft = 0;

    [Header("Control")]
    public GameObject controlBall;
    
    [Header("Magic")]
    public GameObject magicBar;
    public GameObject magicPrefab;
    private GameObject ownMagic;
    
    [Space]
    [Header("Weapon")]
    public Weapon weapon;
    public Transform weaponHolder;
    public LayerMask weaponLayer;
    public LayerMask capsulaLayer;
    public LayerMask glowLayer;
    private Transform _selection;

    [Space]
    [Header("UI")]
    public Image indicator;

    [Space]
    [Header("Prefabs")]
    public GameObject bulletPrefab;




    private void Awake()
    {
        instance = this;
        if (weaponHolder.GetComponentInChildren<Weapon>() != null)
            weapon = weaponHolder.GetComponentInChildren<Weapon>();
        _selection = null;
    }
    // Update is called once per frame
    void Update()
    {
        if(!isEnabled) return;
        //使用武器
        if (CheckStatus(UseStatus.WEAPON)&&Input.GetMouseButtonDown(0))
        {
            StopCoroutine(ActionE(.03f));
            StartCoroutine(ActionE(.03f));
            if (weapon != null){
                switch(weapon.weaponType){
                    case Weapon.WeaponType.GUN: 
                        if(canShoot){
                            ((Gun)weapon).Shoot(SpawnPos(), 
                                Camera.main.transform.rotation, false);
                        }
                        break;
                    default:
                        weapon.Throw();
                        weapon = null;
                        break;
                }
            }
        }
        //抛弃武器
        if (CheckStatus(UseStatus.WEAPON)&&Input.GetMouseButtonDown(1))
        {
            StopCoroutine(ActionE(.4f));
            StartCoroutine(ActionE(.4f));

            if(weapon != null)
            {
                weapon.Release();
                weapon = null;
            }
        }
        //放置光环
        if (CheckStatus(UseStatus.MAGIC)&&canMagic&&Input.GetMouseButtonDown(0))
        {
            if(ownMagic!=null){
                ownMagic.GetComponent<Magic>().SetPlacing(false);
                ownMagic = null;
            }
        }

        
        RaycastHit hit;
        bool keepSlected = false;
        //光标选择
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, glowLayer))
        {
            if (hit.transform != _selection) {
                if (_selection != null)
                    _selection.GetComponent<Glowing>().Unselected(); 
            _selection = hit.transform;
            _selection.GetComponent<Glowing>().Selected();
            }
            
            keepSlected = true;

            if (CheckStatus(UseStatus.CONTROL)&&Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                ChronosBehaviour t_chronosBehaviour = hit.transform.GetComponent<ChronosBehaviour>();
                float currentTimeScale = t_chronosBehaviour.GetLocalTimeScale();
                // float _factor;
                // if (Input.GetAxis("Mouse ScrollWheel") <= 0) _factor = -1;
                // else _factor = 1;
                float _factor;
                if (Input.GetAxis("Mouse ScrollWheel") <= 0) _factor = -1;
                else _factor = 1;

                if (_factor == -1 && PlayerProperty.instance.getPlayDecMP()==0) {
                    _factor = 0;
                    Debug.Log("减速魔法值DecMP为0");
                }
                if (_factor == 1 && PlayerProperty.instance.getPlayAccMP()==0) {
                    _factor = 0;
                    Debug.Log("加速魔法值AccMP为0");
                }

                if (currentTimeScale<=1) currentTimeScale += _factor * 0.1f;
                else currentTimeScale += _factor * 1f;
                

                currentTimeScale = Mathf.Max(currentTimeScale, -1);
                currentTimeScale = Mathf.Min(currentTimeScale, 11);

                Debug.Log(currentTimeScale);

                if (currentTimeScale == 0 && _factor < 0) currentTimeScale = -1;
                if (currentTimeScale < 0 && _factor > 0) currentTimeScale = 0;

                t_chronosBehaviour._setSpeed(currentTimeScale);
                if( _factor>0) PlayerProperty.instance.reduceAccMP(3);
                else PlayerProperty.instance.reduceDecMP(3);
                

            }
        }

        if  (!keepSlected && _selection != null) {
            _selection.GetComponent<Glowing>().Unselected();
            _selection = null;
        }

        //捡起武器
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,5, weaponLayer))
        {
            if (Input.GetKeyDown(KeyCode.E) && weapon == null)
            {
                hit.transform.GetComponent<Weapon>().Pickup();
                ChangeUseStatus(UseStatus.WEAPON);
            }
        }
        // 捡起药丸
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,5, capsulaLayer))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                hit.transform.GetComponent<Capsula>().Pickup();
            }
        }
        //武器加速
        if(CheckStatus(UseStatus.CONTROL)&&Input.GetKeyDown(KeyCode.J) && weapon != null)
        {
            weapon.SpeedUp();
        }

        //监听武器切换
        StatusChangeListener();
    }

    IEnumerator ActionE(float time)
    {
        GlobalTimeController.instance.setAction(true);
        yield return new WaitForSecondsRealtime(.06f);
        GlobalTimeController.instance.setAction(false);
    }

    public void ReloadUI(float time)
    {
        indicator.transform.DORotate(new Vector3(0, 0, 90), time, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(() => indicator.transform.DOPunchScale(Vector3.one / 3, .2f, 10, 1).SetUpdate(true));
    }


    Vector3 SpawnPos()
    {
        return Camera.main.transform.position + (Camera.main.transform.forward * .5f) + (Camera.main.transform.up * -.02f);
    }
    
    void StatusChangeListener()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeUseStatus(UseStatus.CONTROL);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeUseStatus(UseStatus.WEAPON);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(MagicLeft<=0) return;
            ChangeUseStatus(UseStatus.MAGIC);
            return;
        }
    }

    public void AddMagic(int num){
        MagicLeft += num;
        MagicLeft = Mathf.Min(0,MagicLeft);
        MagicLeft = Mathf.Max(1,MagicLeft);
        return;
    }

    public void ChangeUseStatus(UseStatus status)
    {
        
        if (status == UseStatus.CONTROL)
        {
            currentUseStatus = UseStatus.CONTROL;
            controlBall.SetActive(true);
            magicBar.SetActive(false);
            if (weapon != null)
                weapon.gameObject.SetActive(false);
            return;
        }
        if (status == UseStatus.WEAPON)
        {
            currentUseStatus = UseStatus.WEAPON;
            controlBall.SetActive(false);
            magicBar.SetActive(false);
            if (weapon != null)
                weapon.gameObject.SetActive(true);
            return;
        }
        if (status == UseStatus.MAGIC&&canMagic)
        {
            currentUseStatus = UseStatus.MAGIC;
            controlBall.SetActive(false);
            magicBar.SetActive(true);
            if (weapon != null)
                weapon.gameObject.SetActive(false);
            ownMagic = GameObject.Instantiate(magicPrefab);
            ownMagic.GetComponent<Magic>().SetPlacing(true);
            return;
        }
    }

    bool CheckStatus(UseStatus status)
    {
        return status == currentUseStatus;    
    }

    
}
