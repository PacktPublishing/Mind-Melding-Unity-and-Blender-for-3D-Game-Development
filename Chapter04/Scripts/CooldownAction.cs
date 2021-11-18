using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public abstract class CooldownAction : MonoBehaviour
{
    public AudioClip actionSFX;
    public float cooldownTime;
    public bool isEnabled = true;
    public GameObject target;
    [HideInInspector]
    public UnityEvent OnReady = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnEnded = new UnityEvent();
    protected bool isTriggered = false;
    protected AudioSource audioSource;
    private float elapsedTime = 0;

    protected virtual void DoAwake() { }
    protected virtual void DoUpdate() { }
    protected virtual void Triggered() { }
    protected virtual void Ready() { }

    public void Trigger()
    {
        if ((!isEnabled) || isTriggered) return;
        isTriggered = true;
        Triggered();
    }

    protected void Reset()
    {
        elapsedTime = 0;
        isTriggered = false;
    }

    protected void PlaySFX(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        DoAwake();
    }

    private void Update()
    {
        if ((!isEnabled) || (!isTriggered)) return;

        elapsedTime += Time.deltaTime;
        if(elapsedTime >= cooldownTime)
        {
            Reset();
            Ready();
            OnReady.Invoke();
        }
        if (isTriggered) DoUpdate();

    }
}
