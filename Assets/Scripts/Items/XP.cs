using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : Item {

    [SerializeField] private int xp;

    void Start()
    {
        
    }


    protected override void Interact() {
        player.GiveXp(xp);
    }

}
