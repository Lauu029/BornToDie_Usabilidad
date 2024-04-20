using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TrackerCallerSpawnedClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (GetComponent<BoxCollider2D>().OverlapPoint(mousePos))
        {
            sendEvent();
        }
    }
    public void sendEvent()
    {
        ClickSpawnedRabbitEvent click_event = Tracker.Instance.GetEventFactory().GetClickSpawnedRabbit();
        click_event.CurrentLevel = GameManager.instance.currentLevel;
        Tracker.Instance.TrackEvent(click_event);
    }
}
