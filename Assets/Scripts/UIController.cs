using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;
public class UIController : MonoBehaviour {

    public Text HealthText;
    public Text KurenCText;
    private PlatformerCharacter2D pc2d;
    private CollectibleManager cm;
    private GameObject player;
    public Slider HealthSlider;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        pc2d = player.GetComponent<PlatformerCharacter2D>();
        cm = player.GetComponent<CollectibleManager>();
	}
	
	// Update is called once per frame
	void Update () {
        HealthText.text = "Health: " + pc2d.getHealth();
        KurenCText.text = "Kuren C's: " + cm.getKurenCs();
        HealthSlider.value = pc2d.getHealth() / pc2d.getMaxHealth();
	}
}
