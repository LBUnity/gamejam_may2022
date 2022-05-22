using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float jumpSpeed = 5F;
    [SerializeField] float climbSpeed = 1F;
    [SerializeField] Transform gun;
    Vector2 moveInput;
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCapsuleCollider2D;
    BoxCollider2D playerFeetBoxCollider2D;
    float gravityScaleAtStart;
    bool isAlive = true;



    private void Awake()
    {
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerBodyCapsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        playerFeetBoxCollider2D = gameObject.GetComponent <BoxCollider2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        gravityScaleAtStart = playerRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Run();
        FlipSprite();

        ClimbLadder();

        Die();
        
    }

    private void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", hasHorizontalSpeed);

        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f);

        }

    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;
    }

    private void Jumping()
    {
        bool hasVerticalSpeed = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("isJumping", hasVerticalSpeed);
    }


    void ClimbLadder()
    {
        int layerMask = LayerMask.GetMask("Ladder");

        if (playerFeetBoxCollider2D.IsTouchingLayers(layerMask))
        {
            playerRigidBody.gravityScale = 0;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, moveInput.y * climbSpeed);

            bool verticalSpeed = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
            playerAnimator.SetBool("isClimbing", verticalSpeed);
            
        }
        else
        {
            playerRigidBody.gravityScale = gravityScaleAtStart;
        }
    }


    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        int layerMask = LayerMask.GetMask("Ground");

        if(playerFeetBoxCollider2D.IsTouchingLayers(layerMask) && value.isPressed)
        {
            playerRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }

    }

    void OnFire(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        GameObject bullObj = Instantiate(bullet, gun.position, transform.transform.rotation);

    }

    void Die()
    {
        if (playerBodyCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            if (isAlive)
            {
                playerRigidBody.velocity += new Vector2(-playerRigidBody.velocity.x * 2, jumpSpeed);
            }
            isAlive = false;
            playerAnimator.SetTrigger("Dying");

            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
 
    }
}
