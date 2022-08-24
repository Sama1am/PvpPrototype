using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerSelection : MonoBehaviour
{

    [SerializeField]
    GameObject mage;

    [SerializeField]
    GameObject warrior;

    [SerializeField]
    GameObject p1StartPos;

    [SerializeField]
    GameObject p2StartPos;

    [SerializeField]
    GameObject playerSelectionPanel;

    [SerializeField]
    GameObject UIManager;

    [SerializeField]
    GameObject P1Stats;

    [SerializeField]
    GameObject P2Stats;

    [SerializeField]
    private Sprite P1Sprite;

    [SerializeField]
    private Sprite P2Sprite;

    private bool player1Selected = false;
    private bool player2Selected = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player1Selected && player2Selected)
        {
            //playerSelectionPanel.active = false;
            playerSelectionPanel.SetActive(false);
            //UIManager.SetActive(true);
            //P1Stats.SetActive(true);
            //P2Stats.SetActive(true);
        }
    }

    public void p1Mage()
    {
        GameObject player1 = Instantiate(mage, p1StartPos.transform.position, p1StartPos.transform.rotation);
        player1.GetComponentInChildren<SpriteRenderer>().sprite = P1Sprite;
        player1.GetComponent<playerMovementTopDown>().WASD = true;
        player1.GetComponent<playerManager>().isMage = true;
        player1.tag = "player1";
        player1Selected = true;

        
    }

    public void p1Warrior()
    {
        GameObject player1 = Instantiate(warrior, p1StartPos.transform.position, p1StartPos.transform.rotation);
        player1.GetComponentInChildren<SpriteRenderer>().sprite = P1Sprite;
        player1.GetComponent<playerMovementTopDown>().WASD = true;
        player1.GetComponent<playerManager>().iswarrior = true;
        player1.tag = "player1";
        player1Selected = true;

        
    }

    public void p2Mage()
    {
        GameObject player2 = Instantiate(mage, p2StartPos.transform.position, p2StartPos.transform.rotation);
        player2.GetComponentInChildren<SpriteRenderer>().sprite = P2Sprite;
        player2.GetComponent<playerMovementTopDown>().arrowKeys = true;
        player2.GetComponent<playerManager>().isMage = true;
        player2.tag = "player2";
        player2Selected = true;

        

    }

    public void p2Warrior()
    {
        GameObject player2 = Instantiate(warrior, p2StartPos.transform.position, p2StartPos.transform.rotation);
        player2.GetComponentInChildren<SpriteRenderer>().sprite = P2Sprite;
        player2.GetComponent<playerMovementTopDown>().arrowKeys = true;
        player2.GetComponent<playerManager>().iswarrior = true;
        player2.tag = "player2";
        player2Selected = true;

        
    }

}
