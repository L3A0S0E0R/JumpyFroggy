using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isJumped = false;
    public bool isGrounded = true;
    public bool isTargetChased = false;
    public bool isLevelUpdated = true;
    public bool isScaling = false;
    public Vector3 jumpDirRotation;
    public float jumpDirForce;
    public float jumpForce;

    private Animator playerAnim;
    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        CheckOutOfBound();

        playerAnim.SetFloat("velocityChange", playerRb.velocity.y);
    }

    // Prefer go out of bound for player
    private void CheckOutOfBound()
    {
        if (transform.position.y >= 11.5f || transform.position.z > 27f)
        {
            isJumped = false;

            transform.position = new Vector3(8.8f, 2.1f, 0f);
            playerRb.velocity = Vector3.zero;
        }
    }

    private void Jump()
    {
        // First step for jump - start scaling
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isScaling = true;
        }

        // When key up, start jump
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isScaling = false;
            isJumped = true;
            isGrounded = false;

            playerAnim.SetBool("isJumping", true);

            var temp = Mathf.Sin(jumpDirRotation.x * Mathf.Deg2Rad);
            temp = Mathf.Abs(temp);
            jumpForce = jumpDirForce * 60;
            var upForce = jumpForce * temp * Vector3.up;
            var forwardForce = (1 - temp) * jumpForce * Vector3.forward;
            playerRb.AddForce(upForce + forwardForce, ForceMode.Impulse);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        playerAnim.SetBool("isJumping", false);

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumped = false;
        }
        else if (collision.gameObject.CompareTag("Water"))
        {
            isJumped = false;

            transform.position = new Vector3(8.8f, 2.1f, 0f);
            playerRb.velocity = Vector3.zero;
        }
        else if (collision.gameObject.CompareTag("Target"))
        {


            isTargetChased = true;
            collision.gameObject.tag = "Ground";
            playerRb.velocity = Vector3.zero;
            transform.position = collision.transform.position + new Vector3(0, 0.1f, 0);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }

}
