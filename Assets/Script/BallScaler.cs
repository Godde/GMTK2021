using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScaler : MonoBehaviour
{
    // Start is called before the first frame update

    //[SerializeField]
    Rigidbody rb;
    Vector3 startingExtraY;

    private void Start()
    {
        startingExtraY = new Vector3(0, transform.localPosition.y);
        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = rb.position + startingExtraY;
        transform.localScale = new Vector3(transform.position.y/2 + 1, transform.position.y/2 + 1, transform.position.y/2 + 1);
        transform.forward = Vector3.up;
    }
}
