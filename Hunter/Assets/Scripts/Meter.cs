using UnityEngine;


public class Meter : MonoBehaviour 
{
    [SerializeField] GameObject meterObject;

    private void OnEnable()
    {
        BowEventManager.shooting.OnEnter += ShowMeter;
        BowEventManager.shooting.OnExit += DisableMeter;
        DisableMeter();
    }

    private void OnDisable()
    {
        BowEventManager.shooting.OnEnter -= ShowMeter;
        BowEventManager.shooting.OnExit -= DisableMeter;
    }

    void ShowMeter()
    {
        meterObject.SetActive(true);
    }

    void DisableMeter()
    {
        meterObject.SetActive(false);
    }
}
