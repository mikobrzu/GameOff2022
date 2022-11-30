using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CounterBoard : MonoBehaviour
{
    [SerializeField] private ShopFloorController SFCRef;

    [SerializeField] private TextMeshProUGUI customersServedText;
    [SerializeField] private TextMeshProUGUI customersWaitTimeText;
    [SerializeField] private TextMeshProUGUI customersOrderDifficultyText;
    [SerializeField] private TextMeshProUGUI customersNextText;

    // Start is called before the first frame update
    void Start()
    {
        SFCRef = GameObject.Find("ShopFloorController").GetComponent<ShopFloorController>();
    }

    // Update is called once per frame
    void Update()
    {
        customersNextText.text = "New customer every " + SFCRef.GetMinPossibleWaitTime().ToString("F0") + " - " + SFCRef.GetMaxPossibleWaitTime().ToString("F0") + "s.";
        customersOrderDifficultyText.text = "Possible customer order difficulty: Level " + SFCRef.GetCustomerMaxDifficulty().ToString("F0");
        customersServedText.text = "Total served: " + SFCRef.GetTotalServed().ToString("F0");
        customersWaitTimeText.text = "Customer wait time: " + SFCRef.GetCustomerOrderMinWaitTime().ToString("F0") + " - " + SFCRef.GetCustomerOrderMaxWaitTime().ToString("F0") + "s.";
    }
}
