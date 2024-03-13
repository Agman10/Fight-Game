using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadStandingPrevention : MonoBehaviour
{
    public TestPlayer belongsTo;
    public float horizontalKnockback = 0f;
    public float verticalKnockback = 0f;
    public float damageDelay = 0.1f;
    public List<TestPlayer> players = new List<TestPlayer>();
    public bool knockForward = false;

    private void OnEnable()
    {
        this.players.Clear();
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
        this.players.Clear();

    }

    private void OnTriggerStay(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();
        if (player != null && !player.dead && !this.players.Contains(player))
        {
            if (this.belongsTo != player)
            {
                this.players.Add(player);
                //float distancee = (this.transform.position.x - player.transform.position.x);
                //Debug.Log(Mathf.Abs(this.transform.position.x - player.transform.position.x));
                //Debug.Log(this.transform.position.x - player.transform.position.x);
                //if(Vector3.Distance(player, this.transform.po))
                if (!this.knockForward)
                {
                    if (this.transform.position.x < 0f)
                        player.AddKnockback(this.horizontalKnockback, this.verticalKnockback);
                    else
                        player.AddKnockback(-this.horizontalKnockback, this.verticalKnockback);
                }
                else
                {
                    player.AddKnockback(this.transform.forward.z * this.horizontalKnockback, this.verticalKnockback);
                }
            }
            this.StartCoroutine(this.RemoveCoroutine(this.damageDelay, player));
        }
    }

    IEnumerator RemoveCoroutine(float delay, TestPlayer playerToRemove)
    {
        yield return new WaitForSeconds(delay);
        this.players.Remove(playerToRemove);
    }
}
