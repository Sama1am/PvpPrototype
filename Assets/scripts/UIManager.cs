using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("UI")]

    [SerializeField]
    private Slider Player1Health;

    [SerializeField]
    private Slider player1Mana;

    [SerializeField]
    private Slider Player2Health;

    [SerializeField]
    private Slider player2Mana;

    [SerializeField]
    private GameObject critTxt;

    public float p1Health;
    public float p2Health;
    public float p1Mana;
    public float p2Mana;
    public bool didCrit;

    GameObject PM1;
    GameObject PM2;
    // Start is called before the first frame update
    void Start()
    {
       

       
    }

    // Update is called once per frame
    void Update()
    {
       
        setUI();

        if(didCrit)
        {
            StartCoroutine("critPopUp");
        }

        
    }

    public void setUI()
    {
        try
        {
            Player1Health.value = p1Health;
            player1Mana.value = p1Mana;

            Player2Health.value = p2Health;
            player2Mana.value = p2Mana;
        }
        catch
        {
            Debug.Log("COULDNT SET UI");
        }
        
        
        
    }


    void findPlayers()
    {
        PM1 = GameObject.FindGameObjectWithTag("player1");
        PM2 = GameObject.FindGameObjectWithTag("player2");
    }

    IEnumerator critPopUp()
    {
        critTxt.SetActive(true);
        yield return new WaitForSeconds(3);
        didCrit = false;
        critTxt.SetActive(false);
    }

    
}
