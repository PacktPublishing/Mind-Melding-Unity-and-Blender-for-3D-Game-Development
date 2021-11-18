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

    bool isDamaged;
    public int hitPoints = 9;

    public ParticleSystem smokeVFX;
    public ParticleSystem explosionVFX;

    public AudioClip explosionSFX;


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

    void StartLaunch()
    {
        state = State.LAUNCHING;
        launchAction.Trigger();
    }

    public void Damage(int amount)
    {
        if(isDamaged == false)
        {
            smokeVFX.gameObject.SetActive(true);
        }
        isDamaged = true;
        hitPoints -= amount;

        if( hitPoints <= 0)
        {
            StartCoroutine("Die");
        }
    }

    IEnumerator Die()
    {
        turretAudio.clip = explosionSFX;
        turretAudio.Play();
        yield return null;
        explosionVFX.gameObject.SetActive(true);
        Destroy(gameObject, 1.2f);
    }

    void LaunchEnded()
    {
        StartScan();
    }
}
