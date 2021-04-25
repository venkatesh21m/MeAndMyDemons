using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    public CharacterStat_SO playerDefinition;
    PlayerInfoSetter playerStatSetter;
    [SerializeField] PopUp popUp;
    public int Damage
    {
        get
        {
            return playerDefinition.Damage;
        }
    }

    public int Health
    {
        get
        {
            return playerDefinition.CurrentHealth;
        }
    }
    public int Skip
    {
        get
        {
            return playerDefinition.skips;
        }
        set
        {
            playerDefinition.skips = value;
            playerStatSetter.SetPLayerskips(playerDefinition.skips);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerDefinition = Instantiate(playerDefinition);

        playerStatSetter = GetComponent<PlayerInfoSetter>();
        playerStatSetter.SetPLayerInfo(playerDefinition);

        EventManager.onEnemyCardClickedEvent.AddListener(OnEnemyCardClicked);
        EventManager.onPotionCardClickedEvent.AddListener(OnPotionCardClicked);
        EventManager.OnCardHoverEvent.AddListener(HandleOnHoverOnCard);
        EventManager.onHoverExitEvent.AddListener(HandleOnHoverExitCard);
    }

    private void HandleOnHoverExitCard()
    {
        popUp.gameObject.SetActive(false);
    }

    private void HandleOnHoverOnCard(CardStat_SO cardData,Vector3 postion)
    {
        popUp.SetDetails(cardData);
        postion.y = 2;
        popUp.SetPosition(postion);
        popUp.gameObject.SetActive(true);
    }

    void OnEnemyCardClicked(CardStat_SO cardData)
    {
        playerDefinition.DecreaseHealth(cardData.Damage);

        playerStatSetter.SetPLayerInfo(playerDefinition);

        if (playerDefinition.CurrentHealth <= 0)
        {
            EventManager.GameOVerEvent.Invoke();
        }
    }

    void OnPotionCardClicked(CardStat_SO cardData)
    {
        playerDefinition.IncreaseHealth(cardData.CurrentHealth);

        playerStatSetter.SetPLayerInfo(playerDefinition);
    }

    internal void SKipUsed()
    {
        playerDefinition.DecreaseSkips(1);
        playerStatSetter.SetPLayerskips(playerDefinition.skips);
    }

    public void enemyKilled(int score, int enemiesKilled)
    {
        playerStatSetter.SetScore(score);
        playerStatSetter.SetEnemiesKilled(enemiesKilled);
    }

    public void LevedUp(int levelUp)
    {
        playerStatSetter.SetCurrentLeve(levelUp);
    }

}
