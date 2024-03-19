using UnityEngine;

public class HedgeboarController : MonoBehaviour
{
    [SerializeField] private GameObject hedgeboarGraphics;

    private Animator animator;
    private Rigidbody2D rigidbody2d;

    private bool facingRight = false;
    private float nextFlipChance = 0f;
    private float flipTime = 3f;
    private bool canFlip = true;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (canFlip && Time.time > nextFlipChance)
        {
            if (Random.Range(0, 10) >= 5) FlipBoar();
            nextFlipChance = Time.time + flipTime;
        }
    }

    private void FlipBoar()
    {
        hedgeboarGraphics.transform.localScale = new(
            x: hedgeboarGraphics.transform.localScale.x * -1,
            y: hedgeboarGraphics.transform.localScale.y,
            z: hedgeboarGraphics.transform.localScale.z);
    }
}
