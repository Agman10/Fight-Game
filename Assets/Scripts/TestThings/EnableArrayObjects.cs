using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableArrayObjects : MonoBehaviour
{
    public GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableObject(int objectId)
    {
        if(objectId <= this.objects.Length -1 && this.objects[objectId] != null)
        {
            this.objects[objectId].SetActive(true);
        }
    }

    public void DisableObject(int objectId)
    {
        if (objectId <= this.objects.Length - 1 && this.objects[objectId] != null)
        {
            this.objects[objectId].SetActive(false);
        }
    }

    public void DisableAll()
    {
        foreach(GameObject obj in this.objects)
        {
            obj.SetActive(false);
        }
    }
}
