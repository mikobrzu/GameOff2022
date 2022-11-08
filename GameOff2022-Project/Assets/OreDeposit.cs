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

    // Start is called before the first frame update
    void Start()
    {
        numberOfOres = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (depositHealth < 0.0f){
            BreakDeposit();
        }
    }

    public void BreakDeposit(){
        Instantiate(SmokeBreakEffectPrefab, transform.position, Quaternion.identity);
        // Spawn ore
        for (int i = 0; i < numberOfOres; i++){
            GameObject Ore = Instantiate(OrePrefab, transform.position, Quaternion.identity);
            Ore.GetComponent<Ore>().oreType = "Copper";
            Ore.GetComponent<Ore>().CalculateOreStats();
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
}
