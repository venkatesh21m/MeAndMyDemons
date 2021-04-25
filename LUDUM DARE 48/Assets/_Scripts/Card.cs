using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Card : MonoBehaviour
{
    [Space]
    public CardStat_SO cardDefinition;

    CardInfoSetter CardInfoSetter;
    bool canClickOnCard = true;
    [HideInInspector] public bool notActive = false;
    private void Start()
    {
        cardDefinition = Instantiate(cardDefinition);

        CardInfoSetter = GetComponent<CardInfoSetter>();
        CardInfoSetter.SetCardInfo(cardDefinition);
       
        EventManager.GameOVerEvent.AddListener(onGameOver);
    }

    public void setCardDefinition(CardStat_SO carddefinition)
    {
        cardDefinition = carddefinition;
        if (CardInfoSetter)
            CardInfoSetter.SetCardInfo(cardDefinition);
        else
        {
            CardInfoSetter = GetComponent<CardInfoSetter>();
            CardInfoSetter.SetCardInfo(cardDefinition);
        }
    }

    private void onGameOver()
    {
        canClickOnCard = false;
    }

    private void OnMouseEnter()
    {
        transform.DOScale(Vector3.one * 1.2f, 0.25f);
        EventManager.OnCardHoverEvent.Invoke(cardDefinition,transform.position);
    }

    private void OnMouseExit()
    {
        transform.DOScale(Vector3.one, 0.25f);
        EventManager.onHoverExitEvent.Invoke();

    }

    private void OnMouseDown()
    {
        if (!canClickOnCard) return;
        if (notActive) return;
        if (Player.Instance.Health <= 0) return;

        EventManager.onHoverExitEvent.Invoke();
        
        switch (cardDefinition.type)
        {
            case CardType.Enemy:
                cardDefinition.DecreaseHealth(Player.Instance.Damage);
                EventManager.onEnemyCardClickedEvent.Invoke(cardDefinition);
                break;
            case CardType.potion:
                EventManager.onPotionCardClickedEvent.Invoke(cardDefinition);
                break;
            case CardType.Gate:
                EventManager.OnGateCardclickedEvent.Invoke();
                break;
            default:
                break;
        }

        if (cardDefinition.type == CardType.Enemy)
        {
            CardInfoSetter.SetCardInfo(cardDefinition);
            
            if (cardDefinition.CurrentHealth <= 0)
            {
                transform.DOPunchScale(Vector3.one * 2f, 0.25f);
                Destroy(gameObject, .25f);
            }
        }
           
        if(cardDefinition.type != CardType.Enemy)
        {
            if (cardDefinition.type == CardType.Gate)
            {
                transform.DOPunchScale(Vector3.one * 3f, 5f, elasticity: 5);
                Destroy(gameObject, 2f);
            }
            else
            {
                transform.DOPunchScale(Vector3.one * 2f, 0.25f);
                Destroy(gameObject, .25f);
            }

        }
    }
}
