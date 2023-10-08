using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerMovement playerMovementScript;

    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // After moving all objects, check the position of player and stop updating
        if (Mathf.Approximately(playerMovementScript.gameObject.transform.position.z, 0))
        {
            playerMovementScript.isLevelUpdated = true;
        }
        else if (playerMovementScript.gameObject.transform.position.z < 0)
        {
            playerMovementScript.gameObject.transform.position = new Vector3(playerMovementScript.gameObject.transform.position.x, playerMovementScript.gameObject.transform.position.y, 0);
        }
        // Moves all objects
        if (!playerMovementScript.isLevelUpdated)
        {
            //transform.Translate(speed * Time.deltaTime * Vector3.back);
            transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }
    }
}
