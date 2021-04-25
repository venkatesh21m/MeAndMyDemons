using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerInfoSetter : MonoBehaviour
{

   
    [Space]
    public TMP_Text PlayerName;
    public Slider HealthSlider;
    public TMP_Text Health;
    public TMP_Text Damage;
    public TMP_Text skips;
    public TMP_Text score;
    public TMP_Text enemyKilled;
    public TMP_Text currentLevel;

    public void SetPLayerInfo(CharacterStat_SO playerdata)
    {
        HealthSlider.maxValue = playerdata.MaxHealth;
        setPlayerHealth(playerdata.CurrentHealth);
        SetPLayerDamage(playerdata.Damage);
        SetPLayerskips(playerdata.skips);
    }

    private void Start()
    {
        SetEnemiesKilled(0);
        SetScore(0);
        SetCurrentLeve(0);
    }

    void setPlayerHealth(int health)
    {
        Health.text = health.ToString();
        HealthSlider.value = health;
    }

    void SetPLayerDamage(int damage)
    {
        Damage.text = damage.ToString();
    }

    public void SetPLayerskips(int Skips)
    {
        skips.text = Skips.ToString();
    }

    public void SetEnemiesKilled(int number)
    {
        enemyKilled.text = number.ToString();
    }
    public void SetScore(int currentscore)
    {
        score.text = currentscore.ToString();
    }

    public void SetCurrentLeve(int leve)
    {
        currentLevel.text = leve.ToString();
    }

}
