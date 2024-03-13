using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndTeleportObject : MonoBehaviour
{
    public Vector3 respawnPos;
    public float speed;
    public float maxYPos;
    public bool disableOnLimit = false;
    public Vector3 startPos;
    private void OnEnable()
    {
        this.startPos = this.transform.localPosition;
    }
    private void OnDisable()
    {
        this.transform.localPosition = this.startPos;
        this.transform.localEulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(0f, this.speed * Time.deltaTime, 0f));
        if (this.transform.localPosition.y >= this.maxYPos)
        {
            if (!this.disableOnLimit)
                this.transform.localPosition = this.respawnPos;
            else
                this.gameObject.SetActive(false);
        }
            
    }
}
