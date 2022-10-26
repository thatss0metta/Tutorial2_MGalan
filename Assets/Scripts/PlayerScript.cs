using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI lives;
    public GameObject WinTextObject;
    public GameObject LoseTextObject;

    public float speed;

    public TextMeshProUGUI score;

    private int scoreValue = 0;

    private int livesValue = 3;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        WinTextObject.SetActive(false);
        LoseTextObject.SetActive(false);
        lives.text = livesValue.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
         if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (scoreValue >= 8)
        {
            WinTextObject.SetActive(true);
            LoseTextObject.SetActive(false);

        }
        if (livesValue == 0)
        {
            WinTextObject.SetActive(false);
            LoseTextObject.SetActive(true);

        }
        
        if (scoreValue == 4) 
        {
            livesValue = 3;
            transform.position = new Vector2(26f, 1f);
        }

        if (livesValue == 0)
        {
            LoseTextObject.SetActive(true);
            Destroy(gameObject);
        }

        if (scoreValue >= 8)
        {
            WinTextObject.SetActive(true);
            Destroy(gameObject);
            SoundScript.PlaySound("WinMusic");
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
}