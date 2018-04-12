using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LvlManager : Singleton<LvlManager>
{
    public static Transform LvlHolder { get; private set; }
    public static LvlSetting SelectedLvl { get; set; }

    [SerializeField] List<LvlSetting> lvlSettingsArray = new List<LvlSetting>();
    [SerializeField] GameObject lvlButtonPrefab;
    [SerializeField] Transform lvlButtonHolder;

    GameObject createdLvl;

    protected override void Awake()
    {
        base.Awake();
        LvlHolder = new GameObject("LvlHolder").transform;
        SortArrayOfLvls();
        CreateLvlButtons();
    }

    public void DestroyLvl()
    {
        Destroy(createdLvl);
    }

    public void CreateLvl(LvlSetting lvlSetting)
    {
        SelectedLvl = lvlSetting;
        createdLvl = Instantiate(lvlSetting.Prefab);
        createdLvl.transform.SetParent(LvlHolder);
        MenuManager.instance.SwitchToMenu(MENU.Game);
    }

    void CreateLvlButtons()
    {
        for (int i = 0; i < lvlSettingsArray.Count; i++)
        {
            GameObject newButton = Instantiate(lvlButtonPrefab);
            newButton.GetComponent<LvlButton>().LvlSetting = lvlSettingsArray[i];

            newButton.transform.SetParent(lvlButtonHolder);
        }
    }

    void SortArrayOfLvls()
    {
        lvlSettingsArray.OrderBy(t => t.LvlNr);
    }

    public void LvlRestart()
    {
        SelectedLvl.ResetObject();

        IReset[] reset = createdLvl.GetComponentsInChildren<IReset>();
        reset.ForEach(t => t.ResetObject());
    }

    public void UnclockNextLvl()
    {
        if (CheckIfPlayerCompletedLastLvl())
            AllLvlCompleted();
        else if (SelectedLvl.GainedStars > 0)
        {
            Debug.Log(SelectedLvl.GainedStars + "unlocked");
            int nextIndex = lvlSettingsArray.IndexOf(SelectedLvl) + 1;
            Debug.Log(nextIndex);
            lvlSettingsArray[nextIndex].IsLvlLocked = false;
        }
    }

    bool CheckIfPlayerCompletedLastLvl()
    {
        return SelectedLvl.LvlNr == lvlSettingsArray.Count + 1;
    }

    void AllLvlCompleted()
    {
        //TODO:
    }
}
