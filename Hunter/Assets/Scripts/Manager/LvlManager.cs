using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LvlManager : Singleton<LvlManager>
{
    public static Transform LvlHolder { get; private set; }
    public static LvlSetting SelectedLvl { get; set; }
    GameObject createdLvl;


    protected override void Awake()
    {
        base.Awake();
        LvlHolder = new GameObject("LvlHolder").transform;
    }

    public void DestroyLvl()
    {
        Destroy(createdLvl);
    }

    public void CreateLvl(LvlSetting lvlSetting)
    {
        SelectedLvl = lvlSetting;
        createdLvl = Instantiate(lvlSetting.Prefab);
        createdLvl.transform.SetParent(LvlManager.LvlHolder);
        MenuManager.instance.SwitchToMenu(MENU.Game);
    }
}
