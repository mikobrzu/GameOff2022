using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopHeadboard : MonoBehaviour
{
    [SerializeField] private ShopFloorController SFCRef;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;

    [SerializeField] private Image bar;

    [SerializeField] private float currentXp;
    [SerializeField] private float neededXp;

    // Start is called before the first frame update
    void Start()
    {
        SFCRef = GameObject.Find("ShopFloorController").GetComponent<ShopFloorController>();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Shop Level: " + SFCRef.GetCurrentShopLevel().ToString("F0");

        currentXp = SFCRef.GetCurrentExperience();
        neededXp = SFCRef.GetNeededExperience();

        expText.text = "EXP: " + currentXp.ToString("F0") + " / " + neededXp.ToString("F0");

        if (float.IsNaN(currentXp/neededXp)){
            bar.rectTransform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
        }
        else{
            bar.rectTransform.localScale = new Vector3((currentXp / neededXp), 1.0f, 1.0f);
        }
    }
}
