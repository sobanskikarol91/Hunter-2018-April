using UnityEngine;

public class LvlPrefab : MonoBehaviour, IReset
{
    public void ResetObject()
    {
        IReset[] reset = GetComponentsInChildren<IReset>();
        reset.ForEach(t => t.ResetObject());
    }
}
