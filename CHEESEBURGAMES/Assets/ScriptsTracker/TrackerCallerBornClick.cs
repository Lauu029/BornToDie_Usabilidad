using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackerCallerBornClick : MonoBehaviour
{
    public void sendEvent()
    {
        Debug.Log("born");
        ClickBornEvent click_event = Tracker.Instance.GetEventFactory().GetClickBorn();
        click_event.CurrentLevel = GameManager.instance.currentLevel;
        Tracker.Instance.TrackEvent(click_event);
    }
}
