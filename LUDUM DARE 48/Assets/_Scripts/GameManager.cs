using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameObject CardPrefab;

    [Header("CardDatas")]
    public List<CardStat_SO> EnemycarddataLibrary;
    public List<CardStat_SO> PotioncarddataLibrary;
    public CardStat_SO GateCard;


    public List<Card> currentleftActiveCards = new List<Card>();
    public List<Card> currentrightActiveCards = new List<Card>();


    [Header("UI")]
    public GameObject GameCanvas;
    public GameObject GameOVerCanvas;
    public GameObject MainMenu;
    public GameObject LevelUpCanvas;
    public GameObject SkipButtons;

    int currentLevel = 0;
    int currentScore = 0;
    int enemiesKilled = 0;
    int currentSkips = 3;
    public int Score
    {
        get
        {
            return currentScore;
        }
    }

    public int Level
    {
        get
        {
            return currentLevel;
        }
    }

    private void Start()
    {
       
        EventManager.onEnemyCardClickedEvent.AddListener(OnCardClicked);
        EventManager.onPotionCardClickedEvent.AddListener(OnCardClicked);
        EventManager.OnGateCardclickedEvent.AddListener(OnGateCardCkicked);
        EventManager.GameOVerEvent.AddListener(onGameOver);
    }

    private void OnGateCardCkicked()
    {
        StartCoroutine(StartGoingDeep());
    }

    private IEnumerator StartGoingDeep()
    {
        currentLevel++;
        yield return new WaitForSeconds(0.25f);
        LevelUpCanvas.SetActive(true);
        ClearCardsFromGame(currentleftActiveCards);
        ClearCardsFromGame(currentrightActiveCards);
        currentleftActiveCards.Clear();
        currentrightActiveCards.Clear();
        yield return new WaitForSeconds(0.5f);
        LevelUpCanvas.SetActive(false);
        Player.Instance.Skip = currentSkips;
        Player.Instance.LevedUp(currentLevel);
        GameStart();
    }

    public void GameStart()
    {
        MainMenu.SetActive(false);
        GameCanvas.SetActive(true);
        GameOVerCanvas.SetActive(false);
        switch (currentLevel)
        {
            case 0:
                GameObject tempCard = Instantiate(CardPrefab);
                tempCard.GetComponent<Card>().setCardDefinition(GateCard);
                tempCard.transform.position = Vector3.zero;
                currentleftActiveCards.Add(tempCard.GetComponent<Card>());
                break;
            case 1:
                CreateCards(2);
                break; 
            case 2:
                CreateCards(4);
               
                break;
            case 3:
                CreateCards(6);
                break;
            case 4:
                CreateCards(8);
                break;
            case 5:
                CreateCards(8);
                break;
            case 6:
                CreateCards(10);
                break;
            case 7:
                CreateCards(16);
                break;
            case 8:
                CreateCards(20);
                currentSkips++;
                break;
            case 9:
                CreateCards(24);
                break;
            case 10:
                CreateCards(30);
                break;
            default:
                CreateCards(currentLevel*3);
                currentSkips++;
                break;
        }
        if (currentLevel > 3)
        {
            SkipButtons.SetActive(true);
        }
       // CreateCards(20);
    }

    private void onGameOver()
    {
        ClearCardsFromGame(currentleftActiveCards);
        ClearCardsFromGame(currentrightActiveCards);

        currentleftActiveCards.Clear();
        currentrightActiveCards.Clear();
        SkipButtons.SetActive(false);
        if(currentScore > (PlayerPrefs.HasKey("HighScore")?PlayerPrefs.GetInt("HighScore") : 0))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
        GameOVerCanvas.GetComponent<GameOverInfoSetter>().SetGameOverDetatils(Level,enemiesKilled, Score);

        GameCanvas.SetActive(false);
        GameOVerCanvas.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ClearCardsFromGame(List<Card> Cards)
    {
        for (int i = Cards.Count-1; i >= 0; i--)
        {
            Destroy(Cards[i].gameObject);
        }

        Cards.Clear();
    }

    private void OnCardClicked(CardStat_SO card)
    {
        currentScore += card.score;
        if (card.type == CardType.Enemy && card.CurrentHealth > 0) return;

        if (card.type == CardType.Enemy)
        {
            enemiesKilled++;
            Player.Instance.enemyKilled(currentScore, enemiesKilled);
        }
        if (currentleftActiveCards.Count > 0)
        {
            if (currentleftActiveCards[0].cardDefinition == card)
            {
                currentleftActiveCards.RemoveAt(0);
                StartCoroutine(MoveForwardinRow(currentleftActiveCards, 2));
                return;
            }
        }
        if (currentrightActiveCards.Count > 0)
        {
            if (currentrightActiveCards[0].cardDefinition == card)
            {
                currentrightActiveCards.RemoveAt(0);
                StartCoroutine(MoveForwardinRow(currentrightActiveCards, 1));
                return;
            }
        }
    }

    void CreateCards(int numberofCards)
    {
        int direction = 1;
        for (int j = 0; j < 2; j++)
        {
            Vector3 postion = new Vector3(0,0,0);
            for (int i = 0; i < numberofCards/2; i++)
            {
                GameObject tempCard = Instantiate(CardPrefab);

                bool isGateCard = UnityEngine.Random.value > 0.9f;
                bool ispotioncard = UnityEngine.Random.value > 0.75f;

                CardStat_SO cardData = ispotioncard ? PotioncarddataLibrary[UnityEngine.Random.Range(0, PotioncarddataLibrary.Count)] : EnemycarddataLibrary[UnityEngine.Random.Range(0, EnemycarddataLibrary.Count)];
                if (isGateCard) cardData = GateCard;
                cardData = Instantiate(cardData);
                //change card data values based on current level

                tempCard.GetComponent<Card>().setCardDefinition(cardData);

                if (direction == 1)
                    postion.x += 2f;
                else
                    postion.x -= 2f;
               
                if(i!=0) postion.z += 2;

                tempCard.transform.position = postion;
                if(direction == 1)
                    currentrightActiveCards.Add(tempCard.GetComponent<Card>());
                else
                    currentleftActiveCards.Add(tempCard.GetComponent<Card>());
            }

            GameObject temp1Card = Instantiate(CardPrefab);
            temp1Card.GetComponent<Card>().setCardDefinition(GateCard);

            postion.z += 2;
            if (direction == 1)
            {
                postion.x += 2f;
                temp1Card.transform.position = postion;
                currentrightActiveCards.Add(temp1Card.GetComponent<Card>()); 
            }
            else
            {
                postion.x -= 2f;
                temp1Card.transform.position = postion;
                currentleftActiveCards.Add(temp1Card.GetComponent<Card>());
            }

            direction++;
        }

        SetFirstActive(currentrightActiveCards);
        SetFirstActive(currentleftActiveCards);

    }

    void SetFirstActive(List<Card> gameObjects)
    {
       
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (i != 0)
            {
                SpriteRenderer[] renderes = gameObjects[i].GetComponentsInChildren<SpriteRenderer>();
                foreach (var item in renderes)
                {
                    Color color = item.color;
                    color.a = 0.5f;
                    item.color = color;
                }
                gameObjects[i].notActive = true;
            }
            else
            {
                SpriteRenderer[] renderes = gameObjects[i].GetComponentsInChildren<SpriteRenderer>();
                foreach (var item in renderes)
                {
                    Color color = item.color;
                    color.a = 1;
                    item.color = color;
                }
                gameObjects[i].notActive = false;
            }
        }
    }

    IEnumerator MoveForwardinRow(List<Card> gameObjects, int direction)
    {
        yield return new WaitForSeconds(0.3f);
        Vector3 postion = new Vector3(0, 0, 0);
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (direction == 1)
                postion.x += 2f;
            else
                postion.x -= 2f;

            if (i != 0)
            {
                postion.z += 2;

                SpriteRenderer[] renderes = gameObjects[i].GetComponentsInChildren<SpriteRenderer>();
                foreach (var item in renderes)
                {
                    Color color = item.color;
                    color.a = 0.5f;
                    item.color = color;
                }
                gameObjects[i].notActive = true;
            }
            else
            {
                SpriteRenderer[] renderes = gameObjects[i].GetComponentsInChildren<SpriteRenderer>();
                foreach (var item in renderes)
                {
                    Color color = item.color;
                    color.a = 1;
                    item.color = color;
                }
                gameObjects[i].notActive = false;
            }

            gameObjects[i].transform.DOMove(postion, 0.25f);
        }
    }

    public void RightSkipButtonPressed()
    {
        if (Player.Instance.Skip > 0)
        {
            currentrightActiveCards[0].cardDefinition.CurrentHealth = 0;
            Destroy(currentrightActiveCards[0].gameObject);
            OnCardClicked(currentrightActiveCards[0].cardDefinition);
            Player.Instance.SKipUsed();
        }
        if (Player.Instance.Skip <= 0) SkipButtons.SetActive(false);
    }

    public void LeftSkipButtonPressed()
    {
        if (Player.Instance.Skip > 0)
        {
            currentleftActiveCards[0].cardDefinition.CurrentHealth = 0;
            Destroy(currentleftActiveCards[0].gameObject);
            OnCardClicked(currentleftActiveCards[0].cardDefinition);
            Player.Instance.SKipUsed();
        }
        if (Player.Instance.Skip <= 0) SkipButtons.SetActive(false);
    }
}
