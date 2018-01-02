using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Script: MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public int jumpNo = 0;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Slider slider;
    public GameObject controller;
    public int potionNum;
    GameControlScript control;
    public Sprite[] sprites;

    public bool grounded = false;
    private Rigidbody2D rb2d;


    // Use this for initialization
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        control = controller.GetComponent<GameControlScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump2"))
        {
            Jump();
        }

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal2");

        if (Input.GetButtonUp("Horizontal2"))
            rb2d.velocity.Set(0, rb2d.velocity.y);

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        if (grounded)
            rb2d.transform.position += new Vector3(-0.05f, 0, 0);

        if (h == 0.0f || grounded)
        {
            rb2d.velocity.Set(0, rb2d.velocity.y);
        }

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Platform" || coll.gameObject.tag == "Player")
        {
            grounded = true;
            jumpNo = 0;
        }
        else if (coll.gameObject.tag == "Potion")
        {
            control.potionsInScene.Remove(coll.gameObject);
            Destroy(coll.gameObject);
            this.GotPotion();
        }

    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Platform" || coll.gameObject.tag == "Player")
            grounded = false;

    }

    void Jump()
    {
        if (jumpNo < 2)
        {
            jumpNo++;
            rb2d.AddForce(new Vector2(0f, jumpForce));
        }
    }

    public void TakeDamage(float f)
    {
        if (slider.value - f < 0)
        {
            this.Die();
        }
        else
            slider.value -= f;
    }

    public void Die()
    {
        Destroy(gameObject);
        control.Player2Wins();
    }

    public void GotPotion()
    {
        potionNum = Random.Range(0, 3);
        SpriteRenderer s = gameObject.GetComponent<SpriteRenderer>();
        if (potionNum == 2)
        {
            jumpForce *= 3 / 2;
        }
        else if (potionNum == 1)
        {
            moveForce *= 3 / 2;
        }
        s.sprite = sprites[potionNum];
    }
}
