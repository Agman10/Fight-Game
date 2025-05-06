using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItemDestroyHitbox : MonoBehaviour
{
    public Collider hitbox;

    private void OnEnable()
    {
        this.hitbox = GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        FoodItem food = other.GetComponent<FoodItem>();
        if(food != null && !food.broken)
        {
            food.Break();
        }
    }
}
