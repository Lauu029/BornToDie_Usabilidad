using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackerCallerLevelEnd : MonoBehaviour
{
    public void sendEvent()
    {
        EndLevelEvent click_event = Tracker.Instance.GetEventFactory().GetEndLevel();
        click_event.CurrentLevel = GameManager.instance.currentLevel;
        Tracker.Instance.TrackEvent(click_event);
    }
}
