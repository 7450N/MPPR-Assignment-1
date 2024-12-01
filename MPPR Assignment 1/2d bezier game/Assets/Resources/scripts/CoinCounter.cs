using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public int coins = 0;
    public Text coinText;

    private void Start()
    {
        
    }
    void Update()
    {
        coinText.text = " X " + coins.ToString();
    }
}
