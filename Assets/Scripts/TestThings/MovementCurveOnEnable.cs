using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCurveOnEnable : MonoBehaviour
{
    public float startDelay = 0f;
    public AnimationCurve xCurve;
    public AnimationCurve yCurve;
    public AnimationCurve zCurve;
    public float duration;
    public Vector3 vector3Multiplier = new Vector3(1f, 1f, 1f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        
        this.StartCoroutine(this.CurveCoroutine());
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
        //this.transform.localPosition = Vector3.zero;
        //this.transform.localPosition = new Vector3(-0.6f, 0f, 0f);
        this.transform.localPosition = new Vector3(
            this.xCurve.Evaluate(0f) * this.vector3Multiplier.x, 
            this.yCurve.Evaluate(0f) * this.vector3Multiplier.y, 
            this.zCurve.Evaluate(0f) * this.vector3Multiplier.z);
    }

    private IEnumerator CurveCoroutine()
    {
        yield return new WaitForSeconds(this.startDelay);
        //yield return new WaitForSeconds(0f);

        float currentTime = 0;
        while (currentTime < this.duration)
        {
            currentTime += Time.deltaTime;
            this.transform.localPosition = new Vector3(
                this.xCurve.Evaluate(currentTime / this.duration) * this.vector3Multiplier.x,
                this.yCurve.Evaluate(currentTime / this.duration) * this.vector3Multiplier.y,
                this.zCurve.Evaluate(currentTime / this.duration) * this.vector3Multiplier.z
                );
            yield return null;
        }
    }
}
