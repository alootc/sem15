using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 dir;
    public float directionX;

    private AudioSource audioSource;

    public float speed;

    public GameObject prefabBullet;
    public AudioClip clip;

    public int score = 0;
    public Text scoreUI;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
         float x = Input.GetAxis("Horizontal");
         float y = Input.GetAxis("Vertical");

        dir = new Vector2(x, y);

        if (x < 0)
        {
            anim.SetBool("Izquierda", true);
            anim.SetBool("Derecha", false);
        }   
        else if (x > 0)
        {
            anim.SetBool("Derecha", true);
            anim.SetBool("Izquierda", false);
        }
        else
        {
            anim.SetBool("Derecha", false);
            anim.SetBool("Izquierda", false);
        }
           
        if (Input.GetKeyDown(KeyCode.Space))
        {
           audioSource.PlayOneShot(clip);
           GameObject go =  Instantiate(prefabBullet, transform.position, Quaternion.identity);
           go.GetComponent<Bullet>().ship = this;
        }  
    }

    public void UpdateScoreText()
    {
        score += 10;
        scoreUI.text = $"Puntaje:{score}";
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Pared")
        {
            directionX = directionX * -1;
        }
    }
}
