using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : MonoBehaviour
{
    [SerializeField] private string aPiece;
    [SerializeField] private string aType;
    [SerializeField] private bool lightVarient;

    [SerializeField] private GameObject Helmet_CL;
    [SerializeField] private GameObject Helmet_CH;
    [SerializeField] private GameObject Helmet_IL;
    [SerializeField] private GameObject Helmet_IH;

    [SerializeField] private GameObject Chestplate_CL;
    [SerializeField] private GameObject Chestplate_CH;
    [SerializeField] private GameObject Chestplate_IL;
    [SerializeField] private GameObject Chestplate_IH;

    [SerializeField] private GameObject Leggings_CL;
    [SerializeField] private GameObject Leggings_CH;
    [SerializeField] private GameObject Leggings_IL;
    [SerializeField] private GameObject Leggings_IH;

    [SerializeField] private GameObject Shield_CL;
    [SerializeField] private GameObject Shield_CH;
    [SerializeField] private GameObject Shield_IL;
    [SerializeField] private GameObject Shield_IH;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPrefab();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPrefab(){
        
    }
}
