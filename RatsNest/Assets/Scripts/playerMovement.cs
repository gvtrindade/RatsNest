using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    private Vector3 moveDirection;
    public Animator animator;
    private float jumpStart;


    public CharacterController charController;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        animator.SetFloat("vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("horizontal", Input.GetAxis("Horizontal"));
        
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (charController.isGrounded)
        {
            jumpStart = 0f;
            animator.SetBool("isGrounded", true);  
            moveDirection.y = 0f;
            if (Input.GetButton("Jump"))
            {
                jumpTimer();
                animator.SetBool("isGrounded", false);
                moveDirection.y = jumpForce;
            }
        }

        animator.SetFloat("timeOfJump", jumpStart);

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

        // Move the controller
        charController.Move(moveDirection * Time.deltaTime);
    }

    void jumpTimer()
    {
        while(!charController.isGrounded)
        {
            jumpStart += Time.deltaTime;
        }
    }
}
