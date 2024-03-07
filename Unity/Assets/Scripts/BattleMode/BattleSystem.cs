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
		// PlayerTurn();
	}
}