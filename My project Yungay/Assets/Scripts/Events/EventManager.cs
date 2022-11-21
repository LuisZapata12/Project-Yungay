using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    public event Action cutRopeEvent;
    public event Action<int> brokeWindowEvent;
    public event Action<int> useLeverEvent;
    public event Action<int> kathyaVoiceEvent;
    // Start is called before the first frame update
    void Awake()
    {
        if (current == null)
        {
            current = this;
        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void StartCuttingRopeEvent()
    {
        //if (cutRopeEvent != null)
        //{
        //    cutRopeEvent();
        //}
        cutRopeEvent?.Invoke();
    }

    public void StartBreakingWindowEvent(int id)
    {
        brokeWindowEvent?.Invoke(id);
    }

    public void StartUseLeverEvent(int id)
    {
        useLeverEvent?.Invoke(id);
    }

    public void StartkathyaVoiceEvent(int id)
    {
        kathyaVoiceEvent?.Invoke(id);
    }
}
