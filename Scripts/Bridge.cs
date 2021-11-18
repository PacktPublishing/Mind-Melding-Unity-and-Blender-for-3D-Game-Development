using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Bridge : MonoBehaviour
{
    public PlayableDirector timeline;
    bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (!other.CompareTag("Player")) return;

        hasTriggered = true;

        timeline.Play();
    }

    
}
