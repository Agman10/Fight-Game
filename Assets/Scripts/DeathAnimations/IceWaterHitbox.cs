using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWaterHitbox : MonoBehaviour
{
    public float yPos = -2.45f;
    public Transform xEdgeTransform;
    public bool underGround = false; //if true the ice cube and splash effect wont happen

    public bool occupied;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnRoundReset += this.RemoveOccupation;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnRoundReset -= this.RemoveOccupation;
    }

    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();
        if (player != null && !player.dead)
        {
            if(player.iceWaterDeathLogic != null)
            {
                if (!this.underGround)
                {
                    if (!this.occupied)
                    {
                        this.occupied = true;
                        player.IceWaterDeath(this.yPos);
                        if (this.xEdgeTransform != null)
                        {
                            if (this.transform.position.x < 0f)
                            {
                                if (player.transform.position.x > this.xEdgeTransform.position.x)
                                    player.transform.position = new Vector3(this.xEdgeTransform.position.x, player.transform.position.y, 0f);
                            }
                            else
                            {
                                if (player.transform.position.x < this.xEdgeTransform.position.x)
                                    player.transform.position = new Vector3(this.xEdgeTransform.position.x, player.transform.position.y, 0f);
                            }
                        }
                    }
                    else
                    {
                        //player.Die(this.transform.position, false, false, true, false, 5);
                        player.Die(this.transform.position, false, false, true, true, 5);
                        player.iceWaterDeathLogic.PlayWaterSplash();
                    }
                }
                else
                {
                    //player.Die(this.transform.position, false, false, true, false, 5);
                    player.Die(this.transform.position, false, false, true, true, 5);
                    player.iceWaterDeathLogic.PlaySpashSfx();
                }
                
            }
            else
            {
                player.Die(this.transform.position, false, false, true, false, 5);
            }
        }
    }

    public void RemoveOccupation()
    {
        this.occupied = false;
    }
}
