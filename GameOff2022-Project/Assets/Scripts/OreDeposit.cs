using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreDeposit : MonoBehaviour
{
    public float depositHealth = 100.0f;

    public GameObject SmokeBreakEffectPrefab;

    [SerializeField] private int numberOfOres;

    public GameObject OrePrefab;

    [SerializeField] private AudioClip depositDestroyedClip;

    public CameraShake cameraShake;

    public GameObject damageText;

    public GameObject[] ironDepositObjects;
    public GameObject[] copperDepositObjects;

    public string depositType;

    // Start is called before the first frame update
    void Start()
    {
        numberOfOres = Random.Range(1, 5);

        // Disable any deposit prefabs active.
        for (int i = 0; i >= ironDepositObjects.Length; i++){
            ironDepositObjects[i].SetActive(false);
        }
        for (int i = 0; i >= copperDepositObjects.Length; i++){
            copperDepositObjects[i].SetActive(false);
        }

        DetermineDepositType();
    }

    // Update is called once per frame
    void Update()
    {
        if (depositHealth < 0.0f){
            BreakDeposit();
        }

        int randomGO = Random.Range(0, ironDepositObjects.Length);
        //Debug.Log(randomGO);
    }

    public void BreakDeposit(){
        Instantiate(SmokeBreakEffectPrefab, transform.position, Quaternion.identity);
        // Spawn ore
        for (int i = 0; i < numberOfOres; i++){
            GameObject Ore = Instantiate(OrePrefab, transform.position, Quaternion.identity);
            Ore.GetComponent<Ore>().oreType = depositType;
            Ore.GetComponent<Ore>().SetRandomStats();
        }

        SoundManager.Instance.PlaySound(depositDestroyedClip);
        StartCoroutine(cameraShake.Shake(.3f, .01f));
        Destroy(gameObject);
    }

    public void TakeDamage(float damageToDeposit, bool crit){
        depositHealth = depositHealth - damageToDeposit;
        DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        if (crit == true){
            indicator.crit = true;
        }
        else if (crit == false){
            indicator.crit = false;
        }
        indicator.SetDamageText(damageToDeposit);
    }

    public void DetermineDepositType(){
        int randomInt = Random.Range(0,2);
        if (randomInt == 1){
            depositType = "Iron";
            ironDepositObjects[Random.Range(0, ironDepositObjects.Length)].SetActive(true);
        }
        else{
            depositType = "Copper";
            copperDepositObjects[Random.Range(0, copperDepositObjects.Length)].SetActive(true);
        }

        Debug.Log(ironDepositObjects.Length);
    }
}
