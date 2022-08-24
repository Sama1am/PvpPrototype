using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageCombat : MonoBehaviour
{
    #region Blast;
    [Header("BLast")]
    [SerializeField]
    private GameObject blastPrefab;

    [SerializeField]
    private GameObject spawnPos;

    [SerializeField]
    private float force;

    [SerializeField]
    private float manaCost;

    [SerializeField]
    private float shootDelay;
    #endregion


    #region AOE;
    [Header("AOE")]
    [SerializeField]
    private float AOEManaCost;

    [SerializeField]
    private GameObject AOEPoint;

    [SerializeField]
    private float AOEArea;

    [SerializeField]
    private float AOEDamage;

    [SerializeField]
    private float AOEBlastDelay;

    [SerializeField]
    private GameObject AOEEfffect;
    #endregion


    #region otherShit;
    private bool canShoot;
    private bool canAOE;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        canAOE = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        blast();
        waveBlast();
    }

    void blast()
    {


        if (gameObject.CompareTag("player1"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if ((gameObject.GetComponent<playerManager>().currentMana >= 1) && (canShoot))
                {
                    GameObject bullet = Instantiate(blastPrefab, spawnPos.transform.position, spawnPos.transform.rotation);
                    bullet.transform.SetParent(this.gameObject.transform);
                    StartCoroutine("delayAttack");
                    //bullet.GetComponent<Rigidbody2D>().AddForce(transform.forward * force, ForceMode2D.Impulse);
                    Debug.Log("SHOULD SHOOT1");

                    
                    gameObject.GetComponent<playerManager>().takeMana(manaCost);
                    if (gameObject.CompareTag("player1"))
                        bullet.tag = ("player1");

                    if (gameObject.CompareTag("player2"))
                        bullet.tag = ("player2");

                    
                }

            }
        }

        if (gameObject.CompareTag("player2"))
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                if ((gameObject.GetComponent<playerManager>().currentMana >= 1) && (canShoot))
                {
                    GameObject bullet = Instantiate(blastPrefab, spawnPos.transform.position, spawnPos.transform.rotation);
                    bullet.transform.SetParent(this.gameObject.transform);
                    StartCoroutine("delayAttack");
                    //bullet.GetComponent<Rigidbody2D>().AddForce(transform.forward * force, ForceMode2D.Impulse);
                    Debug.Log("SHOULD SHOOT1");

                    
                    gameObject.GetComponent<playerManager>().takeMana(manaCost);
                    if (gameObject.CompareTag("player1"))
                        bullet.tag = ("player1");

                    if (gameObject.CompareTag("player2"))
                        bullet.tag = ("player2");

                    
                }

            }


        }

    }

    void waveBlast()
    {

        if (gameObject.CompareTag("player1"))
        {

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if ((gameObject.GetComponent<playerManager>().currentMana >= 2) && (canAOE))
                {
                    gameObject.GetComponent<playerManager>().takeMana(AOEManaCost);
                    StartCoroutine("AOEEffectTime");
                    Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AOEPoint.transform.position, AOEArea);
                    StartCoroutine("AOEDelay");
                    

                    foreach (Collider2D enemy in hitEnemy)
                    {

                        if (this.gameObject.tag != enemy.gameObject.tag)
                        {
                            try
                            {
                                enemy.gameObject.GetComponent<playerManager>().takeDamage(AOEDamage);
                            }
                            catch
                            {
                                Debug.Log("oops something went wrong");
                            }
                        }

                    }
                }

            }
        }



        if (gameObject.CompareTag("player2"))
        {
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                if ((gameObject.GetComponent<playerManager>().currentMana >= 2) && (canAOE))
                {
                    gameObject.GetComponent<playerManager>().takeMana(AOEManaCost);
                    StartCoroutine("AOEEffectTime");
                    Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AOEPoint.transform.position, AOEArea);
                    StartCoroutine("AOEDelay");
                    

                    foreach (Collider2D enemy in hitEnemy)
                    {

                        if (this.gameObject.tag != enemy.gameObject.tag)
                        {
                            try
                            {
                                enemy.gameObject.GetComponent<playerManager>().takeDamage(AOEDamage);
                            }
                            catch
                            {
                                Debug.Log("oops something went wrong");
                            }
                        }

                    }
                }

            }
        }




    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AOEPoint.transform.position, AOEArea);
    }


    private IEnumerator delayAttack()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }

    private IEnumerator AOEDelay()
    {
        canAOE = false;
        yield return new WaitForSeconds(AOEBlastDelay);
        canAOE = true;
    }
    private IEnumerator AOEEffectTime()
    {
        AOEEfffect.SetActive(true);
        //AOEEfffect.GetComponent<SpriteRenderer>().color = new Color(16f, 144f, 11f, 59f);
        yield return new WaitForSeconds(1);
        //AOEEfffect.GetComponent<SpriteRenderer>().color = new Color(16f, 144f, 11f, 0f);
        AOEEfffect.SetActive(false);
    }
}
