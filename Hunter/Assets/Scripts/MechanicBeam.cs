using UnityEngine;
using System.Collections;

public class MechanicBeam : Obstacle
{
    float angleMove = 90;

    private void OnEnable()
    {
        EventManager.StartListening("Switch", Rotate);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Switch", Rotate);
    }

    void Rotate()
    {
        audioSource.Play();
        float startRotationZ = transform.rotation.eulerAngles.z;
        StartCoroutine(IEnumeratorMethods.Lerp(startRotationZ, startRotationZ + angleMove, audioSource.clip.length, AssignRotation));
    }

    void AssignRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
