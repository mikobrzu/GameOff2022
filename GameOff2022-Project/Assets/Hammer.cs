using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public Transform hammerTargetPosition;
    public Rigidbody hammerRB;
    public Collider hammerCol;

    public GameObject playerCam;

    public Quaternion hammerTargetRotation;

    public bool hammerEquipped = false;

    public AudioClip hammerSwingSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hammerEquipped == true){
            if (Input.GetKeyDown(KeyCode.Q)){
                UnequipHammer();
            }

            transform.position = hammerTargetPosition.transform.position;
            transform.rotation = hammerTargetPosition.transform.rotation;

            if (Input.GetMouseButtonDown(0)){
                hammerTargetPosition.GetComponent<Animator>().SetTrigger("HammerHit");
                SoundManager.Instance.PlaySound(hammerSwingSound);
            }
        }
    }

    public void EquipHammer(){
        hammerRB.isKinematic = true;
        hammerCol.enabled = false;
        hammerEquipped = true;
        transform.SetParent(playerCam.transform, true);
        gameObject.layer = LayerMask.NameToLayer("Tool");
        foreach (Transform child in transform.GetComponentsInChildren<Transform>()){
            child.gameObject.layer = LayerMask.NameToLayer("Tool");
        }
        //transform.position = new Vector3(0f,0f,0f);
    }

    public void UnequipHammer(){
        hammerEquipped = false;
        hammerRB.isKinematic = false;
        hammerCol.enabled = true;
        transform.parent = null;
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        foreach (Transform child in transform.GetComponentsInChildren<Transform>()){
            child.gameObject.layer = LayerMask.NameToLayer("Interactable");
        }
    }
}
