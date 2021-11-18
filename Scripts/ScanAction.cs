using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanAction : CooldownAction
{
    public float turnTime = 1f;

    protected override void DoAwake()
    {
        target = GameObject.FindGameObjectWithTag("Top");
    }

    
    protected override void DoUpdate()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(target.transform.position,
            target.transform.TransformDirection(Vector3.forward), out hit,
            Mathf.Infinity))
        {

            if (!(hit.collider.gameObject.tag == "Player")) return;

            try
            {
                iTween.StopByName("turning");
            }
            catch { }
            Reset();
            OnEnded.Invoke();
        } 
    }

    protected override void Triggered()
    {
        PlaySFX(actionSFX);
        iTween.RotateBy(target, iTween.Hash(
            "name", "turning",
            "y", .25,
            "time", turnTime,
            "easeType", "easeOutBack"));
    }

    protected override void Ready()
    {
        Trigger();
    }
}
