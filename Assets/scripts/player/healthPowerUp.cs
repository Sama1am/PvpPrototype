using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPowerUp : MonoBehaviour
{

    powerUps PU;

    [SerializeField]
    private float increashealth;
    void Start()
    {
        PU = GameObject.FindGameObjectWithTag("gameManager").GetComponent<powerUps>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("player1"))
        {
            PU.tookPotion = true;
            collision.gameObject.GetComponent<playerManager>().currentHealth += 1;
            Debug.Log("should of added " + 1 + " to health!");
            Destroy(gameObject);
        }
        
        
        if(collision.gameObject.CompareTag("player2"))
        {
            PU.tookPotion = true;
            collision.gameObject.GetComponent<playerManager>().currentHealth += 1;
            Debug.Log("should of added " + 1 + " to health!");
            Destroy(gameObject);
        }
    }


    
}
