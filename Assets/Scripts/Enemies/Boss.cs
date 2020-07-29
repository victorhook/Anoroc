using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public Text hpText;
    public Text infoText;
    public Enemy boss;
    public GameObject minion;
    public GameFinishedMenu gameFinishedMenu;

    private bool minionsSpawned;
    private int minionsDead;
    public int MINIONS_TO_SPAWN = 5;
    public int MINIONS_HP = 10;
    public int MINIONS_DAMAGE = 50;

    private float msCounter;
    private int seconds;

    void Start() {
        DisplayText("The bat that spread the Covid-19 has appeared!");
        minionsSpawned = false;
        minionsDead = 0;
    }

    void Update() {
        int hitpoints = boss.GetHitpoints();

        if (hitpoints <= 0) {
            gameFinishedMenu.Show();
        }

        hpText.text = string.Format("BOSS HP: {0}", hitpoints);

        if (hitpoints < 100) {
            if (!minionsSpawned) {
                for (int i = 0; i < MINIONS_TO_SPAWN; i++) {
                    GameObject obj = Instantiate(minion, transform.position, Quaternion.identity) as GameObject;
                    Enemy minionSpawned = obj.gameObject.GetComponent<Enemy>();
                    minionSpawned.canMove = true;
                    minionSpawned.SetDamage(MINIONS_DAMAGE);
                    minionSpawned.SetHitpoints(MINIONS_HP);
                }
                minionsSpawned = true;
                DisplayText("The bat made some babies!");
            }
        }

        msCounter += Time.deltaTime;

        if (msCounter >= 1) {
            seconds++;
            msCounter = 0;
        }

        if (seconds > 3) {
            infoText.text = "";
        }

    }

    private void DisplayText(string text) {
        infoText.text = text;
        seconds = 0;
    }
}
