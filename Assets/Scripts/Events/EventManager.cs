using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Events
{
    HORIZONTAL_INPUT, ///Double
    VERTICAL_INPUT, /// Double
    JUMP_INPUT, ///Null
    PLAYER_IN_AIR, ///Float
}


public class EventManager
{

    ///O primeiro parametro é o objeto que triggou o evento e o segundo argumento generico
    private static Dictionary<Events, List<UnityAction<object, object>>> events;

    public static void AddListener(Events eventTrigger, UnityAction<object, object> callback)
    {
        if (events == null)
            events = new Dictionary<Events, List<UnityAction<object, object>>>();

        if (!events.ContainsKey(eventTrigger))
            events.Add(eventTrigger, new List<UnityAction<object, object>>());

        events[eventTrigger].Add(callback);
    }
    public static void RemoveListener(Events eventTrigger, UnityAction<object, object> callback)
    {
        if (events != null && events.ContainsKey(eventTrigger))
            events[eventTrigger].Remove(callback);
        else
            Debug.Log("Não há mais ouvintes, mas tentando remover ouvinte do evento: " + eventTrigger.ToString());
    }

    public static void OnEvent(object sender, object args, Events eventTrigger)
    {
        if (events != null && events.ContainsKey(eventTrigger))
        {
            foreach (var callback in events[eventTrigger])
            {
                callback(sender, args);
            }
        }
    }
}
