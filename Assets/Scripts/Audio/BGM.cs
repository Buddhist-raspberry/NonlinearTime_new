using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BGM:MonoBehaviour
{
  public static BGM instance { get; protected set; }
  public AudioClip[] audios;
  void Start(){
    this.GetComponent<AudioSource>().clip = audios[0];
    this.GetComponent<AudioSource>().Play();
  }
  public void stop(){
    this.GetComponent<AudioSource>().clip = audios[0];
    this.GetComponent<AudioSource>().Play();
  }
  public void gameStart(){
    this.GetComponent<AudioSource>().clip = audios[1];
    this.GetComponent<AudioSource>().Play();
  }
  public void win(){
    this.GetComponent<AudioSource>().clip = audios[2];
    this.GetComponent<AudioSource>().Play();
  }
  public void fail(){
    this.GetComponent<AudioSource>().clip = audios[3];
    this.GetComponent<AudioSource>().Play();
    Debug.Log("fail bgm");
  }
  public void introduce(){
    this.GetComponent<AudioSource>().clip = audios[4];
    this.GetComponent<AudioSource>().Play();
  }
  public void mainMenu(){
    this.GetComponent<AudioSource>().clip = audios[5];
    this.GetComponent<AudioSource>().Play();
  }
}

