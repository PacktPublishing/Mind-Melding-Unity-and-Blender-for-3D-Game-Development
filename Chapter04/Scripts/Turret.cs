using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { IDLE, SCANNING, SHOOTING, LAUNCHING };

public class Turret : MonoBehaviour
{
    AudioSource turretAudio;
    State state = State.IDLE;

    CooldownAction scanAction;
    CooldownAction shootAction;
    CooldownAction launchAction;

 


    void Start()
    {
        turretAudio = GetComponent<AudioSource>();

        scanAction = GetComponent<ScanAction>();
        scanAction.OnEnded.AddListener(ScanEnded);

        shootAction = GetComponent<ShootAction>();
        shootAction.OnEnded.AddListener(ShootEnded);

        launchAction = GetComponent<LaunchAction>();
        launchAction.OnEnded.AddListener(LaunchEnded);

        StartScan();
    }

    void StartScan()
    {
        state = State.SCANNING;
        scanAction.Trigger();
    }


    void ScanEnded()
    {
        StartShoot();
    }

    void StartShoot()
    {

        state = State.SHOOTING;
        shootAction.Trigger();
    }

    void ShootEnded()
    {
        StartLaunch();
    }

 void StartLaunch() {
  state = State.LAUNCHING;
  launchAction.Trigger();
}


    

   

    void Update() {
       if (Input.GetKeyDown(KeyCode.S)) StartScan(); // code for testing
    } 

}
