using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class CollectibleManager : MonoBehaviour {
    
    private PlatformerCharacter2D platChar;
    public int KurenCs = 0;
    public float smallHealAmount = 10;
    public int coinValue = 1;

    void Start()
    {
        platChar = GetComponent<PlatformerCharacter2D>();
    }
	void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag == "SmallHeal")
        {
            platChar.healDamage(smallHealAmount);
            col.gameObject.SetActive(false);
        } else if (tag == "Coin") 
        {
            addCoin(coinValue);
            col.gameObject.SetActive(false);
        }
    }

    void addCoin(int coinValue)
    {
        KurenCs += coinValue;
    }
    public int getKurenCs()
    {
        return KurenCs;
    }
}
