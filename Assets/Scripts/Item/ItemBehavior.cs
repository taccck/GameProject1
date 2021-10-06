using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemBehavior : MonoBehaviour
{
    public Item item;
    public bool waitOnstart = true;

    [SerializeField] private LayerMask floorMask;
    [SerializeField] private float stopDistance = .75f;
    [SerializeField] private ParticleSystem particleSystem;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    private void Update()
    {
        //check when to stop falling
        if (Mathf.Approximately(body.gravityScale, 1f))
            body.gravityScale = FloorCheck() ? 0f : 1f;
    }

    private void FixedUpdate()
    {
        //bobble when on ground
        if (Mathf.Approximately(body.gravityScale, 0f) && !waitOnstart)
            Bobble();
    }

    private bool FloorCheck() //todo can fall through the floor if it's moving faster than stopDistance
    {
        return Physics2D.Raycast(transform.position, Vector2.down, stopDistance, floorMask);
    }

    float desync;
    float startY;

    private void Bobble()
    {
        if (desync == 0)
        {
            body.velocity = Vector2.zero;
            desync = Random.Range(0f, Mathf.PI * 2f);
            startY = transform.position.y - Mathf.Sin(Time.time * Mathf.PI + desync) / 5;
        }

        float newYPos = Mathf.Sin(Time.time * Mathf.PI + desync) / 5;
        transform.position = new Vector3(transform.position.x, startY + newYPos, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        body.velocity = Vector2.zero;
        //player pickup
        if (other.transform.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerInventory>().Add(item))
                StartCoroutine(Pickup());
        }
    }

    IEnumerator Pickup()
    {
        particleSystem.Play();
        spriteRenderer.color = new Color(0f, 0f, 0f, 0f);
        Destroy(GetComponent<BoxCollider2D>());
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void Start()
    {
        if (waitOnstart)
            StartCoroutine(WaitOnStart());

        SetItemValues();
        particleSystem.GetComponent<Renderer>().sortingLayerName = "Foreground";
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItemValues()
    {
        if (item != null)
        {
            spriteRenderer.sprite = item.sprite;
            name = item.name;
        }
    }

    private IEnumerator WaitOnStart()
    {
        //items fall through floor on start
        body.gravityScale = 0f;
        yield return new WaitForSeconds(1f);
        body.gravityScale = 1f;
        waitOnstart = false;
    }
}