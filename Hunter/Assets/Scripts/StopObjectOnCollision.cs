using UnityEngine;
using System.Collections;

public class StopObjectOnCollision : MonoBehaviour 
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagManager.arrow)
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
