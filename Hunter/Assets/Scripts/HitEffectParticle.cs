using UnityEngine;
using System.Collections;

public class HitEffectParticle : MonoBehaviour, IObjectPooler
{
    public void PrepareObjectToSpawn()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
