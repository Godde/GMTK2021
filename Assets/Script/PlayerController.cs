using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Vector3 directionalInput;

    Rigidbody rb;

    [SerializeField]
    Rigidbody ball1;
    [SerializeField]
    Rigidbody ball2;
    [SerializeField]
    LightingScript lighting;
    [SerializeField]
    float kickStrength = 10;
    bool kickedThisFrame = false;
    [SerializeField]
    bool playerTwo = false;
    [SerializeField]
    float acceleration = 1;
    [SerializeField]
    float deccelerationMult = 2;
    [SerializeField]
    float dashExtraSpeed = 10;
    [SerializeField]
    float dashTimer = 0.7f;
    [SerializeField]
    float dashDuration = 0.2f;
    [SerializeField]
    float dashExtraVelocity = 10;
    [SerializeField]
    float magneticStrength = 1;
    [SerializeField]
    float playerReach = 2;
    [SerializeField]
    float maxSpeed = 10;
    [SerializeField]
    Texture shootingTexture;
    Material material;
    Texture defaultTexture;
    float textureChange = 0.2f;
    float textureChangeTimer = 0;

    [SerializeField]
    AudioClip kickSound;
    [SerializeField]
    AudioClip kickMiss;




    private float dashCountdownTimer = 0;
    private float dashDurationTimer = 0;
    private Vector3 lastDashDirection;
    private Vector3 preDashVelocity;
    private BallScript[] ballScripts;



    // Start is called before the first frame update
    void Start()
    {
        int playerNumber = playerTwo ? 2 : 1;
        transform.position = new Vector3(GamestateScript.Instance.PlayerStartPosition(playerNumber), transform.position.y, transform.position.z);
        rb = GetComponent<Rigidbody>();
        material = GetComponentInChildren<Renderer>().material;
        defaultTexture = material.mainTexture;
        ballScripts = FindObjectsOfType<BallScript>();
        //material.mainTexture = (shootingTexture);
    }

    void Lean()
    {
        transform.rotation = Quaternion.Euler(directionalInput);
        //directionalInput
    }

    void Dash()
    {
        if(directionalInput != Vector3.zero && dashCountdownTimer < 0)
        {
            Debug.Log(directionalInput + " DirInput");
            preDashVelocity = rb.velocity;
            Vector3 difference = directionalInput.normalized * (dashExtraSpeed + maxSpeed) - rb.velocity;
            rb.AddForce(difference, ForceMode.VelocityChange);
            dashCountdownTimer = dashTimer;
            dashDurationTimer = dashDuration;
            lastDashDirection = directionalInput;
        }
    }

    void DashUpdate()
    {
        dashDurationTimer -= Time.fixedDeltaTime;
        dashCountdownTimer -= Time.fixedDeltaTime;
        if(lastDashDirection != Vector3.zero && dashDurationTimer < 0)
        {
            DashCancel();
        }
    }

    void TextureUpdate()
    {
        textureChangeTimer -= Time.deltaTime;
        if(textureChangeTimer < 0)
        {
            material.mainTexture = defaultTexture;
            textureChangeTimer = Mathf.Infinity;
        }
    }


    void DashCancel()
    {
        if(Vector3.Dot(rb.velocity, lastDashDirection) > 0)
        {
            if(rb.velocity.magnitude > dashExtraVelocity)
            {
                rb.AddForce(-lastDashDirection.normalized * dashExtraVelocity, ForceMode.VelocityChange);
            }
            else
            {
                rb.AddForce(-lastDashDirection.normalized * rb.velocity.magnitude, ForceMode.VelocityChange);
            }
        }


        lastDashDirection = Vector3.zero;

    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Joystick1Button0)){
        //    Debug.Log("Joystick");
        //}


        if (!playerTwo)
        {
            directionalInput.x = Input.GetAxis("Horizontal");
            directionalInput.z = Input.GetAxis("Vertical");
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                kickedThisFrame = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                Dash();
            }
        }
        else
        {
            directionalInput.x = Input.GetAxis("Horizontal2");
            directionalInput.z = Input.GetAxis("Vertical2");
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.Joystick2Button0))
            {
                kickedThisFrame = true;
            }
            if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.Joystick2Button1))
            {
                Dash();
            }
        }

        TextureUpdate();

    }

    void Kick()
    {
        //Debug.Log("Kick");
        //Debug.Log((rb.position - ball1.position).magnitude);
        //Debug.Log((rb.position - ball2.position).magnitude);
        if ((rb.position - ball1.position).magnitude < playerReach)
        {
            //Vector3 velocityDifference = rb 

            //Vector3 forceApplied = (ball1.position - rb.position).normalized * kickStrength;
            Vector3 forceApplied = (rb.velocity).normalized * kickStrength;
            ball1.AddForce(forceApplied, ForceMode.VelocityChange);
            ball2.AddForce(forceApplied, ForceMode.VelocityChange);
            //lighting.SetLightingPosAndSize(ball1, ball2);
            material.mainTexture = shootingTexture;
            textureChangeTimer = textureChange;
            GamestateScript.Instance.PlayAudioClipWithSound(kickSound, 1);
            foreach(BallScript ball in ballScripts)
            {
                ball.Fading();
            }
        }
        else if((rb.position - ball2.position).magnitude < playerReach)
        {
            //Vector3 forceApplied = (ball2.position - rb.position).normalized * kickStrength;
            Vector3 forceApplied = (rb.velocity).normalized * kickStrength;
            ball1.AddForce(forceApplied, ForceMode.VelocityChange);
            ball2.AddForce(forceApplied, ForceMode.VelocityChange);
            //lighting.SetLightingPosAndSize(ball2, ball1);
            material.mainTexture = shootingTexture;
            textureChangeTimer = textureChange;
            GamestateScript.Instance.PlayAudioClipWithSound(kickSound, 1);
            foreach (BallScript ball in ballScripts)
            {
                ball.Fading();
            }
        }
        else
        {
            GamestateScript.Instance.PlayAudioClipWithSound(kickMiss, 1);
        }
    }





    private void MagneticBall(Rigidbody ball)
    {
        if((rb.position - ball.position).magnitude <= playerReach)
        {
            ball.AddForce((rb.position - ball.position).normalized * magneticStrength, ForceMode.Acceleration);
        }
    }

    private void FixedUpdate()
    {
        if (kickedThisFrame)
        {
            
            Kick();
            kickedThisFrame = false;
        }
        MovementForce();

        //Debug.Log(directionalInput);
        MagneticBall(ball1);
        MagneticBall(ball2);
        DashUpdate();
        //SetLightingPosAndSize();
    }

    private void MovementForce()
    {
        float moveMult = 1;
        if (Vector3.Dot(directionalInput, rb.velocity) < 0)
        {
            moveMult = deccelerationMult;
        }
        rb.AddForce(directionalInput.normalized * acceleration * moveMult, ForceMode.Acceleration);
    }

}
