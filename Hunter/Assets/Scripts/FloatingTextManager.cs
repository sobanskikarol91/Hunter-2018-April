using System;
using UnityEngine;


public class FloatingTextManager : MonoBehaviour
{
    #region Singleton
    public static FloatingTextManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    [SerializeField] GameObject canvas;
    Vector3 displayTxtPos;

    float balisticShotDistance = 18f;
    public void ShowFloatingText(FLOATING_TXT type, Vector3 pos)
    {
        displayTxtPos = pos;
        switch (type)
        {
            case FLOATING_TXT.Hit:
                {
                    Hit();
                    break;
                }
            case FLOATING_TXT.Miss:
                {
                    Miss();
                    break;
                }
        }
    }

    private void Miss()
    {
        //TODO: another option Critical Miss
        ShowFloatingText("Miss");
    }

    void Hit()
    {
        float distance = Vector2.Distance(displayTxtPos, GameManager.instance.Player.position);
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
        floatingTxtGo.transform.SetParent(canvas.transform);
        floatingTxtGo.transform.position = Camera.main.WorldToScreenPoint(displayTxtPos);
        floatingTxtGo.transform.localScale = Vector3.one;
    }
}

public enum FLOATING_TXT { Hit, Miss }
