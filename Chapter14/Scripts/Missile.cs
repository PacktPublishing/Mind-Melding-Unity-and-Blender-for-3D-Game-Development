using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Missile : MonoBehaviour
{
    public GameObject explosionPrefab;
    public AudioClip explosionSFX;

    AudioSource audioSource;

    public int damage = 20;

    float speed = 20f;
    bool doMove;
    Vector3 targetPos;

    void Start()
    {
        doMove = false;
        audioSource = GetComponent<AudioSource>();
    }

    public void Launch(GameObject target)
    {
        targetPos = target.transform.position;
        doMove = true;
    }

    void Update()
    {
        if(! doMove )
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        if( Vector3.Distance(transform.position, targetPos) < .1f)
        {
            doMove = false;
            Explode();
            gameObject.SetActive(false);
        }
    }

    public void Explode()
    {
        audioSource.clip = explosionSFX;
        audioSource.Play();

        GameObject explo = Instantiate(explosionPrefab);
        explo.transform.position = transform.position;

        var player = GameObject.FindGameObjectWithTag("Player");
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if( dist < 1.5f)
        {
            Marine marine = player.GetComponent<Marine>();
            marine.Damage(damage);
        }

        Destroy(explo, 1.2f);
        gameObject.SetActive(true);
    }
}
