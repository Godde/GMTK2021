using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    float ballGlowDuration = 0.3f;
    float ballTimer = 0;
    bool effect = false;
    
    [SerializeField]
    Renderer renderer;
    //[SerializeField]
    Material glowMaterial;
    Color startColor;
    [SerializeField]
    AudioSource ribba;
    [SerializeField]
    AudioSource marken;
    [SerializeField]
    AudioSource vaggen;


    // Start is called before the first frame update
    void Start()
    {
        startColor = renderer.sharedMaterial.color;
        glowMaterial = renderer.material;
        glowMaterial.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (effect)
        {
            ballTimer -= Time.deltaTime;
            glowMaterial.color = startColor + new Color(0, 0, 0, 1) * (ballTimer / ballGlowDuration);
            if(ballTimer < 0)
            {
                effect = false;
            }
            
        }
    }

    public void Fading()
    {
        effect = true;
        ballTimer = ballGlowDuration;
    }


}
