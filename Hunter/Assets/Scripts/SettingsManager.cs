using UnityEngine;
using System.Collections;

public class SettingsManager : MonoBehaviour 
{
    public static SettingsManager ins;

    private void Awake()
    {
        ins = this;
    }
}
