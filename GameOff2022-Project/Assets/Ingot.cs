using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingot : MonoBehaviour
{
    [SerializeField] private MeshFilter ingotMesh;
    [SerializeField] private Mesh ironMesh;
    [SerializeField] private Mesh copperMesh;

    public string ingotType;

    // Attributes
    //public float minSize = 0.5f;
    //public float maxSize = 2.5f;
    public float size;
    public float weight;
    //public float minQuality = 1f;
    //public float maxQuality = 5f;
    public float quality;
    public float price;

    // Start is called before the first frame update
    void Start()
    {
        SetMesh();

        //CalculateSize();
        //CalculateWeight();
        //CalculateQuality();
        //CalculatePrice();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetMesh(){
        if (ingotType == "Iron"){
            ingotMesh.mesh = ironMesh;
        }
        else if (ingotType == "Copper"){
            ingotMesh.mesh = copperMesh;
        }
        else{
            ingotMesh.mesh = null;
        }
    }

    //void CalculateSize(){
    //    size = Random.Range(minSize, maxSize);
    //    transform.localScale = new Vector3(transform.localScale.x * size, transform.localScale.y * size, transform.localScale.z * size);
    //}

    //void CalculateWeight(){
    //    weight = size * 10f;
    //}

    //void CalculateQuality(){
    //    quality = Random.Range(1,5);
    //}

    //void CalculatePrice(){
    //    price = quality * weight;
    //}
}
