using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : CooldownAction
{
    int shotsFired;
    Light glow;
    LineRenderer beam;

    public int damage = 3;
    public float range = 40f;

    protected override void DoAwake()
    {
        target.SetActive(false);
        glow = target.GetComponent<Light>();
        beam = target.GetComponent<LineRenderer>();
    }

    protected override void Triggered()
    {
        shotsFired++;
        PlaySFX(actionSFX);
        target.SetActive(true);
        beam.enabled = true;

        var origin = transform.TransformPoint(Vector3.zero);
        var adjOrigin = new Vector3(origin.x, target.transform.position.y, origin.z);

        beam.SetPosition(0, adjOrigin);

        var player = GameObject.FindGameObjectWithTag("Player");
        var playerPos = player.transform.position;
        var pos = new Vector3(playerPos.x, adjOrigin.y, playerPos.z); 
        beam.SetPosition(1, pos);

        var marine = player.GetComponent<Marine>();
        marine.Damage(damage);

        iTween.ValueTo(target, iTween.Hash(
            "from", 5f,
            "to", .5f,
            "time", .4,
            "onupdate", "OnTweenUpdate",
            "onupdatetarget", gameObject,
            "oncomplete", "OnTweenComplete",
            "oncompletetarget", gameObject));
    }

    void OnTweenUpdate(float value)
    {
        glow.range = value;
        if (value < 2f) beam.enabled = false;
    }

    void OnTweenComplete()
    {
        target.SetActive(false);


        if (shotsFired < 3) Triggered();
        else
        {
            Reset();
            OnEnded.Invoke();
        }
    }
}
