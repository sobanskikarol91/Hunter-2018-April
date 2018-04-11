﻿using UnityEngine;
using UnityEngine.UI;

public class StarController : MonoBehaviour 
{
    Animator animator;
    Image img;
    AudioSource audioSource;

    [SerializeField] Color32 destinyColor = new Color32(255,255,0,255);

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        img = GetComponent<Image>();
    }

    public void LightStar(bool withSound)
    {
        animator.SetBool("Light", true);
        img.color = destinyColor;
        if(withSound) audioSource.Play();
    }
}
