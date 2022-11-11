using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField] private MeshFilter oreMesh;
    [SerializeField] private Mesh ironMesh;
    [SerializeField] private Mesh copperMesh;

    public string oreType;

    // Ore Attributes
    public float minSize = 0.5f;
    public float maxSize = 2.5f;
    public float size;
    public float weight;
    public float minQuality = 1f;
    public float maxQuality = 5f;
    public float quality;
    public float price;

    public bool inCollectionZone;

    public bool calculatedStats = false;

    // Start is called before the first frame update
    void Start()
    {
        SetMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if (calculatedStats == false){
            CalculateOreStats();
            calculatedStats = true;
        }
    }

    public void CalculateOreStats(){
        CalculateSize();
        CalculateWeight();
        CalculateQuality();
        CalculatePrice();
    }

    public void SetRandomStats(){
        size = Random.Range(minSize, maxSize);
        quality = Random.Range(1,5);
    }

    void SetMesh(){
        if (oreType == "Iron"){
            oreMesh.mesh = ironMesh;
        }
        else if (oreType == "Copper"){
            oreMesh.mesh = copperMesh;
        }
        else{
            oreMesh.mesh = null;
        }
    }

    void CalculateSize(){
        // size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(transform.localScale.x * size, transform.localScale.y * size, transform.localScale.z * size);
    }

    void CalculateWeight(){
        weight = size * 10f;
    }

    void CalculateQuality(){
        //quality = Random.Range(1,5);
    }

    void CalculatePrice(){
        price = quality * weight;
    }

    public void SetSize(float sizeValue){
        transform.localScale = new Vector3(transform.localScale.x * sizeValue, transform.localScale.y * sizeValue, transform.localScale.z * sizeValue);
    }
}
