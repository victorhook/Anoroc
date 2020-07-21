using UnityEngine;
using static Item;

public abstract class Weapon : Item {

    public enum WeaponType {
        Melee,
        Range,
    };

    protected int damage;
    protected int range;
    protected WeaponType type;

    protected abstract void attack();

}