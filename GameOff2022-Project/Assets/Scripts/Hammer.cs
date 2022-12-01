using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hammer : MonoBehaviour
{
    public Transform hammerTargetPosition;
    public Rigidbody hammerRB;
    public Collider hammerCol;

    public GameObject playerCam;

    public Quaternion hammerTargetRotation;

    public bool hammerEquipped = false;

    public AudioClip hammerSwingSound;
    public AudioClip hammerHitSound;

    public LayerMask constructionLayer;
    [SerializeField] private float raycastLength = 2f;

    [SerializeField] private GameObject UIPrompt;

    [SerializeField] private AudioClip hammerDrop;
    [SerializeField] private AudioClip hammerPickup;
    [SerializeField] private SoundManager SMRef;

    [SerializeField] private CameraShake CamShake;

    [SerializeField] private PlayerController PRef;

    // Start is called before the first frame update
    void Start()
    {
        if (UIPrompt != null){
            UIPrompt.SetActive(false);
        }   

        if (SMRef == null){
            SMRef = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        }

        if (PRef == null && SceneManager.GetActiveScene().name != "Start" && SceneManager.GetActiveScene().name != "GameOver"){
            PRef = GameObject.Find("Player").GetComponent<PlayerController>();
        }
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

                // Check collision with construction zone layer.
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hit, raycastLength, constructionLayer)){
                    //Debug.Log(hit.collider.name);
                    if (hit.collider.GetComponent<AnvilConstructZone>() != false){
                        //Debug.Log(" === HIT CONSTRUCTION ZONE === ");
                        hit.collider.GetComponent<AnvilConstructZone>().ConstructArmourPiece();
                        SoundManager.Instance.PlaySound(hammerHitSound);
                        // Camera Shake
                        if (CamShake != null){
                            StartCoroutine(CamShake.Shake(.3f, .01f));
                        }
                    }
                }
            }
        }
    }

    public void EquipHammer(){
        if (PRef.GetHoldingBP() == false){
            hammerRB.isKinematic = true;
            hammerCol.enabled = false;
            hammerEquipped = true;
            transform.SetParent(playerCam.transform, true);
            gameObject.layer = LayerMask.NameToLayer("Tool");
            foreach (Transform child in transform.GetComponentsInChildren<Transform>()){
                child.gameObject.layer = LayerMask.NameToLayer("Tool");
            }
            UIPrompt.SetActive(true);
            PRef.SetHoldingHammer(true);
            SMRef.PlaySound(hammerPickup);
            //transform.position = new Vector3(0f,0f,0f);
        }
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
        UIPrompt.SetActive(false);
        PRef.SetHoldingHammer(false);
        SMRef.PlaySound(hammerDrop);
    }
}
