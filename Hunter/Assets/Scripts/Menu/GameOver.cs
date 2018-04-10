using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameOver : MonoBehaviour
{
    [SerializeField] float changeScoreTime;
    [SerializeField] float durationNegativeScoreRun = .2f;
    [SerializeField] Text scoreTxt;
    [SerializeField] Text negativeScore;
    [SerializeField] Text totalScore;
    [SerializeField] AudioSource scoreCounterSnd;
    [SerializeField] StarController[] stars;


    private int lightedStars = 0;

    void DisplayScore(float score)
    {
        scoreTxt.text = "Score: +" + score.ToString();
        CheckIfLightStar(score);
    }

    void DisplayNegativeScore(float score)
    {
        negativeScore.text = "Loss: " + score.ToString();
    }

    void DisplayTotalSore()
    {
        totalScore.text = "Total Score: " + ScoreManager.instance.TotalScore.ToString();
    }

    IEnumerator DisplayAllScore()
    {
        int score = ScoreManager.instance.Score;
        int negativeScore = ScoreManager.instance.NegativeScore;

        if (score + negativeScore > 0)
            scoreCounterSnd.Play();

        PlayScoreCounter(score, DisplayScore);
        yield return new WaitForSeconds(durationNegativeScoreRun);

        PlayScoreCounter(negativeScore, DisplayNegativeScore);
        yield return new WaitForSeconds(scoreCounterSnd.clip.length);

        DisplayTotalSore();
    }

    void PlayScoreCounter(float score, Action<float> txtToDispaly)
    {
        Func<IEnumerator> scoreCounterDuration = () => IEnumeratorMethods.Lerp(0, score, changeScoreTime, txtToDispaly);
        StartCoroutine(scoreCounterDuration());
    }


    void CheckIfLightStar(float score)
    {
        if (lightedStars == stars.Length) return;

        if (LvlManager.SelectedLvl.CheckIfPlayerReciveStar(score))
        {
            stars[lightedStars].LightStar();
            lightedStars++;
        }
    }

    private void OnEnable()
    {
        lightedStars = 0;
        StartCoroutine(DisplayAllScore());
    }
}
