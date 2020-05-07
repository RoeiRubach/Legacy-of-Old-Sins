using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Event/Tutorial", order = 2)]
public class GameEvent : ScriptableObject
{
    private List<GameEventSubscriber> subscribers = new List<GameEventSubscriber>();

    public void FireEvent()
    {
        foreach (var subs in subscribers)
        {
            subs.OnEventFire();
        }
    }

    public static GameEvent operator +(GameEvent gameEvent, GameEventSubscriber gameEventSub)
    {
        gameEvent.subscribers.Add(gameEventSub);
        return gameEvent;
    }

    public static GameEvent operator -(GameEvent gameEvent, GameEventSubscriber gameEventSub)
    {
        gameEvent.subscribers.Remove(gameEventSub);
        return gameEvent;
    }
}
