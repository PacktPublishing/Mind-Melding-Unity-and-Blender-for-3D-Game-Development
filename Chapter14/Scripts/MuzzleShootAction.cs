using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleShootAction : CooldownAction
{
    public int damage = 3;
    public float range = 25f;

    Light glow;
    LineRenderer beamLine;

    Ray ray;
    RaycastHit hit;

    protected override void DoAwake()
    {
        beamLine = GetComponent<LineRenderer>();
        glow = GetComponent<Light>();

    }

    void EnableEffects(bool show)
    {
        try
        {
            beamLine.enabled = show;
            glow.enabled = show;
        } catch { }
    }

    protected override void Triggered()
    {
        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        audioSource.Play();
        EnableEffects(true);


        var pos = transform.position;
        ray = new Ray();
        ray.origin = new Vector3(pos.x, 1.35f, pos.z);


        beamLine.SetPosition(0, ray.origin);

        ray.direction = transform.forward;

        if(Physics.Raycast( ray, out hit, range))
        {
            
            if((hit.collider.gameObject.tag == "Foe"))
            {
                
                Turret turret = hit.collider.gameObject.GetComponent<Turret>();
                if( turret != null )
                {
                    turret.Damage(damage);
                }
            }

            beamLine.SetPosition(1, hit.point);
        } else
        {
            beamLine.SetPosition(1, ray.origin + ray.direction * range);
        }

        yield return new WaitForSeconds(.2f);
        EnableEffects(false);
    }

}
