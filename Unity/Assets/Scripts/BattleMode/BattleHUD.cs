using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
	public Text nameText;
	public Slider hpSlider;

	public void SetHUD(Character character)
	{
		nameText.text = character.charName;
		hpSlider.maxValue = character.maxHp;
		hpSlider.value = character.curHp;
	}

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}

}