using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxSetOwner : MonoBehaviour
{
    public TestPlayer owner;
    public TestHitbox[] hitboxes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetOwner(TestPlayer player)
    {
        this.owner = player;

        foreach (TestHitbox hitbox in this.hitboxes)
        {
            if (hitbox != null)
                hitbox.belongsTo = player;
        }
    }
}
