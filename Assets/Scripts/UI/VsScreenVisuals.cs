using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VsScreenVisuals : MonoBehaviour
{
    public Transform nameBackground;
    public Transform vsText;
    public Transform fightGameText;
    public GameObject characterModels;
    public GameObject characterNames;

    private void OnEnable()
    {
        this.StartCoroutine(this.VsScreenVisualCoroutine());
    }

    private IEnumerator VsScreenVisualCoroutine()
    {

        yield return new WaitForSeconds(0.1f);
        this.nameBackground.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        this.fightGameText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        this.vsText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        this.characterModels.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        this.characterNames.SetActive(true);
    }
}
