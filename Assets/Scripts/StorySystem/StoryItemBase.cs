using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// This class will trigger a text message when the player enters the trigger,
/// and optionally start a cutscene.
/// </summary>
public abstract class StoryItemBase : MonoBehaviour, ISerializationCallbackReceiver
{
    public string ID;
    [Multiline]
    public string text = "There is no story to be found here.";     //剧情说明

    public bool disableWhenDiscovered = true;      //触发后是否隐藏
    public bool executeByTrigger=true;              //通过碰撞触发事件
    public float afterEventTime = 0;
    protected bool hasTriggered = false;

    public HashSet<StoryItemBase> requiredStoryItems;       //前置事件
    public List<StoryItemBase> afterStoryItems;         //后续事件集合
    [System.NonSerialized] public HashSet<StoryItemBase> dependentStoryItems = new HashSet<StoryItemBase>();

    [SerializeField] StoryItemBase[] _requiredStoryItems;

    protected StorySystem storySystem =null;


    void OnEnable()
    {
        if (ID == string.Empty && text != null)
        {
            ID = $"SI:{text.GetHashCode()}";
        }
    }

    void Awake()
    {
        ConnectRelations();
        storySystem=StorySystem.getInstance();
    }

    void ConnectRelations()
    {
        foreach (var i in requiredStoryItems)
        {
            i.dependentStoryItems.Add(this);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "Player")  return;

        if(executeByTrigger&&!hasTriggered)    TriggerEvent();
    }

    void TriggerAfterEvent()        //触发后续事件
    {
        foreach(StoryItemBase storyItem in afterStoryItems)
        {
            storyItem.TriggerEvent();
        }
    }

    public void TriggerEvent()      //触发事件
    {
        foreach (var requiredStoryItem in requiredStoryItems)
            if (requiredStoryItem != null)
                if (!storySystem.HasSeenStoryItem(requiredStoryItem.ID))
                    return;

        executeEvent();
        if (ID != string.Empty)
            storySystem .RegisterStoryItem(ID);
        if (disableWhenDiscovered) gameObject.SetActive(false);
        Invoke("TriggerAfterEvent",afterEventTime);
        // TriggerAfterEvent();
    }

    protected abstract void executeEvent();       //执行事件


    public void OnBeforeSerialize()
    {
        if(requiredStoryItems != null)
            _requiredStoryItems = requiredStoryItems.ToArray();
    }

    public void OnAfterDeserialize()
    {
        requiredStoryItems = new HashSet<StoryItemBase>();
        if (_requiredStoryItems != null)
            foreach (var i in _requiredStoryItems) requiredStoryItems.Add(i);
    }

}