using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingScript : MonoBehaviour
{
    [SerializeField]
    float lightingDuration = 0.2f;

    private float lightingDurationTimer = 0;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        LightingUpdate();
    }


    public void SetLightingPosAndSize(Rigidbody ball1, Rigidbody ball2)
    {
        //Debug.Log("Activate lighting");
        gameObject.SetActive(true);
        transform.position = ball1.position + (ball2.position - ball1.position) / 2;
        transform.localScale = new Vector3(1, (ball2.position - ball1.position).magnitude, 1);
        transform.up = ball2.position - ball1.position;
        lightingDurationTimer = lightingDuration;
    }

    void LightingUpdate()
    {
        //Debug.Log("Timer " + lightingDurationTimer);
        lightingDurationTimer -= Time.deltaTime;
        if (lightingDurationTimer < 0)
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
