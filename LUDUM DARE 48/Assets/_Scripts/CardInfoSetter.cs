using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardInfoSetter : MonoBehaviour
{
    public TMP_Text CardName;
    public TMP_Text Health;
    public TMP_Text Damage;
    public TMP_Text ActionCost;
    [Space]
    public SpriteRenderer CardImage;
    public SpriteRenderer CardBorder;

 
    public void SetCardInfo(CardStat_SO cardData)
    {
        SetCardName(cardData.CardName);
        
        switch (cardData.type)
        {
            case CardType.Enemy:
                SetHealth(cardData.CurrentHealth);
                SetDamge(cardData.Damage);
                SetColor(Color.red);
                break;
            case CardType.potion:
                SetHealth(cardData.CurrentHealth);
                Damage.gameObject.SetActive(false);
                SetColor(Color.green);
                break;
            case CardType.Gate:
                Damage.gameObject.SetActive(false);
                ActionCost.gameObject.SetActive(false);
                Health.gameObject.SetActive(false);
                SetColor(Color.white);
                break;
            default:
                break;
        }
        
        SetCArdImage(cardData.CardSprite);
        SetCardBorder(cardData.BGSprite);
    }


    void SetColor(Color color)
    {
        SpriteRenderer[] renderers = transform.GetComponentsInChildren<SpriteRenderer>();
        foreach (var item in renderers)
        {
            item.color = color;
        }

    }

    void SetCardName(string cardName)
    {
        CardName.text = cardName;
    }

    void SetHealth(int health)
    {
        Health.text = "Health : " + health.ToString();
    }

    void SetDamge(int damage)
    {
        Damage.text = "Damage : " + damage.ToString();
    }
    void SetActionCost(int cost)
    {
        ActionCost.text = cost.ToString();
    }
    void SetCArdImage(Sprite image)
    {
        CardImage.sprite = image;
    }
    void SetCardBorder(Sprite image)
    {
        CardBorder.sprite = image;
    }
}
