using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnReset : MonoBehaviour
{

    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnRoundReset += this.DisableObject;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnRoundReset -= this.DisableObject;
    }

    public void DisableObject()
    {
        //Debug.Log("test");
        this.gameObject.SetActive(false);
    }
}
