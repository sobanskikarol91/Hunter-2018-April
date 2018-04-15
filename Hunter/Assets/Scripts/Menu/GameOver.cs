using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameOver : MonoBehaviour, IReset
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
    }

    void DisplayNegativeScore(float score)
    {
        negativeScore.text = "Loss: " + score.ToString();
    }

    void DisplayTotalSore(float score)
    {
        totalScore.text = "Total Score: " + score.ToString();
        CheckIfLightStar(score);
    }

    IEnumerator DisplayAllScore()
    {
        int score = ScoreManager.instance.Score;
        int negativeScore = ScoreManager.instance.NegativeScore;
        int totalScore = score + negativeScore;

        ScoreManager.instance.ResetScore();
        // TODO: send score package
        if (score + negativeScore > 0)
            scoreCounterSnd.Play();

        PlayScoreCounter(score, DisplayScore);
        yield return new WaitForSeconds(durationNegativeScoreRun);

        PlayScoreCounter(negativeScore, DisplayNegativeScore);
        yield return new WaitForSeconds(durationNegativeScoreRun);

        PlayScoreCounter(totalScore, DisplayTotalSore);
    }

    void PlayScoreCounter(float score, Action<float> txtToDispaly)
    {
        Func<IEnumerator> scoreCounterDuration = () => IEnumeratorMethods.Lerp(0, score, scoreCounterSnd.clip.length, txtToDispaly);
        StartCoroutine(scoreCounterDuration());
    }

    void CheckIfLightStar(float score)
    {
        if (lightedStars == stars.Length) return;

        if (LvlManager.SelectedLvl.CheckIfPlayerReciveStar(score))
        {
            stars[lightedStars].LightStar(true);
            lightedStars++;
        }
    }

    private void OnEnable()
    {
        ResetObject();
    }

    public void ResetObject()
    {
        lightedStars = 0;
        StartCoroutine(DisplayAllScore());
    }
}
