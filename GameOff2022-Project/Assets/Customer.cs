using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private bool atPosition = false;
    [SerializeField] private float customerSpeed;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform spawnerTransform;

    // Start is called before the first frame update
    void Start()
    {
        spawnerTransform = GameObject.Find("CustomerSpawnLocation(Clone)").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == targetTransform.position){
            atPosition = true;
        }
        else{
            atPosition = false;
        }

        if (atPosition == false){
            MoveToSlot();
        }
    }

    public void MoveToSlot(){
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, customerSpeed * Time.deltaTime);
    }

    public void SetTargetTransform(Transform tTransform){
        targetTransform = tTransform;
    }

    public void Served(){
        SetTargetTransform(spawnerTransform);
        //Destroy(gameObject);
    }
}
