using UnityEngine;
using System.Collections;


public class Marine : MonoBehaviour
{

    AudioSource audioSource;

    public int rotSpeed = 5;  // rotation speed

    Animator animator;

    RaycastHit hit;  // to detect walls

    bool isMoving;  // else idling 

    public float speed = 3.5f;

    float lookAheadDistance = .25f;

    public GameObject muzzle;  // populate this!

    

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
       
        if (muzzle == null) Debug.Log("NULL muzzle");
    }

    public void Boot1() {
        audioSource.pitch = 1f;
        audioSource.Play();
    }

    public void Boot2() {
        audioSource.pitch = .9f;
        audioSource.Play();
    }


    void DoTurnLeft()
    {
        // counter clockwise
        transform.Rotate(new Vector3(0, -rotSpeed, 0));
    }

    void DoTurnRight()
    {
        // clockwise
        transform.Rotate(new Vector3(0, rotSpeed, 0));      
    }

    void DoIdle() // new
    {
        animator.SetTrigger("isIdle");
        isMoving = false;
    }


    void DoMove()
    {
        //Debug.Log($"DoMove() !!!");
        var moveDistance = Time.deltaTime * speed;       

        Ray ray = new Ray(muzzle.transform.position, transform.forward);

        Debug.DrawRay(muzzle.transform.position, transform.forward, Color.red);

        // if you hit a wall, don't move
        if (Physics.Raycast(ray, out hit, lookAheadDistance) && (hit.transform.gameObject.tag == "Wall"))
        {
            // Debug.Log("wall blocking");
        } else { 
            //Debug.Log("setting is walking");

            transform.position += transform.forward * moveDistance;            
        }
    }

    void Update()
    {
        // are we turning ?       
        if (Input.GetKey(KeyCode.RightArrow)) DoTurnRight();
        else if (Input.GetKey(KeyCode.LeftArrow)) DoTurnLeft();

        // if the move key is pressed
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (isMoving == false)
            {
                animator.SetTrigger("isWalking");
                //Debug.Log("setting is walking");
                isMoving = true;
            }
            else DoMove();
        }
        else
        { // no movement
            // still in move stae but no key
            if (isMoving && !Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetTrigger("isIdle");
                //Debug.Log("setting isIdle");
                isMoving = false;
            }
            
        }

    }

} // end of Marine
