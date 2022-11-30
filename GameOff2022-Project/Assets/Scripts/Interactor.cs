using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Interactor : MonoBehaviour
{
    public LayerMask interactableLM;
    UnityEvent onInteract;

    [SerializeField] private float raycastLength = 2f;

    [SerializeField] private TextMeshProUGUI interactorUIText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hit, raycastLength, interactableLM)){
            //Debug.Log(hit.collider.name);
            //interactorUIText.text = hit.collider.name;
            if (hit.collider.name == "GoMiningCollider"){
                interactorUIText.text = "Go Mining (E)";
            }
            else if (hit.collider.name == "Hammer"){
                interactorUIText.text = "Use Hammer (E)";
            }
            else if (hit.collider.name == "SmeltLever"){
                interactorUIText.text = "Pull Lever (E)";
            }
            else if (hit.collider.name == "QCheckLever"){
                interactorUIText.text = "Pull Lever (E)";
            }
            else if (hit.collider.name == "MineExit"){
                interactorUIText.text = "Go Home (E)";
            }
            else{
                interactorUIText.text = "";
            }

            if (hit.collider.GetComponent<Interactable>() != false){
                onInteract = hit.collider.GetComponent<Interactable>().onInteract;
                if (Input.GetKeyDown(KeyCode.E)){
                    onInteract.Invoke();
                }
            }
        }
        else{
            interactorUIText.text = "";
        }
    }
}
