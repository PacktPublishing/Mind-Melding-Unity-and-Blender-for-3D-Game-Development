using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanAction : CooldownAction
{
    
    public float turnTime = 1f;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Top");
    }

    protected override void DoUpdate()
    {
        //Debug.DrawRay(target.transform.position, target.transform.TransformDirection(Vector3.forward) * 100, Color.yellow);

        RaycastHit hit;

        if (Physics.Raycast(target.transform.position, target.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (!(hit.collider.gameObject.tag == "Player")) return;
             iTween.StopByName("turning");  // can interrupt tween, stop turning
            Reset();
            OnEnded.Invoke();
        }
    }

    protected override void Triggered()
    {
        PlaySFX(actionSFX);

        iTween.RotateBy(target, iTween.Hash(
          "name", "turning",
          "y", .25,  // quarter turn, 25% of circle
          "time", turnTime,
          "easeType", "easeOutBack"  
        ));
    }

    protected override void Ready()
    {
        Trigger();  // continuously scan
    }


   /* IEnumerator ScanRoutine()
    {
        yield return new WaitForSeconds(turnTime); // let the turn complete       
    } */
}


//lockedOn = true;

//Debug.DrawRay(top.transform.position, top.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

//yield return new WaitForSeconds(scanInterval);
//scanning = false;
//if (turretAudio.isPlaying) turretAudio.Stop();
// if 