using Febucci.UI.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeWriterTestScript : MonoBehaviour
{
    public TypewriterCore typewriter;
    public AudioSource explosionSfx;
    private void OnEnable()
    {
        if(this.typewriter != null)
        {
            this.typewriter.onMessage.AddListener(OnTypewriterMessage);

            //this.typewriter.SetTypewriterSpeed(1000f);
            //this.typewriter.StartShowingText(true);

            //this.typewriter.ShowText("fdsf sdfsdf <waitfor=1.5>ewfsdf");
            //this.typewriter.StartShowingText();
            this.StartCoroutine(this.TestShowText());
        }
    }
    private void OnDisable()
    {
        if (this.typewriter != null)
        {
            this.typewriter.onMessage.RemoveListener(OnTypewriterMessage);
        }
    }

    private IEnumerator TestShowText()
    {
        yield return new WaitForSeconds(0.1f);
        this.typewriter.SetTypewriterSpeed(0.75f);
        this.typewriter.ShowText("<size=200%>Fuck you J-Cap!\n<waitfor=0.4>I don't like you!");
        
        //yield return new WaitForSeconds(1f);

        while (this.typewriter.isShowingText)
        {
            Debug.Log(this.typewriter.isShowingText);
            yield return null;
        }
        Debug.Log(this.typewriter.isShowingText);
        this.typewriter.ShowText("");
        yield return new WaitForSeconds(1f);
        //this.typewriter.SetTypewriterSpeed(111f);
        //this.typewriter.ShowText("this is <waitfor=0.5>a test");
        this.typewriter.ShowText("<rainb f=4 s=-0.1>HYPER\n<size=130%>K.O.</rainb> ");
        //Debug.Log(this.typewriter.isShowingText);
        yield return new WaitForSeconds(0.9f);
        this.typewriter.SkipTypewriter();
        yield return new WaitForSeconds(0.1f);
        this.typewriter.SetTypewriterSpeed(1f);
        this.typewriter.ShowText("<speed=1>You are so <waitfor=0.05><?explosion><notype><color=#FF4A4A>{size d=0.25}FUCKING</color>{/size}</notype><waitfor=0.05> stupid!");
        //this.typewriter.SkipTypewriter();
    }

    void OnTypewriterMessage(Febucci.UI.Core.Parsing.EventMarker eventMarker)
    {
        switch (eventMarker.name)
        {
            case "explosion":
                if (this.explosionSfx != null)
                    this.explosionSfx.Play();
                break;
        }
    }

    public void TestDebug()
    {
        Debug.Log("fsfsf");
    }
}
