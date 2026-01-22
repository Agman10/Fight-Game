using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHitbox : MonoBehaviour
{
    public int effectId;
    public float duration = 1f;
    public List<TestPlayer> players = new List<TestPlayer>();
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
        this.players.Clear();
    }

    private void OnDisable()
    {
        this.players.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();

        if (player != null && player.statusEffectHandler != null && !player.dead && !this.players.Contains(player))
        {
            this.players.Add(player);

            /*if (this.effectId == 0)
                player.statusEffectHandler.GivePoison(this.duration);*/

            player.statusEffectHandler.GiveStatusEffect(this.effectId, this.duration);
        }
    }


    /*private void OnTriggerStay(Collider other)
    {
        TestPlayer player = other.GetComponent<TestPlayer>();

        if (player != null && player.statusEffectHandler != null && !player.dead && !this.players.Contains(player))
        {
            this.players.Add(player);

            if (this.effectId == 0)
                player.statusEffectHandler.GivePoison(this.duration);

            if (this.gameObject.active)
                this.StartCoroutine(this.RemoveCoroutine(0.1f, player));
        }
    }*/

    private IEnumerator RemoveCoroutine(float delay, TestPlayer playerToRemove)
    {
        yield return new WaitForSeconds(delay);
        this.players.Remove(playerToRemove);
    }
}
