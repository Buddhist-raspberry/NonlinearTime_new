using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.Sprites;
public class BGM:MonoBehaviour
{
  public GameObject btnObj;//定义按钮
  public Sprite stop; //定义待用的按钮图标
  public Sprite play;
  public Button btn; //声明按钮
  bool isplay = false; //是否播放
  void Start(){}
  void Update()
  {
    AudioSource bgm = gameObject.GetComponent <AudioSource>();
    btn = btnObj.GetComponent <Button>();
    btn.onClick.AddListener(delegate()
    {
      isplay =!isplay;
      if(isplay)
      {//改变按钮图标
        btn.GetComponent <Image>().sprite = stop;
        bgm.Play();
        bgm.time = 0;
      }
      else
      {
        btn.GetComponent <Image>().sprite = play;
        bgm.Stop();
        bgm.time = 0;
      }
    });
  }
}

