using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string charName;
    public int maxHp;
    public int curHp;
    public int damage; // Consider removing if not used elsewhere
    public int attackMove1Damage;
    public int attackMove2Damage;
    public int attackMove3Damage; // Added third attack move
    
    public bool TakeDamage(int dmg)
    {
        curHp -= dmg;
        return curHp <= 0;
    }

    public void Heal(int amount)
    {
        curHp += amount;
        if (curHp > maxHp)
            curHp = maxHp;
    }

    public bool AttackMove1(Character target)
    {
        return target.TakeDamage(attackMove1Damage);
    }


    public bool AttackMove2(Character target)
    {
        return target.TakeDamage(attackMove2Damage);
    }

    public bool AttackMove3(Character target) // New method for third attack move
    {
        return target.TakeDamage(attackMove3Damage);
    }
}
