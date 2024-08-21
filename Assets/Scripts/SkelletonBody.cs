using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelletonBody : MonoBehaviour
{
    public GameObject[] mainBodyGameObjects;
    public Renderer[] mainBodyRenderers;

    public GameObject[] skelletonBodyParts;

    public bool onGoing;

    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnRoundReset += this.DisableSkelleton;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnRoundReset -= this.DisableSkelleton;

        this.StopAllCoroutines();
        this.onGoing = false;
        this.DisableSkelleton();
    }

    [ContextMenu("EnableAndDisableSkelleton")]
    public void EnableAndDisableSkelleton()
    {
        if (!this.onGoing && this.gameObject.active)
        {
            this.StopAllCoroutines();
            this.StartCoroutine(this.SkelletonCoroutine());
        }
        
    }

    private IEnumerator SkelletonCoroutine()
    {
        this.EnableSkelleton();
        this.onGoing = true;
        yield return new WaitForSeconds(0.1f);
        this.DisableSkelleton();
        yield return new WaitForSeconds(0.1f);
        this.onGoing = false;
    }

    [ContextMenu("EnableSkelleton")]
    public void EnableSkelleton()
    {
        foreach (GameObject skelletonBodyPart in this.skelletonBodyParts)
        {
            if (skelletonBodyPart != null)
                skelletonBodyPart.SetActive(true);
        }

        foreach (GameObject mainBodyGameObject in this.mainBodyGameObjects)
        {
            if (mainBodyGameObject != null)
                mainBodyGameObject.SetActive(false);
        }

        foreach (Renderer mainBodyRenderer in this.mainBodyRenderers)
        {
            if (mainBodyRenderer != null)
                mainBodyRenderer.enabled = false;
        }
    }

    [ContextMenu("DisableSkelleton")]
    public void DisableSkelleton()
    {
        foreach (GameObject skelletonBodyPart in this.skelletonBodyParts)
        {
            if (skelletonBodyPart != null)
                skelletonBodyPart.SetActive(false);
        }

        foreach (GameObject mainBodyGameObject in this.mainBodyGameObjects)
        {
            if (mainBodyGameObject != null)
                mainBodyGameObject.SetActive(true);
        }

        foreach (Renderer mainBodyRenderer in this.mainBodyRenderers)
        {
            if (mainBodyRenderer != null)
                mainBodyRenderer.enabled = true;
        }
    }
}
