using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class playerManager : MonoBehaviour
{
    #region Health;
    [Header("Player Health")]
    [SerializeField]
    public float currentHealth;

    [SerializeField]
    private float playerHealth;
    #endregion


    #region Mana;
    [Header("Player Mana")]
    [SerializeField]
    public float currentMana;

    [SerializeField]
    private float playerMana;
    #endregion

    #region Bools;
    [Header("Other shit")]
    public bool iswarrior;
    public bool isMage;
    public bool cantTakeDamage;
    public bool isAttacking;
    private bool endGame;
    #endregion


    public bool didCrit;

    winner winnerScript;
    gameManager GM;
    winner WS;
    UIManager UIM;

    GameObject GMO;

    void Start()
    {
        UIM = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        WS = GameObject.FindGameObjectWithTag("winner").GetComponent<winner>();
        endGame = false;
        currentHealth = playerHealth;
        currentMana = playerMana;
        setUI();
        winnerScript = GameObject.FindGameObjectWithTag("winner").GetComponent<winner>();
        GM = GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>();
        GMO = GameObject.FindGameObjectWithTag("gameManager");

        didCrit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 2)
        {
           //Debug.Log("DROP HEALTH SHOULD BE SET TO TRUE!");
            GMO.GetComponent<powerUps>().dropHealth = true;
        }

        if (currentMana > playerMana)
        {
            currentMana = playerMana;
        }

        setUI();
        if (endGame)
        {
            die();
        }

        if (currentHealth > playerHealth)
        {
            currentHealth = playerHealth;
        }

        if (currentMana < playerMana)
        {
            manaRegen();
        }
        else if(currentMana == playerMana)
        {
            return;
        }

        
        
       
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("YOOOO " + gameObject.tag + " is taking " + damage);

        if(currentHealth <= 0)
        {
            if (this.gameObject.CompareTag("player1"))
            {
                WS.P2Winner = true;
            }
            else if(this.gameObject.CompareTag("player2"))
            {
                WS.P1Winner = true;
            }

            GM.gameOver = true;
            endGame = true;
            //setWinner();
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }

    void manaRegen()
    {
        if(currentMana == playerMana)
            return;

        currentMana += Time.deltaTime;

        if (currentMana == playerMana)
            return;

        
    }


    public void takeMana(float manaCost)
    {
        currentMana -= manaCost;
    }


    public void setUI()
    {
        if(this.gameObject.CompareTag("player1"))
        {
            UIM.p1Health = currentHealth;
            UIM.p1Mana = currentMana;
        }

        if(this.gameObject.CompareTag("player2"))
        {
            UIM.p2Health = currentHealth;
            UIM.p2Mana = currentMana;
        }
    }

    

    //public void setWinner()
    //{
    //    try
    //    {
    //        if (this.gameObject.CompareTag("player1"))
    //        {
    //            winnerScript.P1Winner = true;
    //            GM.gameOver = true;
    //        }

    //        if (this.gameObject.CompareTag("player2"))
    //        {
    //            winnerScript.P2Winner = true;
    //            GM.gameOver = true;
    //        }
    //    }
    //    catch
    //    {
    //        Debug.Log("WINNER IS NOT ASSIGNED");
    //    }
        
    //}
}
