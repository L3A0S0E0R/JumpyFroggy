using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRotate : MonoBehaviour
{
    private Quaternion startRotationPos = Quaternion.Euler(0, 0, 0);
    private Quaternion lastRotationPos = Quaternion.Euler(-60, 0, 0);
    private PlayerMovement playerMovementScript;
    
    public float isCounterClockwise = 1;
    public float rotationSpeed;
    public float scalingSpeed;

    public float isIncreases = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();

        Scaling();
        
    }

    // Rotate arrow aroung player for giving jump direction
    void Rotate()
    {
        transform.Rotate(Vector3.left * isCounterClockwise, rotationSpeed * Time.deltaTime);
        playerMovementScript.jumpDirRotation = transform.rotation.eulerAngles;

        // First and second 'ifs' change clockwise
        if (transform.rotation.x <= lastRotationPos.x)
        {
            isCounterClockwise = -1;
        }
        else if (transform.rotation.x >= startRotationPos.x)
        {
            isCounterClockwise = 1;
        }
        // stoping arrow when player jumps and scales its
        else if (playerMovementScript.isJumped || playerMovementScript.isScaling)
        {
            isCounterClockwise = 0;
        }
        // Prefer don't moving arrow bugs with no other interactions
        else if (!playerMovementScript.isJumped && !playerMovementScript.isScaling && isCounterClockwise == 0)
        {
            isCounterClockwise = 1;
            gameObject.SetActive(true);
        }
    }

    // Scale the arrow for giving more or less force to jump
    void Scaling()
    {
        if (playerMovementScript.isScaling)
        {
            // Adding/decreasing force step by step
            var scale = transform.localScale.z + (Time.deltaTime * scalingSpeed * isIncreases);
            transform.localScale = new Vector3(1.25f, 1.25f, scale);
            playerMovementScript.jumpDirForce = transform.localScale.z;

            // Change the scaling and stops when jump
            if (transform.localScale.z <= 0.75f)
            {
                isIncreases = 1;
            }
            else if (transform.localScale.z >= 2f)
            {
                isIncreases = -1;
            }
            else if (playerMovementScript.isJumped)
            {
                isIncreases = 0;
                gameObject.SetActive(false);
            }
        }
        // Normalize arrow after scaling
        else if (!playerMovementScript.isScaling)
        {
            transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }
    }
}
