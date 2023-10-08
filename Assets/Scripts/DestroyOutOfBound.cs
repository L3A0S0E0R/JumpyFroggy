using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBound : MonoBehaviour
{
    private float leftBound = -5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy object have passed the display
        if (transform.position.z <= leftBound)
        {
            Destroy(gameObject);
        }
    }
}
