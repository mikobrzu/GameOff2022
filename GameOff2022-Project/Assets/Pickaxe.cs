using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    //[SerializeField]
    //private string currentState = null;

    // Start is called before the first frame update
    void Start()
    {
        //currentState = "";
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState);
    }

    public void HitMiddle(){

    }

    public void EnableStrikeTime(){
        //currentState = "Strike Time";
    }

    public void EnableBestStrikeTime(){
        //currentState = "Best time to strike!";
    }

    public void DisableBestStrikeTime(){
        //currentState = "Strike Time";
    }

    public void DisableStrikeTime(){
        //currentState = "Not time to strike.";
    }
}
