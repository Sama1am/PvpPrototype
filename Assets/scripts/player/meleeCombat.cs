using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class meleeCombat : MonoBehaviour
{
    #region Sword

    [Header("Sword")]

    [SerializeField]
    private float attackDamage;

    [SerializeField]
    private GameObject swordAttackPoint;

    [SerializeField]
    private float swordAttackArea;

    [SerializeField]
    private GameObject sword;

    [SerializeField]
    private GameObject swordAttackPos;

    [SerializeField]
    private float attackTime;

    [SerializeField]
    private float attackCost;

    [SerializeField]
    private float attackDelay;

    public bool canAttack;
    public bool attacking;
    public float swordcounter = 0f;
    #endregion

    #region sheild

    [Header("sheild")]
    [SerializeField]
    private GameObject sheild;

    [SerializeField]
    private GameObject blockPos;


    [SerializeField]
    private float blockTime;

    [SerializeField]
    private float blockCost;

    [SerializeField]
    private float blockDelayTime;

    public bool blocking;
    private bool canBlock;
    #endregion


    #region otherShit
    public GameObject swordRestPos;
    public GameObject sheildRestPos;
    playerMovementTopDown PMS;
    playerManager PM;

    #endregion

    UIManager UIM;
   
    // Start is called before the first frame update
    void Start()
    {
        UIM = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        PM = gameObject.GetComponent<playerManager>();
        canAttack = true;
        canBlock = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(attacking)
        {
            PM.isAttacking = true;
        }
        else
        {
            PM.isAttacking = false;
        }

        meleeAttack();
        sheildBlock();
    }


    private void meleeAttack()
    {

        if (gameObject.CompareTag("player1"))
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && (gameObject.GetComponent<playerManager>().currentMana >= 0) && (canAttack))
            {

                canAttack = false;
                gameObject.GetComponent<playerManager>().takeMana(attackCost);
                Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(swordAttackPoint.transform.position, swordAttackArea);

                StartCoroutine("delayAttack");
                float Crit = Random.Range(1, 100);

                foreach (Collider2D enemy in hitEnemy)
                {

                    if (this.gameObject.tag != enemy.gameObject.tag)
                    {
                        try
                        {
                            if(Crit > 85)
                            {
                                enemy.gameObject.GetComponent<playerManager>().takeDamage(attackDamage * 2);
                                Debug.Log(enemy.name + " TOOK DAMAGE!");
                                UIM.didCrit = true;
                            }
                            else
                            {
                                enemy.gameObject.GetComponent<playerManager>().takeDamage(attackDamage);
                                Debug.Log(enemy.name + " TOOK DAMAGE!");
                            }
                            
                        }
                        catch
                        {
                            Debug.Log("oops something went wrong");
                        }
                    }

                }

                if (blocking == false && attacking == false)
                {
                    attacking = true;
                }


            }

            if (attacking == true)
            {
                if (swordcounter <= 1)
                {
                    swordcounter += Time.deltaTime * 5;
                    sword.transform.position = Vector3.Lerp(swordRestPos.transform.position, swordAttackPos.transform.position, swordcounter);
                }
                if (swordcounter >= 1)
                {
                    swordcounter = 0;
                    //sword.transform.position = Vector3.Lerp(sword.transform.position, swordAttackPos.transform.position, swordcounter);
                    attacking = false;
                }

            }
            else if (attacking == false)
            {
                sword.transform.position = swordRestPos.transform.position;
            }



        }

        if (gameObject.CompareTag("player2"))
        {
            if ((Input.GetKeyDown(KeyCode.Keypad1)) && (gameObject.GetComponent<playerManager>().currentMana >= 0) && (canAttack))
            {

                canAttack = false;
                gameObject.GetComponent<playerManager>().currentMana -= attackCost;
                Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(swordAttackPoint.transform.position, swordAttackArea);
                StartCoroutine("delayAttack");

                float Crit = Random.Range(1, 100);

                foreach (Collider2D enemy in hitEnemy)
                {

                    if (this.gameObject.tag != enemy.gameObject.tag)
                    {
                        try
                        {
                            if(Crit > 85)
                            {
                                enemy.gameObject.GetComponent<playerManager>().takeDamage(attackDamage * 2);
                                Debug.Log(enemy.name + " TOOK DAMAGE!");
                                UIM.didCrit = true;
                            }
                            else
                            {
                                enemy.gameObject.GetComponent<playerManager>().takeDamage(attackDamage);
                                Debug.Log(enemy.name + " TOOK DAMAGE!");
                            }
                            
                        }
                        catch
                        {
                            Debug.Log("oops something went wrong");
                        }
                    }

                }


                if (blocking == false && attacking == false)
                {
                    attacking = true;
                    
                }
            }

            if (attacking == true)
            {
                if (swordcounter <= 1)
                {
                    swordcounter += Time.deltaTime * 5;
                    sword.transform.position = Vector3.Lerp(swordRestPos.transform.position, swordAttackPos.transform.position, swordcounter);
                }
                if (swordcounter >= 1)
                {
                    Debug.Log("TITTIES");
                    swordcounter = 0;
                    //sword.transform.position = Vector3.Lerp(sword.transform.position, swordAttackPos.transform.position, swordcounter);
                    attacking = false;
                }

            }
            else if (attacking == false)
            {
                sword.transform.position = swordRestPos.transform.position;
            }

        }

    }


    void sheildBlock()
    {
       
        if (gameObject.CompareTag("player1"))
        {

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if ((gameObject.GetComponent<playerManager>().currentMana >= 1) && (canBlock))
                {
                    canBlock = false;
                    Debug.Log("SHOULD BE BLOCKING!!");
                    StartCoroutine("blockDelay");
                    gameObject.GetComponent<playerManager>().takeMana(blockCost);
                    sheild.transform.position = blockPos.transform.position;
                    sheild.transform.rotation = blockPos.transform.rotation;
                    blocking = true;
                   
                }
                   
            }
           
        }

        if (gameObject.CompareTag("player2"))
        {

            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                if ((gameObject.GetComponent<playerManager>().currentMana >= 1) && (canBlock))
                {
                    canBlock = false;
                    Debug.Log("SHOULD BE BLOCKING!!");
                    StartCoroutine("blockDelay");
                    gameObject.GetComponent<playerManager>().takeMana(blockCost);
                    sheild.transform.position = blockPos.transform.position;
                    sheild.transform.rotation = blockPos.transform.rotation;
                    blocking = true;
                    
                }

            }
        }
    }


    private IEnumerator blockDelay()
    {
        PM.cantTakeDamage = true;
        canBlock = false;
        blocking = true;
        yield return new WaitForSeconds(blockDelayTime);
        sheild.transform.position = sheildRestPos.transform.position;
        sheild.transform.rotation = sheildRestPos.transform.rotation;
        blocking = false;
        canBlock = true;
        PM.cantTakeDamage = false;
    }

    private IEnumerator delayAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(swordAttackPoint.transform.position, swordAttackArea);
        
    }
}
