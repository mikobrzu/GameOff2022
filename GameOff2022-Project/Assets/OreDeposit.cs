using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreDeposit : MonoBehaviour
{
    public float depositHealth;

    public GameObject SmokeBreakEffectPrefab;

    [SerializeField] private int numberOfOres;

    public GameObject OrePrefab;

    [SerializeField] private AudioClip depositDestroyedClip;

    // Start is called before the first frame update
    void Start()
    {
        numberOfOres = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakDeposit(){
        Instantiate(SmokeBreakEffectPrefab, transform.position, Quaternion.identity);
        // Spawn ore
        for (int i = 0; i < numberOfOres; i++){
            GameObject Ore = Instantiate(OrePrefab, transform.position, Quaternion.identity);
            Ore.GetComponent<Ore>().oreType = "Copper";
        }

        SoundManager.Instance.PlaySound(depositDestroyedClip);
        Destroy(gameObject);
    }
}
