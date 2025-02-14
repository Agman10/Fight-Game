using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionToGlobal : MonoBehaviour
{
    public Vector3 localPos;
    public Vector3 globalPos;
    private void OnEnable()
    {
        this.localPos = this.transform.localPosition;
        this.globalPos = this.transform.position;

    }
    private void OnDisable()
    {
        this.transform.localPosition = this.localPos;

    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.transform.position = this.globalPos;
    }
}
