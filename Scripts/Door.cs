using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    bool hasTriggered = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        
        if (!other.CompareTag("Player")) return;
        
        hasTriggered = false;
        animator.enabled = true;
        audioSource.Play();
    }
}
