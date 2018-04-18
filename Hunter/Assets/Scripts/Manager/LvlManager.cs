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

    public void SaveCompletedLvlData()
    {
        SelectedLvl.SaveTotalScore(ScoreManager.instance.TotalScore);
    }

    public void LvlRestart()
    {
        SelectedLvl.PrepareObjectToSpawn();

        IObjectPooler[] reset = createdLvl.GetComponentsInChildren<IObjectPooler>();
        reset.ForEach(t => t.PrepareObjectToSpawn());
    }

    public void UnclockNextLvl()
    {
        if (CheckIfPlayerCompletedLastLvl())
            AllLvlCompleted();
        else if (SelectedLvl.GainedStars > 0)
        {
            int nextIndex = lvlSettingsArray.IndexOf(SelectedLvl) + 1;
            lvlSettingsArray[nextIndex].IsLvlLocked = false;
        }
    }

    void CreateLvlButtons()
    {
        for (int i = 0; i < lvlSettingsArray.Count; i++)
        {
            GameObject newButton = Instantiate(lvlButtonPrefab);
            newButton.GetComponent<LvlButton>().LvlSetting = lvlSettingsArray[i];

            newButton.transform.SetParent(lvlButtonHolder, false);
        }
    }

    void SortArrayOfLvls()
    {
        lvlSettingsArray.OrderBy(t => t.LvlNr);
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
