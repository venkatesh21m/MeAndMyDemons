using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public TMP_Text CardName;
    public TMP_Text ItsHealth;
    public TMP_Text Damage;
    [Space]
    public float postionOffset;

   
    public void SetDetails(CardStat_SO cardData)
    {
        CardName.text = cardData.CardName;
        switch (cardData.type)
        {
            case CardType.Enemy:
                Damage.text = "Deals " + cardData.Damage + " damage on you";
                Damage.gameObject.SetActive(true);
                ItsHealth.text = "can be killed with " + cardData.CurrentHealth + " Damage";
                GetComponent<Image>().color = Color.red;
                break;
            case CardType.potion:
                ItsHealth.text = "Gives " + cardData.CurrentHealth + " Health to you";
                Damage.gameObject.SetActive(false);
                GetComponent<Image>().color = Color.green;
                break;
            case CardType.Gate:
                ItsHealth.text = "let's you go deeper";
                Damage.gameObject.SetActive(false);
                GetComponent<Image>().color = Color.white;
                break;
            default:
                break;
        }
    }

    public void SetPosition(Vector3 postion)
    {
        transform.position = Camera.main.WorldToScreenPoint(postion + Vector3.up * postionOffset);
    }

}
