using System.Collections;
using UnityEngine;

public class Marine : MonoBehaviour
{
    AudioSource audioSource;
    Animator animator;

    bool isMoving;

    public float speed = 3.5f;
    public int rotSpeed = 5;
    public GameObject muzzle;
    RaycastHit hit;
    float lookAheadDistance = .25f;



    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();


    }

    public void Boot1()
    {
        audioSource.pitch = 1f;
        audioSource.Play();
    }

    public void Boot2()
    {
        audioSource.pitch = .9f;
        audioSource.Play();
    }

    private void Update()
    {


        if (Input.GetKey(KeyCode.RightArrow)) DoTurnRight();
        else if (Input.GetKey(KeyCode.LeftArrow)) DoTurnLeft();

        if(Input.GetKey(KeyCode.UpArrow))
        {
            if (isMoving == false)
            {
                animator.SetTrigger("isWalking");
                isMoving = true;
            }
            else DoMove();
        } else
        {
            if(isMoving && ! Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetTrigger("isIdle");
                isMoving = false;
            }
        }

 
    }

    void DoTurnLeft()
    {
        transform.Rotate(new Vector3(0, -rotSpeed, 0));
    }

    void DoTurnRight()
    {
        transform.Rotate(new Vector3(0, rotSpeed, 0));
    }

    void DoMove()
    {
        var moveDistance = Time.deltaTime * speed;

        Ray ray = new Ray(muzzle.transform.position, transform.forward);


        if (Physics.Raycast(ray, out hit, lookAheadDistance) && (hit.transform.gameObject.tag=="Wall"))
        {
            // nothing
        } else
        {
            transform.position += transform.forward * moveDistance;
        }
    }


}
