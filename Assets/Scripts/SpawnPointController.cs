using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPointController : MonoBehaviour {

    public int levelIndex = -1;
    public int checkLevelIndex;
    public GameObject player;
    public GameObject leftSpawn;
    public GameObject rightSpawn;
	// Use this for initialization
	void Awake () {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        player = GameObject.Find("Player");
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(transform.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        checkLevelIndex = SceneManager.GetActiveScene().buildIndex;
		if (checkLevelIndex != levelIndex)
        {
            getPlayerAndSpawnPoints();
            
            if (levelIndex == 1) // if leaving tutorial level
            {
                if (checkLevelIndex == 2) // traveling from tutorial level to Kuran
                {                    
                    player.transform.position = leftSpawn.transform.position;                    
                }
            } else if (levelIndex == 2) // if leaving Kuran
            {
                if (checkLevelIndex == 1) // traveling from Kuran to tutorial level
                {
                    player.transform.position = rightSpawn.transform.position;
                }
                if (checkLevelIndex == 3) // traveling from Kuran to forrest level
                {
                    player.transform.position = leftSpawn.transform.position;
                }
            } else if (levelIndex == 3) // if leaving Kuran
            {
                if (checkLevelIndex == 2) // traveling from forrest to Kuran
                {
                    player.transform.position = rightSpawn.transform.position;
                }

            } else
            {
                player.transform.position = leftSpawn.transform.position;
            }
            levelIndex = checkLevelIndex;
        }
	}
    void getPlayerAndSpawnPoints ()
    {
        player = GameObject.Find("Player");
        leftSpawn = GameObject.Find("leftSpawn");
        rightSpawn = GameObject.Find("rightSpawn");
    }
}
