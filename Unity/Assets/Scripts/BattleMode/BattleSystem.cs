using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject playerObj;
	public GameObject enemyObj;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

    public Text dialogueText;

    public Character playerChar;
	public Character enemyChar;
	
	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

    public BattleState state;

    public void Start()
    {
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerObj, playerBattleStation);
		playerChar = playerGO.GetComponent<Character>();

		GameObject enemyGO = Instantiate(enemyObj, enemyBattleStation);
		enemyChar = enemyGO.GetComponent<Character>();

		dialogueText.text = "A wild " + enemyChar.charName + " approaches...";

		playerHUD.SetHUD(playerChar);
		enemyHUD.SetHUD(enemyChar);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator PlayerAttack()
	{
		bool isDead = enemyChar.TakeDamage(playerChar.damage);

		enemyHUD.SetHP(enemyChar.curHp);
		dialogueText.text = "The attack is successful!";

		yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator EnemyTurn()
	{
		dialogueText.text = enemyChar.charName + " attacks!";

		yield return new WaitForSeconds(1f);

		bool isDead = playerChar.TakeDamage(enemyChar.damage);

		playerHUD.SetHP(playerChar.curHp);

		yield return new WaitForSeconds(1f);

		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}

	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
	}

	void PlayerTurn()
	{
		dialogueText.text = "Choose an action:";
	}

	IEnumerator PlayerHeal()
	{
		playerChar.Heal(20);

		playerHUD.SetHP(playerChar.curHp);
		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack());
	}

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal());
	}
}