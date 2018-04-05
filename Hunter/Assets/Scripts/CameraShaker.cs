using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker instance;
    Vector2 velocity;
    Vector2 cameraOriginPos;

    [SerializeField]  float smoothTimerX = 0.01f;
    [SerializeField]  float smoothTimerY = 0.01f;
    [SerializeField] const float defaultShakeTimer = 0.25f;
    [SerializeField] const float defaultSkakePower = 0.5f;

    private void Awake()
    {
        instance = this;
        cameraOriginPos = transform.position;
    }

    public void ShakeCamere(float shakePower = defaultSkakePower, float shakeTimer = defaultShakeTimer)
    {
        StartCoroutine(IEShakeCamera(shakePower, shakeTimer));
    }

    IEnumerator IEShakeCamera(float shakePower, float shakeTime)
    {
        float currentShakeTime = shakeTime;

        while (currentShakeTime >= 0)
        {
            Vector2 ShakePos = Random.insideUnitCircle * shakePower;
            transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);
            currentShakeTime -= Time.deltaTime;
            SmoothFallow();
            yield return null;
        }
    }

    void SmoothFallow()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, cameraOriginPos.x, ref velocity.x, smoothTimerX);
        float posY = Mathf.SmoothDamp(transform.position.y, cameraOriginPos.y, ref velocity.y, smoothTimerY);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
