using UnityEngine;
using UnityEngine.UI;

public class Quiver : MonoBehaviour
{
    public Text leftArrowsTxt;

   [SerializeField] int maxArrows = 25;
    public int LeftArrows { get { return leftArrows; } private set { leftArrows = value; leftArrowsTxt.text = "Arrows: " + value.ToString(); } }

    private int leftArrows;
    private void Start()
    {
        leftArrows = maxArrows;
        BowEventManager.shooting.OnExit += RemoveArrowFromQuiver;
    }

    public void RemoveArrowFromQuiver()
    {
        LeftArrows--;
    }
}
