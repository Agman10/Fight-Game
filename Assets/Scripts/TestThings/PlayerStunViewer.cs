using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunViewer : MonoBehaviour
{
    public TestPlayer player;
    public Renderer stunViewer;
    public Color noStunsColor;
    public Color stunsColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /*private void OnEnable()
    {
        if (this.stunViewer != null && this.stunViewer.material != null && this.player != null)
        {
            Material material = new Material(this.stunViewer.material);
            this.stunViewer.material = material;

            //this.stunViewer.material = new Material(this.stunViewer.material);
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        if(this.stunViewer != null && this.stunViewer.material != null && this.player != null)
        {
            if (this.player.stuns.Count > 0)
                this.stunViewer.material.color = this.stunsColor;
            else
                this.stunViewer.material.color = this.noStunsColor;
        }
    }
}
