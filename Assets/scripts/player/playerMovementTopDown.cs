using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementTopDown : MonoBehaviour
{

    #region Movement
    [Header("Movement")]
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float moveAcceleration;
    [SerializeField] private float dragForce;
    [SerializeField] private float startBoost;
    [SerializeField] public bool WASD;
    [SerializeField] public bool arrowKeys;
    string Direction;
    private float horizontalPos;
    //checks to see if we are turning or have turned 
    private bool changeDir => (rb.velocity.x > 0f && horizontalPos < 0f) || (rb.velocity.x < 0f && horizontalPos > 0f);
    Vector2 _move;

    #endregion

    #region Dodge
    [Header("Dodge")]
    public bool canDodge;
    [SerializeField]
    private float dodgeDelayTime;

    [SerializeField]
    private float dodgeSpeed;

    [SerializeField]
    private float dodgeDist;

    [SerializeField]
    private float dodgeCost;

    private float dodgeCounter = 1f;
    #endregion


    #region Player GFX
    [Header("player GFX")]
    [SerializeField] private SpriteRenderer sr;
    #endregion

   

    Rigidbody2D rb;

    float horizontal;
    float vertical;

    playerManager PM;

    
    // Start is called before the first frame update
    void Start()
    {
        PM = gameObject.GetComponent<playerManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        canDodge = true;
    }

    // Update is called once per frame
    void Update()
    {
        //checks what control scheme the player is using and then looks at the respective axis 
        if(WASD)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
        else if(arrowKeys)
        {
            horizontal = Input.GetAxisRaw("Horizontal2");
            vertical = Input.GetAxisRaw("Vertical2");
        }

        //playerDirection();
        flipPlayer();
        dodge();
        
    }

    private void FixedUpdate()
    {
        
        if(horizontal != 0 || vertical != 0)
        {
            movement();
        }

        horizontalDrag();

    }


    public void movement()
    {
        float xpos;
        xpos = Input.GetAxisRaw("Horizontal");


        _move = new Vector2(horizontal, vertical);
        _move *= (moveAcceleration);

        //to make beinging moevment snappy 
        if (rb.velocity.magnitude == 0)
        {
            rb.AddForce(_move * startBoost);
        }
        else
        {
            rb.AddForce(_move);
        }

        //checks to see what velcoity the character is at, if it is bigger or equal to max move speed it then clamps it to the max move speed 

        if (Mathf.Abs(rb.velocity.x) >= maxMoveSpeed)
        {
            Debug.Log("AT MAX MOVE SPEED");
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), rb.velocity.y);
            Debug.Log("VELOCITY IS " + rb.velocity.x);
        }

    }



    public void horizontalDrag()
    {
        if ((horizontalPos == 0) || (changeDir))   //change xpos to horizontalPos
        {
            rb.drag = dragForce;
        }
        else if ((horizontalPos != 0)) //change xpos to horizontalPos   || (Mathf.Abs(rb.velocity.y) > 5)
        {
            //so if it is jumping it sets the drag force to zero
            rb.drag = dragForce;

        }
    }


    private void flipPlayer()
    {

        if(WASD)
        {
            if(Input.GetKeyDown((KeyCode.D)))
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                SetDirection("RIGHT");
            }

            if (Input.GetKeyDown((KeyCode.A)))
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                SetDirection("LEFT");
            }

            if (Input.GetKeyDown((KeyCode.W)))
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                SetDirection("UP");
            }

            if (Input.GetKeyDown((KeyCode.S)))
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                SetDirection("DOWN");
            }

        }


        if(arrowKeys)
        {
            if (Input.GetKeyDown((KeyCode.RightArrow)))
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                SetDirection("RIGHT");
            }

            if (Input.GetKeyDown((KeyCode.LeftArrow)))
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                SetDirection("LEFT");
            }

            if (Input.GetKeyDown((KeyCode.UpArrow)))
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                SetDirection("UP");
            }

            if (Input.GetKeyDown((KeyCode.DownArrow)))
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                SetDirection("DOWN");
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the player collide with each other there will be no forced applied so they wont bounce off each other 
        if(collision.gameObject.CompareTag("player1"))
        {
            rb.velocity = Vector2.zero;
        }

        if (collision.gameObject.CompareTag("player2"))
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void playerDirection()
    {

        if(rb.velocity.x > 0f)
        {
            
            SetDirection("RIGHT");
            
        }
        
        if(rb.velocity.x < 0f)
        {
            
            SetDirection("LEFT");
           
        }

        if(rb.velocity.y > 0f)
        {
            
            SetDirection("UP");
            
        }
        
        if(rb.velocity.y < 0f)
        {
            
            SetDirection("DOWN");
            
        }
    }

    public void SetDirection(string _Direction)
    {
        Direction = _Direction;

    }


    public string ReturnDirection()
    {
        return Direction;
    }


    private void dodge()
    {
        if(gameObject.CompareTag("player1"))
        {
            if((Input.GetKeyDown(KeyCode.E)) && (canDodge) && (PM.currentMana >= 1))
            {
                dodgeCounter += Time.deltaTime * 5;
                
                StartCoroutine("dodgeDelay");
                PM.takeMana(dodgeCost);
                if(Direction == "RIGHT")
                {
                    
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position + new Vector3(dodgeDist, 0f,0f), dodgeCounter);
                }
                else if(Direction == "LEFT")
                {
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position + new Vector3(-dodgeDist, 0f, 0f), dodgeCounter);
                }
                else if(Direction == "UP")
                {
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position + new Vector3(0f, dodgeDist, 0f), dodgeCounter);
                }
                else if(Direction == "DOWN")
                {
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position + new Vector3(0f, -dodgeDist, 0f), dodgeCounter);
                }
                
            }
        }

        if(gameObject.CompareTag("player2"))
        {
            if((Input.GetKeyDown(KeyCode.Keypad3)) && (canDodge) && (PM.currentMana >= 1))
            {
                dodgeCounter += Time.deltaTime * 5;

                StartCoroutine("dodgeDelay");
                PM.takeMana(dodgeCost);
                if (Direction == "RIGHT")
                {

                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position + new Vector3(dodgeDist, 0f, 0f), dodgeCounter);
                }
                else if (Direction == "LEFT")
                {
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position + new Vector3(-dodgeDist, 0f, 0f), dodgeCounter);
                }
                else if (Direction == "UP")
                {
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position + new Vector3(0f, dodgeDist, 0f), dodgeCounter);
                }
                else if (Direction == "DOWN")
                {
                    gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position + new Vector3(0f, -dodgeDist, 0f), dodgeCounter);
                }

            }
        }
    }

    private IEnumerator dodgeDelay()
    {
        canDodge = false;
        yield return new WaitForSeconds(dodgeDelayTime);
        canDodge = true;
    }

}
