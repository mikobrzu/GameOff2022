using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineInteractor : MonoBehaviour
{
    public LayerMask rockLM;
    //UnityEvent onInteract;

    public Animator pickaxeAnimation;

    [SerializeField] private float raycastLength = 2f;

    public AudioClip pickaxeHitClip;

    public CameraShake cameraShake;

    public AnimationCurve pickaxeDamageCurve;
    public float pickaxeBaseDamage;
    public float pickaxeDamage;
    public float pickaxeDamageMultiplier;

    public float timeSinceLastHit;

    public bool crit = false;

    // Start is called before the first frame update
    void Start()
    {
        pickaxeDamage = 100.0f;
        timeSinceLastHit = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hit, raycastLength, rockLM)){
            //Debug.Log(hit.collider.name);
            //if (hit.collider.GetComponent<Interactable>() != false){
                //onInteract = hit.collider.GetComponent<Interactable>().onInteract;
                //if (Input.GetKeyDown(KeyCode.E)){
                    //onInteract.Invoke();
                //}
            //}

            if (Input.GetMouseButtonDown(0)){
                if (hit.collider.GetComponent<OreDeposit>() != false){
                    pickaxeAnimation.SetTrigger("Mine");
                    SoundManager.Instance.PlaySound(pickaxeHitClip);
                    timeSinceLastHit = 0f;
                    StartCoroutine(cameraShake.Shake(.1f, .01f));
                    hit.collider.GetComponent<OreDeposit>().TakeDamage(pickaxeDamage, crit);
                }
            }
        }

        CalculatePickaxeDamage();
        timeSinceLastHit = timeSinceLastHit + Time.deltaTime;
    }

    void CalculatePickaxeDamage(){
        // Normal hit about 70% of damage.
        // Optimal at 110% of damage.
        // Least optimal at 10% damage.
        pickaxeDamageMultiplier = pickaxeDamageCurve.Evaluate(timeSinceLastHit);
        if (pickaxeDamageMultiplier > 1.0f){
            crit = true;
        }
        else{
            crit = false;
        }
        pickaxeDamage = pickaxeBaseDamage * pickaxeDamageMultiplier;
    }
}
