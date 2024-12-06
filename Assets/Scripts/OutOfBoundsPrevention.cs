using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsPrevention : MonoBehaviour
{
    public float xPosTeleport;
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
        TestPlayer player = other.GetComponent<TestPlayer>();

        if (player != null)
        {
            player.transform.position = new Vector3(this.xPosTeleport, player.transform.position.y, 0f);
            Debug.Log("teleported");
        }
    }
}
