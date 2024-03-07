using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public Text dialogueText;

    Character player;
	Character enemy;
    public BattleState state;

    void Start()
    {
		state = BattleState.START;
		// StartCoroutine(SetupBattle());
    }

    // IEnumerator SetupBattle()
	// {
	// 	GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
	// 	playerUnit = playerGO.GetComponent<Unit>();

	// 	GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
	// 	enemyUnit = enemyGO.GetComponent<Unit>();

	// 	dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

	// 	playerHUD.SetHUD(playerUnit);
	// 	enemyHUD.SetHUD(enemyUnit);

	// 	yield return new WaitForSeconds(2f);

	// 	state = BattleState.PLAYERTURN;
	// 	PlayerTurn();
	// }
}