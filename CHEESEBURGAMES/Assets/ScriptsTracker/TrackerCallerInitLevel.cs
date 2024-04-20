using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackerCallerInitLevel : MonoBehaviour
{
    public void sendEvent()
    {
        InitLevelEvent click_event = Tracker.Instance.GetEventFactory().GetInitLevel();
        click_event.CurrentLevel = GameManager.instance.currentLevel;
        Tracker.Instance.TrackEvent(click_event);
    }
}
