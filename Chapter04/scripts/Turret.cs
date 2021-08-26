using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public enum State { IDLE, SCANNING, SHOOTING, LAUNCHING };

public class Turret : MonoBehaviour
{
    AudioSource turretAudio; // for SFX!

    State state = State.IDLE;

    CooldownAction scanAction;
    CooldownAction shootAction;
    CooldownAction launchAction;  //

    void Start()
    {

       
        turretAudio = GetComponent<AudioSource>();

        scanAction = GetComponent<ScanAction>();
        scanAction.OnEnded.AddListener(ScanEnded);

        shootAction = GetComponent<ShootAction>();
        shootAction.OnEnded.AddListener(ShootEnded); // 

        launchAction = GetComponent<LaunchAction>();  //
    }

    void StartScan() {
        state = State.SCANNING;
        scanAction.Trigger();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S)) {
            StartScan();
        }
    }

    void ScanEnded()
    {

        // we have found hero, start shooting
        StartShoot();
    }

    void StartShoot()
    {
        //turretAudio.Stop();

        state = State.SHOOTING;
        shootAction.Trigger();
    }
    
    void ShootEnded() {
        StartLaunch();
    }

    void StartLaunch()
    {
        state = State.LAUNCHING;
        launchAction.Trigger();
    }
    

}



// lid = GameObject.FindGameObjectWithTag("Lid");

//player = GameObject.FindGameObjectWithTag("Player");

/*       


        if ( lockedOn )
        {


            if (! Physics.Raycast(top.transform.position, top.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                lockedOn = false;
                StartCoroutine(ScanComplete());
            }


        }

        {
            // scanning , not locked




        }
 */