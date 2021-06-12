using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScaler : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.position.y/2 + 1, transform.position.y/2 + 1, transform.position.y/2 + 1);
        transform.forward = Vector3.up;
    }
}
