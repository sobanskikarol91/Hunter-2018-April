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

    void DisplayScore(float score)
    {
        scoreTxt.text = "Score: +" + score.ToString();
    }

    void DisplayNegativeScore(float score)
    {
        negativeScore.text = "Loss: " + score.ToString();
    }

    void DisplayTotalSore()
    {
        totalScore.text = "Total Score: " + ScoreManager.instace.TotalScore.ToString();
    }

    private void OnEnable()
    {
        StartCoroutine(DisplayAllScore());
    }

    IEnumerator DisplayAllScore()
    {
        int score = ScoreManager.instace.Score;
        int negativeScore = ScoreManager.instace.NegativeScore;

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
         
    }
}
