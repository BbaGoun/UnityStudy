using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1.25f;
    const float walkSpeed = 1.25f;
    const float runSpeed = 2.25f;
    const float jumpForce = 200;
    const float timeBeforeNextJump = 1.5f;
    const float Double_Tap_Time = 0.15f;
   
    float canJump = 0f;
    float lastMoveTime = 0f;
    bool isRun = false;
    Vector3 movement = Vector3.zero;
    Vector3 oldMovement = Vector3.zero;

    Animator anim;
    Rigidbody rb;
    public Camera mainCamera;

    public Interactable nearInteractable;
    
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ControllPlayer();
    }

    void ControllPlayer()
    {
        oldMovement = movement;
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (movement != Vector3.zero)
        {
            float timeSinceLastMove = Time.time - lastMoveTime;
            lastMoveTime = Time.time;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            
            if (isRun || timeSinceLastMove <= Double_Tap_Time && oldMovement == Vector3.zero)
            {
                isRun = true;
                movementSpeed = runSpeed;
                anim.SetBool("Run", true);
            }
            else
            {
                movementSpeed = walkSpeed;
                anim.SetBool("Run", false);
                anim.SetBool("Walk", true);
            }
        }
        else {
            isRun = false;
            movementSpeed = walkSpeed;
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
        }

        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

        if (Input.GetButtonDown("Jump") && Time.time > canJump)
        {
            rb.AddForce(0, jumpForce, 0);
            canJump = Time.time + timeBeforeNextJump;
            anim.SetTrigger("Jump");
        }

        if(Input.GetButtonDown("Interact"))
        {
            if(nearInteractable != null)
            {
#if UNITY_EDITOR
                Debug.Log($"Interact : {nearInteractable.name}");
#endif
                nearInteractable.Interact();
            }
        }
    }

    //void LookMouseCursor()
    //{
    //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hitResult;
    //    if (Physics.Raycast(ray, out hitResult))
    //    {
    //        Vector3 mouseDir = new Vector3(hitResult.point.x, transform.position.y, hitResult.point.z) - transform.position;
    //        anim.transform.forward = mouseDir;
    //    }
    //}
}