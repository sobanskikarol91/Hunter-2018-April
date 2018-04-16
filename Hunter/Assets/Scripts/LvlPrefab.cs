using UnityEngine;

public class LvlPrefab : MonoBehaviour, IObjectPooler
{
    public void PrepareObjectToSpawn()
    {
        IObjectPooler[] reset = GetComponentsInChildren<IObjectPooler>();
        reset.ForEach(t => t.PrepareObjectToSpawn());
    }
}
