using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsCatcher : MonoBehaviour
{
    //public Vector2 teleportPosition;
    //public float xPos;
    public Transform positionTransform;

    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();

        //ENABLE THIS THING AFTER ENTRANCE ANIMATIONS ARE DONE

        if (player != null && !player.dead && this.positionTransform != null)
        {
            //player.transform.position = new Vector3(this.transform.position.x + this.xPos, player.transform.position.y, 0f);
            player.transform.position = new Vector3(this.positionTransform.position.x, player.transform.position.y, 0f);
            Debug.Log(player.name);
        }
    }
}
