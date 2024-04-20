using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackerCallerGoClick : MonoBehaviour
{
    public void sendEvent()
    {
        Debug.Log("GO");
        ClickGoEvent click_event = Tracker.Instance.GetEventFactory().GetClickGo();
        click_event.CurrentLevel = GameManager.instance.currentLevel;
        Tracker.Instance.TrackEvent(click_event);
    }
}
