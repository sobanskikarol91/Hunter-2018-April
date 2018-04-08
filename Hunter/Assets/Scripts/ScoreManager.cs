using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instace;

    [SerializeField] int score;
    [SerializeField] Text scoreTxt;
    [SerializeField] int pointsFactor = 2;


    private void Awake()
    {
        instace = this;
    }

    public void AddScore(Collision2D other)
    {
        if (other.gameObject.tag != TagManager.obstacle) return;
        if (other.contacts.Length == 0) return;
        int currentScore = CountScore(other);
        FloatingTextManager.instance.ShowFloatingText(currentScore, other.contacts[0].point);

        score += currentScore;
        scoreTxt.text = "Score: " + score.ToString();
    }

    int CountScore(Collision2D other)
    {
        float distanceFromCenter = Mathf.Abs(other.contacts[0].point.y - other.transform.position.y);
        float hightFromCenter = other.gameObject.GetComponent<Collider2D>().bounds.extents.y;
        float percent = distanceFromCenter * 100 / hightFromCenter;

        Debug.Log(percent);
        if (percent < 20) return 50 * pointsFactor;
        else if (percent < 40) return 40 * pointsFactor;
        else if (percent < 60) return 30 * pointsFactor;
        else if (percent < 80) return 20 * pointsFactor;
        else return 10 * pointsFactor;
    }
}
