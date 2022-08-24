using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordLogic : MonoBehaviour
{

    [SerializeField]
    private float damage;

    playerManager PM;

    // Start is called before the first frame update
    void Start()
    {
        PM = gameObject.GetComponentInParent<playerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLLIDED WITH " + collision.gameObject.name);

        if(PM.isAttacking)
        {
            if ((collision.gameObject.CompareTag("player1")) || (collision.gameObject.CompareTag("player2")))
            {
                Debug.Log("COLLIDED WITH A PLAYER!");

                if (collision.gameObject.GetComponent<playerManager>().cantTakeDamage == false)
                {
                    float Crit = Random.Range(1, 100);
                    if(Crit > 85)
                    {
                        collision.gameObject.GetComponent<playerManager>().takeDamage(damage * 2);
                    }
                    else
                    {
                        collision.gameObject.GetComponent<playerManager>().takeDamage(damage);
                    }
                    
                    Debug.Log("TOOK DAMAGE FROM SWORD!");
                }

            }
        }

    }

   
}
