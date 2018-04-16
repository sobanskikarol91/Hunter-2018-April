using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lvl", menuName = "Lvl")]
public class LvlSetting : ScriptableObject, IObjectPooler
{
    public int LvlNr { get { return lvlNr; } }
    public int[] StarsRequireScore { get { return starsRequireScore; } }
    public int ArrowsAmount { get { return arrowAmount; } }
    public int MaxScoreToGain { get { return maxScore; } }
    public GameObject Prefab { get { return prefab; } }
    public bool IsLvlLocked { get { return isLvlLocked; } set { isLvlLocked = value; } }
    public int HighestScore { get; set; }

    [SerializeField] int lvlNr;
    [SerializeField] private int[] starsRequireScore = new int[3];
    [SerializeField] private int arrowAmount = 10;
    [SerializeField] private int maxScore = 0;
    [SerializeField] GameObject prefab;
    [SerializeField] private bool isLvlLocked = true;
    [SerializeField] int gainedStars;

    // TODO is lvl completed
    public int GainedStars { get { return gainedStars; } }

    public bool CheckIfPlayerReciveStar(float playerScore)
    {
        Func<int> starsAmount = () => starsRequireScore.Length;

        if (GainedStars >= starsAmount()) return false;
        if (playerScore >= StarsRequireScore[gainedStars] && gainedStars <= starsAmount())
        {
            gainedStars++;
            return true;
        }

        return false;
    }

    public void PrepareObjectToSpawn()
    {
        gainedStars = 0;
    }
}
