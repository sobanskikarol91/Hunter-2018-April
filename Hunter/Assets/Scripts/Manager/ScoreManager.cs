using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] Text scoreTxt;
    [SerializeField] int pointsFactor = 2;

    public int Score { get; private set; }
    public int NegativeScore { get; private set; }
    public int TotalScore { get { return Score + NegativeScore; } }

    public void AddScore(Collision2D other)
    {
        if (other.gameObject.tag != TagManager.obstacle) return;
        if (other.contacts.Length == 0)
        {
            Debug.LogError(other.contacts.Length);
            return;
        }
        int currentScore = CountScore(other);
        FloatingTextManager.instance.ShowFloatingText(currentScore, other.contacts[0].point);

        Score += currentScore;
        scoreTxt.text = "Score: " + Score.ToString();
    }

    int CountScore(Collision2D other)
    {
        float distanceFromCenter = Mathf.Abs(other.contacts[0].point.y - other.transform.position.y);
        float hightFromCenter = other.gameObject.GetComponent<Collider2D>().bounds.extents.y;
        float percent = distanceFromCenter * 100 / hightFromCenter;

        if (percent < 20) return 50 * pointsFactor;
        else if (percent < 40) return 40 * pointsFactor;
        else if (percent < 60) return 30 * pointsFactor;
        else if (percent < 80) return 20 * pointsFactor;
        else return 10 * pointsFactor;
    }

    public void ResetScore()
    {
        Score = 0; 
        NegativeScore = 0;
    }
}
