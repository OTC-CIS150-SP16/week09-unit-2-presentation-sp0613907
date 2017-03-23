using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class GameManager : MonoBehaviour {
    static public GameManager S;

    public GameObject[] EnemyTanks;
    public Vector3 tankPos;

    public bool _________________________;
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject enemyTank;
    public GameMode mode = GameMode.idle;
    public string showing = "Your Tank";

	// Use this for initialization
	void Start () {
        S = this;
        level = 0;
        levelMax = EnemyTanks.Length;
        StartLevel();
	}
	
    void StartLevel()
    {
        if (enemyTank != null)
        {
            Destroy(enemyTank);
        }

        GameObject[] oldShots = GameObject.FindGameObjectsWithTag("Shot");
        foreach (GameObject sTemp in oldShots)
        {
            Destroy(sTemp);
        }

        enemyTank = Instantiate(EnemyTanks[level]) as GameObject;
        enemyTank.transform.position = tankPos;
        shotsTaken = 0;

        SwitchView("Both");
        TankHit.tankDestroyed = false;

        //ShowGT();

        mode = GameMode.playing;
    }

    void Update()
    {
        //ShowGT();
        if (mode == GameMode.playing && TankHit.tankDestroyed)
        {
            mode = GameMode.levelEnd;
            SwitchView("Both");
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel()
    {
        level++;
        if(level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    void OnGUI()
    {
        Rect buttonRect = new Rect((Screen.width / 2) - 50, 10, 100, 24);
        switch (showing)
        {
            case "Your Tank":
                if (GUI.Button(buttonRect, "Show Enemy"))
                {
                    SwitchView("Enemy");
                }
                break;
            case "Enemy":
                if (GUI.Button(buttonRect, "Show Both"))
                {
                    SwitchView("Both");
                }
                break;
            case "Both":
                if (GUI.Button(buttonRect, "Show Your Tank"))
                {
                    SwitchView("Your Tank");
                }
                break; 
        }
    }

    static public void SwitchView( string eView)
    {
        S.showing = eView;
        switch (S.showing)
        {
            case "Your Tank":
                FollowCAM.S.poi = null;
                break;
            case "Enemy":
                FollowCAM.S.poi = S.enemyTank;
                break;
            case "Both":
                FollowCAM.S.poi = GameObject.Find("ViewBoth");
                break;
        }
    }

    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
