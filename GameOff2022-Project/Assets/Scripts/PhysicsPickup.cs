using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPickup : MonoBehaviour
{
    [SerializeField] private LayerMask PickupMask;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform PickupTarget;
    [Space]
    [SerializeField] private float PickupRange;
    private Rigidbody CurrentObject;

    [SerializeField] private GameObject Player;

    [SerializeField] private QualityCheckMachine QCM;
    [SerializeField] private SoundManager SMRef;
    [SerializeField] private AudioClip dropAC;
    [SerializeField] private AudioClip pickupAC;

    private void Start(){
        SMRef = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.E)){
            if (CurrentObject){
                
                if (CurrentObject.GetComponent<ArmourPiece>() != null){
                    CurrentObject.GetComponent<ArmourPiece>().SetBeingHeld(false);
                }

                //Debug.Log(CurrentObject);
                if (CurrentObject.GetComponent<ArmourPiece>() != null && QCM != null){
                    if (QCM.GetPlayerInZone() == true && QCM.GetNumberOfPiecesInCheckZone() == 0){
                        if (CurrentObject.GetComponent<ArmourPiece>().GetArmourPieceString() == "Helmet"){
                            CurrentObject.transform.position = new Vector3(-9.219f, 1.047061f, -10.94439f);
                            Debug.Log("Placed helmet.");
                        //CurrentObject.transform.rotation = new Quaternion();
                        }
                        else if (CurrentObject.GetComponent<ArmourPiece>().GetArmourPieceString() == "Shield"){
                            CurrentObject.transform.position = new Vector3(-9.225f, 0.847f, -10.872f);
                            Debug.Log("Placed shield.");
                        }
                        else if (CurrentObject.GetComponent<ArmourPiece>().GetArmourPieceString() == "Leggings"){
                            CurrentObject.transform.position = new Vector3(-9.213f, 1.244f, -10.90586f);
                            Debug.Log("Placed leggings.");
                        }
                        else if (CurrentObject.GetComponent<ArmourPiece>().GetArmourPieceString() == "Chestplate"){
                            CurrentObject.transform.position = new Vector3(-9.225f, 1.385613f, -10.875f);
                            Debug.Log("Placed chestplate.");
                        }
                    }
                    Debug.Log("Dropped armour piece object.");
                }

                CurrentObject.useGravity = true;
                Physics.IgnoreCollision(CurrentObject.GetComponent<Collider>(), Player.GetComponent<Collider>(), false);
                SMRef.PlaySound(dropAC);
                CurrentObject = null;
                return;
            }

            Ray CameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
            if (Physics.Raycast(CameraRay, out RaycastHit HitInfo, PickupRange, PickupMask)){
                CurrentObject = HitInfo.rigidbody;
                CurrentObject.useGravity = false;
                SMRef.PlaySound(pickupAC);
                Physics.IgnoreCollision(CurrentObject.GetComponent<Collider>(), Player.GetComponent<Collider>(), true);
                if (CurrentObject.GetComponent<ArmourPiece>() != null){
                    CurrentObject.GetComponent<ArmourPiece>().SetBeingHeld(true);
                }
            }
        }
    }

    void FixedUpdate(){
        if (CurrentObject){
            Vector3 DirectionToPoint = PickupTarget.position - CurrentObject.position;
            float DistanceToPoint = DirectionToPoint.magnitude;

            CurrentObject.velocity = DirectionToPoint * 12f * DistanceToPoint;
        }
    }
}
