using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerControl : MonoBehaviour
{
    
    public float Speed = 2f, jumpSpeed = 5f, horizontal = 0f; // for player movement for player jump  for player movement
    private Rigidbody2D player; // for player movement  //for player jump
    public Transform groundCheck; // marker to indicate where the players feet are and are they touching the ground //for player jump
    public float groundCheckradius; //for player jump
    public LayerMask groundLayer; //for player jump
    private bool isTouchingground; //for player jump

    private Animator playerAnimation; // player animation
    private Vector3 respawnPoint;
    public GameObject fallDector;

    public Text scoreText;

        // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>(); //For player movement and jump
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;
        scoreText.text = "Score: " + Scoring.totalScore;
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingground = Physics2D.OverlapCircle(groundCheck.position, groundCheckradius, groundLayer);
        horizontal = Input.GetAxis("Horizontal");
        if(horizontal > 0f)
        {
            player.velocity = new Vector2(horizontal*Speed,player.velocity.y);
            transform.localScale = new Vector2(0.1024908f,0.1024908f);
        }
        else if(horizontal < 0f)
        {
            player.velocity = new Vector2(horizontal*Speed,player.velocity.y);
            transform.localScale = new Vector2(-0.1024908f,0.1024908f);
        }
        else
        {
            player.velocity = new Vector2(0,player.velocity.y);//for player not to move when no key are pressed
            
        }

        if(Input.GetButtonDown("Jump") && isTouchingground)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }
       
       playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x)); // Mathf.abs is used because as the player is moving along the x axis when the value change to a negative value the walking animation stops so for that to not happen we use the mathf.abs function.
       playerAnimation.SetBool("OnGround", isTouchingground);

       fallDector.transform.position = new Vector2(transform.position.x, fallDector.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FallDector")
        {
            transform.position = respawnPoint;
        }
        else if(collision.tag == "Obstacles")
        {
            transform.position = respawnPoint;
        }
        else if(collision.tag == "Next Level")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            respawnPoint = transform.position;
        } 
        else if(collision.tag == "Prev Level")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            respawnPoint = transform.position;
        } 
         else if(collision.tag == "Crystal")
        {
            Scoring.totalScore += 1;
            scoreText.text = "Score: " + Scoring.totalScore;
            collision.gameObject.SetActive(false);
        } 
        else if(collision.tag == "Crystal2")
        {
            Scoring.totalScore += 10;
            scoreText.text = "Score: " + Scoring.totalScore;
            collision.gameObject.SetActive(false);
        } 
    } 

    
}
