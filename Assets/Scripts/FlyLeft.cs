using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyLeft : MonoBehaviour
{
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Bird flying
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
