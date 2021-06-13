using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueAdder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddTorque(new Vector3(0.5f, 0, 0), ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddTorque(new Vector3(0.5f, 0, 0), ForceMode.VelocityChange);
    }
}
