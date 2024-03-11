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

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action: \n1. Attack Move 1\n2. Attack Move 2\n3. Attack Move 3\n4. Heal";
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
            dialogueText.text = "Attack Move 1 is successful!";
            break;
        case 2:
            isDead = playerChar.AttackMove2(enemyChar); // Player performs Attack Move 2 on enemy
            dialogueText.text = "Attack Move 2 hits hard!";
            break;
        case 3:
            isDead = playerChar.AttackMove3(enemyChar); // Player performs Attack Move 3 on enemy
            dialogueText.text = "Attack Move 3 strikes!";
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
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyChar.charName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerChar.TakeDamage(enemyChar.damage);

        playerHUD.SetHP(playerChar.curHp);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }
}
