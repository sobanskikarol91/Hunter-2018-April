using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : Singleton<HUDManager>
{
    [SerializeField] Text angleTxt;
    [SerializeField] Image tenseMeter;

    const string degrees = " \u00B0";

    public void SetAngleTxt(float value)
    {
        if (value < 0)
            value = 360 + value;

        angleTxt.text = ((int)value).ToString() + degrees;
    }

    public void SetTenseMeter(float value)
    {
        if (value < 0) return;
        tenseMeter.fillAmount = value;
        tenseMeter.color = Color.Lerp(Color.green,Color.red,value);
    }
}
