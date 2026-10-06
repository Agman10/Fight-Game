using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI.Core;

public class LoadingText : MonoBehaviour
{
    public TypewriterCore typewriter;

    private void OnEnable()
    {
        if(this.typewriter != null)
        {
            this.StartCoroutine(this.LoadingTextCoroutine());
        }
    }

    private void OnDisable()
    {
        this.StopAllCoroutines();
    }

    private IEnumerator LoadingTextCoroutine()
    {
        this.typewriter.ShowText("<notype>Loading<waitfor=0.4>.<waitfor=0.4>.<waitfor=0.4>.<waitfor=0.4>");
        //this.typewriter.ShowText("<notype>Loading.<waitfor=0.4>.<waitfor=0.4>.<waitfor=0.4>");
        while (this.typewriter.isShowingText)
        {
            yield return null;
        }

        //yield return new WaitForSeconds(0.4f);

        this.StartCoroutine(this.LoadingTextCoroutine());
    }
}
