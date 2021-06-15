using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        GameLogic.instance.GameStart();
    }

}
