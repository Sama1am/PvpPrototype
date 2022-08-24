using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUps : MonoBehaviour
{
    [SerializeField]
    private float powerUpTimeDelay;

    [SerializeField]
    private GameObject healthPowerUp;

    [SerializeField]
    private GameObject spawnPoint;

    public bool spawnedPowerUp;

    public bool dropHealth;

    public bool tookPotion;

    void Start()
    {
        spawnedPowerUp = false;
        dropHealth = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(tookPotion)
        {

            StartCoroutine("powerUpDelay");
            tookPotion = false;

        }

        try
        {
            if((dropHealth) && (spawnedPowerUp == false))
            {
                Debug.Log("Should SPAWN HEALTH POTION!");
                spawnHealth();
            }
        }
        catch
        {
            
        }
       
    }


    private void spawnHealth()
    {
        Instantiate(healthPowerUp, spawnPoint.transform.position, spawnPoint.transform.rotation);
        spawnedPowerUp = true;
        dropHealth = false;
        
    }

   IEnumerator powerUpDelay()
    {

        yield return new WaitForSeconds(5);
        tookPotion = false;
        spawnedPowerUp = false;
        dropHealth = false;
        

    }

}
