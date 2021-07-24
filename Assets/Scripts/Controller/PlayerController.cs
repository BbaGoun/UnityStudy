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

    Rigidbody rb;
    public Camera mainCamera;
    PlayerAnimator playerAnim;
    CharacterCombat combat;

    public Interactable nearInteractable;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<PlayerAnimator>();
        combat = GetComponent<CharacterCombat>();
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
        LookMouseCursor();

        if (movement != Vector3.zero)
        {
            float timeSinceLastMove = Time.time - lastMoveTime;
            lastMoveTime = Time.time;

            if (isRun || timeSinceLastMove <= Double_Tap_Time && oldMovement == Vector3.zero)
            {
                isRun = true;
                movementSpeed = runSpeed;
                playerAnim.Run();
            }
            else
            {
                movementSpeed = walkSpeed;
                playerAnim.Walk();
            }

            transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        }
        else {
            isRun = false;
            playerAnim.Idle();
        }

        if (Input.GetButtonDown("Jump") && Time.time > canJump)
        {
            rb.AddForce(0, jumpForce, 0);
            canJump = Time.time + timeBeforeNextJump;
            playerAnim.Jump();
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

        if(Input.GetButtonDown("Fire1"))
        {
            combat.Attack();
        }
    }

    void LookMouseCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;
        if (Physics.Raycast(ray, out hitResult))
        {
            Vector3 mouseDir = new Vector3(hitResult.point.x, transform.position.y, hitResult.point.z) - transform.position;
            transform.forward = mouseDir;
        }
    }
}