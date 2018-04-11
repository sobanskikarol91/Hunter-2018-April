using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LvlButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Button button;
    [SerializeField] StarController[] starsControllers;
    [SerializeField] LvlSetting lvlSetting;

    private void Start()
    {
        button.interactable = !lvlSetting.IsLvlLocked;
    }
   

    public void LightStarsOnTile()
    {
        for (int i = 0; i < lvlSetting.GainedStars; i++)
            starsControllers[i].LightStar(false);
    }

    private void OnEnable()
    {
        LightStarsOnTile();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        lvlSetting.ResetObject();
        LvlManager.instance.CreateLvl(lvlSetting);
    }
}
