using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1.25f;
    public float walkSpeed = 1.25f;
    public float runSpeed = 2.25f;
    public float jumpForce = 200;
    public float timeBeforeNextJump = 1.5f;
    private float canJump = 0f;
    Animator anim;
    Rigidbody rb;

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
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = movement.normalized;

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            if (Input.GetButton("Run"))
            {
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
}