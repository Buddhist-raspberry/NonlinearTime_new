using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class provides all the data you need to control and change gameplay.
/// </summary>
[Serializable]
public class StorySystem
{
    static StorySystem instance=null;

    HashSet<string> storyItems = new HashSet<string>();

    public static StorySystem getInstance()
    {
        if (instance == null)
        {
            Debug.Log("创建剧情系统实例");
            instance=new StorySystem();
        }
        return instance;
    }

    public void RegisterStoryItem(string ID)
    {
        storyItems.Add(ID);
    }

    public bool HasSeenStoryItem(string ID)
    {
        Debug.Log(ID);
        return storyItems.Contains(ID);
    }

}