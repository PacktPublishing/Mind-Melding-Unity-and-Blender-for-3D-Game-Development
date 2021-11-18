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
    Missile missileScript;

    protected override void Triggered()
    {
       
        RaiseLid();
        CreateMissile();
    }

    void CreateMissile()
    {
        missile = Instantiate(missilePrefab, target.transform);

        missileScript = missile.GetComponent<Missile>();
        missile = missileScript.gameObject;
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

        var player = GameObject.FindGameObjectWithTag("Muzzle");
        missileScript.Launch(player);
        StartCoroutine("OnLaunchComplete");
    }

    IEnumerator OnLaunchComplete()
    {
        yield return new WaitForSeconds(2);
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
