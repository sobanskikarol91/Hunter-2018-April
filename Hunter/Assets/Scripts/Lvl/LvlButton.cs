using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LvlButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Text buttonTxt;
    [SerializeField] Button button;
    [SerializeField] StarController[] starsControllers;

    private LvlSetting lvlSetting;
    public LvlSetting LvlSetting { get { return lvlSetting; } set { lvlSetting = value; LightStarsOnTile(); } }

    private void Start()
    {
        SetLvlNrTxt();
    }

    public void LightStarsOnTile()
    {
        for (int i = 0; i < LvlSetting.GainedStars; i++)
            starsControllers[i].LightStar(false);
    }

    private void OnEnable()
    {
        if (LvlSetting == null) return;
        LightStarsOnTile();
        SetInteractibleButton();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!button.interactable) return;
        LvlSetting.PrepareObjectToSpawn();
        LvlManager.instance.CreateLvl(LvlSetting);
    }

    void SetInteractibleButton()
    {
        button.interactable = !LvlSetting.IsLvlLocked;
    }

    
    void SetLvlNrTxt()
    {
        buttonTxt.text = LvlSetting.LvlNr.ToString();
    }
}
