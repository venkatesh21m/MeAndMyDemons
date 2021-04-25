using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Stats/Player")]
public class CharacterStat_SO : statsBase
{
    [Space]
    public string Name;
    public int skips;

    #region StatModifiers

    public void IncreaseSkips(int amount)
    {
        skips += amount;
    }

    public void DecreaseSkips(int amount)
    {
        skips -= amount;
        if (skips < 0)
        {
            skips = 0;
        }
    }

    #endregion
}
