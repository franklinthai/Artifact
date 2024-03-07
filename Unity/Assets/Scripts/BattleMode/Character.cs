using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string charName;
    public int maxHp;
    public int curHp;
    
    public int damage;
    
    // Returns true if char dead, false otherwise
    public bool TakeDamage(int dmg)
	{
        curHp -= dmg;
        if (curHp <= 0)
			return true;
		else
			return false;
    }

    public void Heal(int amount)
	{
		curHp += amount;
		if (curHp > maxHp)
			curHp = maxHp;
	}
}