using UnityEngine;

public class DynamicTarget : MonoBehaviour, IArrowHitEffect
{
    public void ArrowHitEffect()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
