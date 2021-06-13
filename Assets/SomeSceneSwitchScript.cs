using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeSceneSwitchScript : MonoBehaviour
{
   
    public void Exit()
    {
        GamestateScript.Exit();
    }
    public void NextScene()
    {
        GamestateScript.NextScene();
    }
}
