using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 directionalInput;

    Rigidbody rb;

    [SerializeField]
    Rigidbody ball1;
    [SerializeField]
    Rigidbody ball2;
    [SerializeField]
    float kickStrength = 10;
    bool kickedThisFrame = false;
    [SerializeField]
    bool playerTwo = false;
    [SerializeField]
    float acceleration = 1;
    [SerializeField]
    float deccelerationMult = 2;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Lean()
    {
        transform.rotation = Quaternion.Euler(directionalInput);
        //directionalInput
    }

    // Update is called once per frame
    void Update()
    {

        if (!playerTwo)
        {
            directionalInput.x = Input.GetAxis("Horizontal");
            directionalInput.z = Input.GetAxis("Vertical");
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftControl))
            {
                kickedThisFrame = true;
            }
        }
        else
        {
            directionalInput.x = Input.GetAxis("Horizontal2");
            directionalInput.z = Input.GetAxis("Vertical2");
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.RightShift))
            {
                kickedThisFrame = true;
            }
        }
        //Lean();
    }

    void Kick()
    {
        Debug.Log("Kick");
        Debug.Log((rb.position - ball1.position).magnitude);
        Debug.Log((rb.position - ball2.position).magnitude);
        if ((rb.position - ball1.position).magnitude < 2)
        {
            //Vector3 velocityDifference = rb 

            //Vector3 forceApplied = (ball1.position - rb.position).normalized * kickStrength;
            Vector3 forceApplied = (rb.velocity).normalized * kickStrength;
            ball1.AddForce(forceApplied, ForceMode.VelocityChange);
            ball2.AddForce(forceApplied, ForceMode.VelocityChange);
        }
        else if((rb.position - ball2.position).magnitude < 2)
        {
            //Vector3 forceApplied = (ball2.position - rb.position).normalized * kickStrength;
            Vector3 forceApplied = (rb.velocity).normalized * kickStrength;
            ball1.AddForce(forceApplied, ForceMode.VelocityChange);
            ball2.AddForce(forceApplied, ForceMode.VelocityChange);
        }
    }


    private void FixedUpdate()
    {
        if (kickedThisFrame)
        {
            Kick();
            kickedThisFrame = false;
        }
        float moveMult = 1;
        if(Vector3.Dot(directionalInput, rb.velocity) < 0)
        {
            moveMult = deccelerationMult;
        }
        rb.AddForce(directionalInput.normalized *acceleration*moveMult, ForceMode.Acceleration);
        //Debug.Log(directionalInput);
    }
}
