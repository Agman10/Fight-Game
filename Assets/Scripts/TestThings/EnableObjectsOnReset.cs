using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectsOnReset : MonoBehaviour
{
    public GameObject[] objectsToEnable;
    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnRoundReset += this.EnableObject;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnRoundReset -= this.EnableObject;
    }

    public void EnableObject()
    {
        foreach (GameObject gameObject in this.objectsToEnable)
        {
            if (gameObject != null)
                gameObject.SetActive(true);
        }
    }
}
