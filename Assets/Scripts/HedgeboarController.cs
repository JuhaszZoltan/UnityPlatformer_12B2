using UnityEditor.Build;
using UnityEngine;

public class HedgeboarController : MonoBehaviour
{
    //[SerializeField] private GameObject hedgeboarGraphics;
    [SerializeField] private float speed = 7f;

    private Animator animator;
    private Rigidbody2D rigidbody2d;

    private bool facingRight = false;
    private float nextFlipChance = 0f;
    private float flipTime = 3f;
    private bool canFlip = true;
    private bool isCharging = false;
    private float startChargeTime = 0f;
    private float chargeDelay = .5f;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (canFlip && Time.time > nextFlipChance)
        {
            if (Random.Range(0, 10) >= 3) FlipBoar();
            nextFlipChance = Time.time + flipTime;
        }

        animator.SetBool("isCharging", isCharging);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if ((facingRight && other.transform.position.x < transform.position.x) 
                || !facingRight && other.transform.position.x > transform.position.x)
            {
                FlipBoar();
            }
            canFlip = false;
            isCharging = true;
            startChargeTime = Time.time + chargeDelay;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (startChargeTime < Time.time)
            {
                if (!facingRight) rigidbody2d.AddForce(new Vector2(-1, 0) * speed);
                else rigidbody2d.AddForce(new Vector2(1, 0) * speed);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canFlip = true;
            isCharging = false;
            rigidbody2d.velocity = Vector2.zero;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {

    }

    private void FlipBoar()
    {
        facingRight = !facingRight;
        transform.localScale = new(
            x: transform.localScale.x * -1,
            y: transform.localScale.y,
            z: transform.localScale.z);
    }
}
