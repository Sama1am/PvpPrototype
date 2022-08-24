using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileLogic : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    private float force;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float lifeTime;


    UIManager UIM;
    
    // Start is called before the first frame update
    void Start()
    {
        UIM = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * force;
    }

    // Update is called once per frame
    void Update()
    {

        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void move()
    {
        rb.AddForce(transform.forward * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("obstacle"))
        {
            rb.velocity = Vector2.zero;
            Destroy(gameObject);
        }


        if(collision.gameObject.CompareTag("player2"))
        {
            if (collision.gameObject.GetComponent<playerManager>().cantTakeDamage == false)
            {
                float Crit = Random.Range(1, 101);
                if(Crit > 85)
                {
                    rb.velocity = Vector2.zero;
                    collision.gameObject.GetComponent<playerManager>().takeDamage(damage* 2);
                    Debug.Log("WARRIOR TOOK DAMAGE FROM MAGE, NOT BLOCKING!");
                    Destroy(gameObject);
                    
                    UIM.didCrit = true;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                    collision.gameObject.GetComponent<playerManager>().takeDamage(damage);
                    Debug.Log("WARRIOR TOOK DAMAGE FROM MAGE, NOT BLOCKING!");
                    Destroy(gameObject);
                }
                
            }

            if (collision.gameObject.GetComponent<playerManager>().cantTakeDamage == true)
            {
                rb.velocity = Vector2.zero;
                Destroy(gameObject);

            }
        }


        if(collision.gameObject.CompareTag("player1"))
        {
            if (collision.gameObject.GetComponent<playerManager>().cantTakeDamage == false)
            {
                float Crit = Random.Range(1, 100);
                if(Crit > 85)
                {
                    rb.velocity = Vector2.zero;
                    collision.gameObject.GetComponent<playerManager>().takeDamage(damage * 2);
                    Debug.Log("TOOK DAMAGE FROM PROJECTILE!");
                    Destroy(gameObject);

                    UIM.didCrit = true;

                }
                else
                {
                    rb.velocity = Vector2.zero;
                    collision.gameObject.GetComponent<playerManager>().takeDamage(damage);
                    Debug.Log("TOOK DAMAGE FROM PROJECTILE!");
                    Destroy(gameObject);
                }
               
            }

            if (collision.gameObject.GetComponent<playerManager>().cantTakeDamage == true)
            {
                rb.velocity = Vector2.zero;
                Destroy(gameObject);
            }
        }
        

    }


   
}
