using System;
using UnityEngine;

public class Lever : Interactable
{
    public GameObject toggle;
    public Sprite usedTexture;

    private SpriteRenderer m_Renderer;
    private bool m_Used;

    private void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
    }

    public override void Interact(GameObject trigger)
    {
        Debug.Log("Lever#Interact");
        if (m_Used) return;
        toggle.SetActive(true);
        m_Renderer.sprite = usedTexture;
        m_Used = true;
    }
}