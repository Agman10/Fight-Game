using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnDate : MonoBehaviour
{
    public GameObject targetObject;
    public int targetMonth = 1;

    [Space]
    public bool specificDay;
    public int targetDay;

    private void OnEnable()
    {
        if (this.targetObject != null)
        {
            if (!this.specificDay)
            {
                this.targetObject.SetActive(DateTime.Now.Month == this.targetMonth);
            }
            else
            {
                if(DateTime.Now.Month == this.targetMonth)
                    this.targetObject.SetActive(DateTime.Now.Day == this.targetDay);
            }
        }
    }
}
