using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Weapon;

public class Treasure : Item {

    [SerializeField] private int xp;
    [SerializeField] private int points;
    [SerializeField] private Weapon weapon;

    public enum Loot {
        XP,
        POINTS,
        WEAPON,
    };

    [SerializeField] private Loot loot;

    protected override void Interact() {
        if (loot == Loot.POINTS) {
            player.GivePoints(points);
        }
        else if (loot == Loot.XP) {
            player.GiveXp(xp);
        } else {
            player.GiveWeapon(weapon);
        }
    }

}

