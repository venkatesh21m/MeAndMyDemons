using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events
{
    public class CardClickEvent : UnityEvent<CardStat_SO> { };
    public class CardHoverEvent : UnityEvent<CardStat_SO,Vector3> { };
    
    public class TriggerEvent : UnityEvent { };
}


public static class EventManager
{
    public static Events.CardClickEvent onEnemyCardClickedEvent = new Events.CardClickEvent();
    public static Events.CardClickEvent onPotionCardClickedEvent = new Events.CardClickEvent();
    public static Events.TriggerEvent OnGateCardclickedEvent = new Events.TriggerEvent();

    public static Events.CardHoverEvent OnCardHoverEvent = new Events.CardHoverEvent();
    public static Events.TriggerEvent onHoverExitEvent = new Events.TriggerEvent();

    public static Events.TriggerEvent GameOVerEvent = new Events.TriggerEvent();
}