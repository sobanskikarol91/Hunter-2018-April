using System;
using UnityEngine;


public class FloatingTextManager : Singleton<FloatingTextManager>
{
    [SerializeField] GameObject canvas;
    Vector3 displayTxtPos;

    float balisticShotDistance = 18f;

    public void ShowFloatingText(FLOATING_TXT type, Vector3 pos)
    {
        displayTxtPos = pos;
        switch (type)
        {
            case FLOATING_TXT.Miss:
                ShowFloatingText("Miss");
                break;
        }
    }

    public void ShowFloatingText(int value, Vector3 pos)
    {
        displayTxtPos = pos;

        if (value == 100)
        {
            ShowFloatingText("100");
            CameraShaker.instance.ShakeCamere(1.4f, 0.32f);
            return;
        }
        else if (value == 80)
            ShowFloatingText("80");
        else if (value == 60)
            ShowFloatingText("60");
        else if (value == 40)
            ShowFloatingText("40");
        else
            ShowFloatingText("20");

        CameraShaker.instance.ShakeCamere();
    }


    private void Miss()
    {
        //TODO: another option Critical Miss
        ShowFloatingText("Miss");
    }

    void Hit()
    {
        float distance = Vector2.Distance(displayTxtPos, GameManager.Player.position);
        if (distance > balisticShotDistance)
        {
            ShowFloatingText("Ballistic");
            CameraShaker.instance.ShakeCamere(1.4f, 0.32f);
        }
        else
        {
            ShowFloatingText("NiceShot");
            CameraShaker.instance.ShakeCamere();
        }
    }

    void ShowFloatingText(string tag)
    {
        GameObject floatingTxtGo = ObjectPoolerManager.instance.SpawnFromPool(tag, displayTxtPos, Quaternion.identity);
        floatingTxtGo.transform.SetParent(canvas.transform, false);
        floatingTxtGo.transform.position = displayTxtPos;
    }
}

public enum FLOATING_TXT { Hit, Miss }
