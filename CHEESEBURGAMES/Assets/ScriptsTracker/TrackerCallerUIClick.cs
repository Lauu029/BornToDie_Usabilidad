using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackerCallerUIClick : MonoBehaviour
{
    public void sendEvent()
    {
        ClickUIRabbitEvent click_event = Tracker.Instance.GetEventFactory().GetClickUIRabbit();
        click_event.CurrentLevel = GameManager.instance.currentLevel;
        Tracker.Instance.TrackEvent(click_event);        
    }
}
