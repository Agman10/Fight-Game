using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRandomly : MonoBehaviour
{
    public float chance;
    public GameObject model;
    public GameObject alternativeModel;
    private void OnEnable()
    {
        float number = Random.Range(0f, 100f);
        //Debug.Log(number);
        if (this.model != null)
        {
            this.model.SetActive(number <= this.chance);
            if (this.alternativeModel != null)
                this.alternativeModel.SetActive(number > this.chance);
        }
    }
}
