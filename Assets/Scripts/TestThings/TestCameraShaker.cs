using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraShaker : MonoBehaviour
{
    //private static Vector2 shakeOffset = Vector2.zero;
    //private static Vector2 shakeOffset = Vector2.zero;
    // Start is called before the first frame update

    public float testShakeMagnitude = 0.1f;
    public float testShaketime = 0.1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /*public static async UniTask CameraShakeAsync(float magnitude, float time)
    {
        float elapsed = 0f;

        while (elapsed < time)
        {


            shakeOffset = UnityEngine.Random.insideUnitCircle.normalized * magnitude;

            elapsed += Time.unscaledDeltaTime;

            await UniTask.Yield();
        }

        shakeOffset = Vector2.zero;
    }*/

    public void Shake(float magnitude, float time, float priority)
    {
        //ADD PRIORITY SYSTEM
        //also rename this script so there is no "test" when it's actually in use

        this.StopAllCoroutines();
        this.transform.localPosition = Vector3.zero;
        this.StartCoroutine(this.ShakeCoroutine(magnitude, time));
    }

    [ContextMenu("Shake")]
    public void TestShake()
    {
        this.StopAllCoroutines();
        this.transform.localPosition = Vector3.zero;
        this.StartCoroutine(this.ShakeCoroutine(this.testShakeMagnitude, this.testShaketime));
    }

    private IEnumerator ShakeCoroutine(float magnitude, float time)
    {
        float elapsed = 0f;

        while (elapsed < time)
        {


            //this.transform.localPosition = UnityEngine.Random.insideUnitCircle.normalized * magnitude;

            //this.transform.localPosition = new Vector3(Random.Range(-magnitude, magnitude), Random.Range(-magnitude, magnitude), Random.Range(-magnitude, magnitude));
            this.transform.localPosition = new Vector3(Random.Range(-magnitude, magnitude), Random.Range(-magnitude, magnitude), 0f);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        this.transform.localPosition = Vector3.zero;
    }
}
