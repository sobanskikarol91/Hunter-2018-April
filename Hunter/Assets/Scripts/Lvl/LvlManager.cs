using UnityEngine;
using System.Collections;

public class LvlManager : MonoBehaviour
{
    public static LvlManager ins;
    public static Transform LvlHolder { get; private set; }
    public static Lvl SelectedLvl { get; set; }


    private void Awake()
    {
        LvlHolder = new GameObject("LvlHolder").transform;
    }

    public void DestrotLvl()
    {
        SelectedLvl.EraseCreatedLvl();
    }
}
