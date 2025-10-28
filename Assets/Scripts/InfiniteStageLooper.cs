using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteStageLooper : MonoBehaviour
{
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

        if (player != null && !player.dead && GameManager.Instance != null && GameManager.Instance.gameCamera != null)
        {
            if(GameManager.Instance.player1 != null && GameManager.Instance.player2 != null)
            {
                float p1Pos = GameManager.Instance.gameCamera.transform.position.x - GameManager.Instance.player1.transform.position.x;
                float p2Pos = GameManager.Instance.gameCamera.transform.position.x - GameManager.Instance.player2.transform.position.x;

                GameManager.Instance.player1.transform.position = new Vector3(-p1Pos, GameManager.Instance.player1.transform.position.y, GameManager.Instance.player1.transform.position.z);
                GameManager.Instance.player2.transform.position = new Vector3(-p2Pos, GameManager.Instance.player2.transform.position.y, GameManager.Instance.player2.transform.position.z);

                //GameManager.Instance.gameCamera.transform.position = new Vector3(0f, GameManager.Instance.gameCamera.transform.position.y, GameManager.Instance.gameCamera.transform.position.z);
            }
        }
    }
}
