using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAction : CooldownAction
{
    public GameObject missilePrefab;
    public GameObject lid;
    public AudioClip lidSFX;

    float lidLiftTime = 1.5f;
    private GameObject missile;
   

    protected override void Triggered()
    {
       missile = Instantiate(missilePrefab, target.transform);
        RaiseLid();
       
    }

    void RaiseLid()
    {
        PlaySFX(lidSFX);
        iTween.MoveBy(lid, iTween.Hash(
            "y", .25,
            "time", lidLiftTime,
            "oncomplete", "OnRaiseComplete",
            "oncompletetarget", gameObject));
    }

    void OnRaiseComplete()
    {
        LaunchMissile();
    }

    void LaunchMissile()
    {
        PlaySFX(actionSFX);

        var player = GameObject.FindGameObjectWithTag("Player");
        iTween.MoveTo(missile, iTween.Hash(
            "position", player.transform,
            "time", 1f,
            "oncomplete", "OnLaunchComplete",
            "oncompletetarget", gameObject));

        
    }

    IEnumerator OnLaunchComplete()
    {

        LowerLid();
    }

    void LowerLid()
    {
        PlaySFX(lidSFX);
        iTween.MoveBy(lid, iTween.Hash(
            "y", -.25,
            "time", lidLiftTime,
            "oncomplete", "OnLowerComplete",
            "oncompletetarget", gameObject));
    }

    void OnLowerComplete()
    {
        Reset();
        OnEnded.Invoke();
    } 
    
}
