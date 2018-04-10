using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Lvl : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] bool isUnloked;
    [SerializeField] public Star[] stars;

    public bool IsUnloked { get { return isUnloked; } set { button.interactable = !(isUnloked = value); } }

    public GameObject lvlPrefab;
     GameObject createdLvl;

    private void Start()
    {
        IsUnloked = isUnloked;
    }

    int CheckHowManyStarsRecivedPlayer(int playerScore)
    {
        return stars.Where(t => t.CheckIfPlayerGainedStar(playerScore)).Count();
    }

    public void CreateLvl()
    {
        createdLvl = Instantiate(lvlPrefab);
        createdLvl.transform.SetParent(LvlManager.LvlHolder);
        LvlManager.SelectedLvl = this;
        MenuManager.instance.SwitchToMenu(MENU.Game);
    }

    public void EraseCreatedLvl()
    {
        Destroy(createdLvl);
    }
}
