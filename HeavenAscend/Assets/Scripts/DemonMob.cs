using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonMob : MonoBehaviour
{
    public int health = 3;
    public float movementInterval = 2.2f;
    private float timer = 0f;
    private bool isFacingLeft = true;
    [SerializeField] private AudioSource jump;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform trans;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingLeft && rb.velocity.x > 0 || !isFacingLeft && rb.velocity.x < 0)
        {
            Flip();
        }
        if (rb.velocity.y == 0 && rb.velocity.x == 0) { 
            float dist = GameObject.FindWithTag("Player").transform.position.x - trans.position.x;
            int sign = dist > 0f ? 1 : -1;
            float vertical = (dist <= .05f && dist >= -.05f) ? Random.Range(2f, 7f) : 0f;
            rb.velocity = new Vector2(sign * Random.Range(2f, 6f), vertical);
            if (vertical > 1f)
            {
                jump.Play();
            }
        }
        if (timer + Random.Range(0.0f, 0.5f) >= 2.2f)
        {
            timer = 0;
            Transform playerTranform = GameObject.FindWithTag("Player").transform;
            Vector3 pos = playerTranform.position;
            float xvel = pos.x - trans.position.x > 3.5f || trans.position.x - pos.x > 6 ? (pos.x - trans.position.x > 1 ? 3.5f : -3.5f) : pos.x - trans.position.x;
            float vertical = 5.2f * (pos.y - trans.position.y) + Random.Range(.1f, 1f);
            rb.velocity = new Vector2(2.3f * xvel, vertical) - new Vector2(Random.Range(-.02f, .02f), 0f);
            Debug.Log("movement");
            if (vertical >= 1f) {
                jump.Play();
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
        if (rb.position.x < -12.5)
        {
            rb.position = new Vector2(12.5f, rb.position.y);
        }
        if (rb.position.x > 12.5)
        {
            rb.position = new Vector2(-12.5f, rb.position.y);
        }
    }

    public void Damage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
