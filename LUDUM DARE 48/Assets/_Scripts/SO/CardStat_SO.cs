using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Stats", menuName = "Stats/Card")]
public class CardStat_SO : statsBase
{
    [Space]
    public string CardName;

    [Space]
    public CardType type;
    [Space]
    public Sprite CardSprite;
    public Sprite BGSprite;

    [Space]
    public int score;
}

public class statsBase: ScriptableObject
{
    public int MaxHealth;
    public int CurrentHealth;
    public int Damage;

    #region statModifiers
    public void IncreaseHealth(int amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
    }
    public void DecreaseHealth(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth < 0 ) CurrentHealth = 0;
    }

    public void IncreaseDamage(int amount)
    {
        Damage += amount;
    }
    public void DecreaseDamage(int amount)
    {
        Damage -= amount;
        if (Damage < 0) Damage = 0;
    }
    #endregion

}

public enum CardType
{
    Enemy,
    potion,
    Gate,
}