using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class winner : MonoBehaviour
{


    public bool P1Winner;
    public bool P2Winner;

    public GameObject winnerUi;

    public bool gameIsOver;

    [SerializeField]
    private TextMeshProUGUI winnerTxt;

    //public GameObject[] players = new GameObject[2];



    gameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        P1Winner = false;
        P2Winner = false;
        gameIsOver = false;
        GM = GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if(GM.gameOver)
        {
            gameIsOver = true;
        }

        if(gameIsOver)
        {
            setWinner();
            winnerUI();
        }

       
    }

    void setWinner()
    {
        if(P1Winner)
        {
            winnerTxt.text = "Player 1 is the winner!";
        }

        if(P2Winner)
        {
            winnerTxt.text = "Player 2 is the winner!";
        }
    }


    void winnerUI()
    {
        winnerUi.SetActive(true);
    }


   

}
