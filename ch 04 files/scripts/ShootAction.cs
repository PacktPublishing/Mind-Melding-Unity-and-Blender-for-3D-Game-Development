using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : CooldownAction
{

    int shotsFired;
    Light glow;
    LineRenderer beam;

    protected override void DoAwake()
    {
        target.SetActive(false); // turn off light
        glow = target.GetComponent<Light>();
        beam = target.GetComponent<LineRenderer>();
    }


    protected override void Triggered()
    {
        shotsFired++;

        PlaySFX( actionSFX );
        
        target.SetActive(true);
        beam.enabled = true;

        // add back in later
        /*var player = GameObject.FindGameObjectWithTag("Player");
        var distance = player.transform.position - target.transform.position;
        lineRenderer.SetPosition(1, new Vector3(0, 0, distance.magnitude - 1));
        */

        iTween.ValueTo(target, iTween.Hash(
            "from", 5f,
            "to", .5f,
            "time", .4,
            "onupdate", "OnTweenUpdate",
            "onupdatetarget", gameObject,
            "oncomplete", "OnTweenComplete",
            "oncompletetarget", gameObject
            ));        
    }

   
    void OnTweenUpdate(float value) {
        glow.range = value;

        if( value < 2f) beam.enabled = false;
    }

    void OnTweenComplete() {
        target.SetActive(false);
        
        if (shotsFired < 3) Triggered();
        else
        {
            Reset();
            OnEnded.Invoke();
        }
    }
}
