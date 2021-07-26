using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    float movementSpeed = 1.25f;
    const float walkSpeed = 1.25f;
    const float runSpeed = 2.25f;
    const float jumpForce = 200;
    const float dodgeSpeed = 3f;
    const float dodgeDuration = 0.5f;
    const float timeBeforeNextJump = 1.5f;
    const float Double_Tap_Time = 0.15f;
    public float stiffTime = 0.5f;
   
    float canJump = 0f;
    float lastMoveTime = 0f;
    bool isRun = false;
    Vector3 movement = Vector3.zero;
    Vector3 oldMovement = Vector3.zero;

    public Rigidbody rb;
    public Camera mainCamera;
    public PlayerAnimator playerAnim;
    public CharacterCombat combat;

    bool isHitted = false;
    bool isDodge = false;

    Interactable nearInteractable;

    private void OnEnable()
    {
        combat.OnHitted += OnHitted;
        combat.OnDie += OnDie;
    }

    void Update()
    {
        ControllPlayer();
    }

    void ControllPlayer()
    {
        if (isHitted)
            return;
        else if (isDodge)
            return;

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
            if (IsPointerOverUIElement())
                return;
            combat.Attack();
        }

        if(Input.GetButtonDown("Dodge"))
        {
            if (IsPointerOverUIElement())
                return;
            StartCoroutine(Dodge());
        }
    }

    IEnumerator Dodge()
    {
        playerAnim.Dodge();
        isDodge = true;
        float delta = 0.01f;
        combat.OnInvincible();
        for (float t = 0f; t < dodgeDuration; t += delta)
        {
            transform.Translate(movement * dodgeSpeed * delta, Space.World);
            yield return new WaitForSeconds(delta);
        }
        isDodge = false;
        combat.OffInvincible();
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

    public void DetectInteractable(Interactable it)
    {
        nearInteractable = it;
    }

    public void OnHitted()
    {
        isHitted = true;
        StartCoroutine(Delay(stiffTime));
    }

    IEnumerator Delay(float stiffTime)
    {
        yield return new WaitForSeconds(stiffTime);
        isHitted = false;
    }

    void OnDie()
    {
#if UNITY_EDITOR
        Debug.Log("Player is dead");
#endif
        StartCoroutine(ReturnObject());
    }

    IEnumerator ReturnObject()
    {
        this.enabled = false;
        yield return new WaitForSeconds(3f);
        this.enabled = true;
        this.transform.position = Player.Instance.spawnPoint;
        Player.Instance.stat.Respawn();
        playerAnim.Revive();
    }

    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach(RaycastResult raycastResult in raycastResults)
        {
            if (raycastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }
}