using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsScript : MonoBehaviour
{
    [SerializeField] private GameObject Footstep;
    [SerializeField] private PlayerController PCRef;

    // Start is called before the first frame update
    void Start()
    {
        PCRef = GameObject.Find("Player").GetComponent<PlayerController>();
        Footstep.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PCRef.GetWalking() == true){
            StartFootsteps();
        }
        else {
            StopFootsteps();
        }
    }

    void StartFootsteps()
    {
        Footstep.SetActive(true);
    }

    void StopFootsteps()
    {
        Footstep.SetActive(false);
    }
}
