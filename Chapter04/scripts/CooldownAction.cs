using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;  // add this line

public abstract class CooldownAction : MonoBehaviour  // alter this
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

    // subclasses can override these   
    protected virtual void DoAwake() { }
    protected virtual void DoUpdate() { }
    protected virtual void Triggered() { }
    protected virtual void Ready() { }

    // subclasses should NOT override Awake() and Uodate()
    void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
        DoAwake();
    }

    void Update()
    {
        if ((!isEnabled) || (!isTriggered)) return;

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= cooldownTime)
        {
            Reset();
            Ready();
            OnReady.Invoke();  // alert listeners
        }

        //if (isTriggered)
            DoUpdate();
    }

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
}
