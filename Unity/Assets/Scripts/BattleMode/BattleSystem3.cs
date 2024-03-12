using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BattleSystem3 : MonoBehaviour
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

    public GameObject button;

    public BattleState state;

	public Button attackMove1Button;
    public Button attackMove2Button;
    public Button attackMove3Button;
    public Button healButton;
    public void Start()
	{
        state = BattleState.START;
		StartCoroutine(SetupBattle());
		button.gameObject.SetActive(false);
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

	void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        PlayerPrefs.DeleteKey("PlayerPosZ");
		PlayerPrefs.DeleteKey("level");
    }

    void PlayerTurn()
    {
        dialogueText.text = "Its time to fight! Pick a Move";
    }

    public void OnAttackMove1Button()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttackMove(1));
    }

    public void OnAttackMove2Button()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttackMove(2));
    }

    public void OnAttackMove3Button()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttackMove(3));
    }

	IEnumerator PlayerAttackMove(int moveNumber)
	{
		bool isDead = false;
		switch (moveNumber)
		{
			case 1:
				isDead = playerChar.AttackMove1(enemyChar); // Player performs Attack Move 1 on enemy
				dialogueText.text = "Ouch hit him in the balls";
				break;
			case 2:
				isDead = playerChar.AttackMove2(enemyChar); // Player performs Attack Move 2 on enemy
				dialogueText.text = "That scared " + enemyChar.charName;
				break;
			case 3:
				isDead = playerChar.AttackMove3(enemyChar); // Player performs Attack Move 3 on enemy
				dialogueText.text = "Well there goes his eardrums";
				break;
		}

		enemyHUD.SetHP(enemyChar.curHp); // Update enemy HP HUD

		yield return new WaitForSeconds(2f);

		if (isDead)
		{
			state = BattleState.WON;
			EndBattle();
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

		public void OnHealButton()
		{
			if (state != BattleState.PLAYERTURN)
				return;

			StartCoroutine(PlayerHeal());
		}

		IEnumerator PlayerHeal()
		{
			playerChar.Heal(20);

			playerHUD.SetHP(playerChar.curHp);
			dialogueText.text = "Mmmmm nice healing!";

			yield return new WaitForSeconds(2f);

			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}

	IEnumerator EnemyTurn()
	{
		bool willHeal = false;
		// Example condition: Enemy decides to heal if its current health is below 50% of its max health
		if (enemyChar.curHp < enemyChar.maxHp * 0.5f)
		{
			// Let's say there's a 50% chance to heal if below 50% health, adjust as needed
			willHeal = Random.Range(0, 2) == 0;
		}

		if (willHeal)
		{
			// Perform healing; you can adjust the heal amount as needed
			enemyChar.Heal(20);
			enemyHUD.SetHP(enemyChar.curHp);
			dialogueText.text = $"{enemyChar.charName} eats a taco!";

			yield return new WaitForSeconds(2f);
		}
		else
		{
			// Perform attack
			dialogueText.text = $"{enemyChar.charName} punches!";

			yield return new WaitForSeconds(1f);

			bool isDead = playerChar.TakeDamage(enemyChar.damage);
			playerHUD.SetHP(playerChar.curHp);

			yield return new WaitForSeconds(1f);

			if (isDead)
			{
				state = BattleState.LOST;
				EndBattle();
				yield break; // Exit the coroutine if the game ends
			}
		}

		// Transition back to the player's turn regardless of the enemy's action
		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}


    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle! Lets go!";

			string sceneName = "WorldScene";
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;

			// Load the scene
			SceneManager.LoadScene(sceneName);
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
			button.gameObject.SetActive(true);
        }
    }
}
