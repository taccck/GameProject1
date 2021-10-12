using System.Collections;
using FG;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public class ItemBehavior : MonoBehaviour
{
    public Item item;
    public bool waitOnstart = true;

    [SerializeField] private float stopDistance = .75f;

    private LayerMask floorMask;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem pickupParticles;
    private ParticleSystem burnParticles;
    private BoxCollider2D boxCollider;
    private Vector2 colliderSize;
    private Coroutine destroying;

    private void Update()
    {
        //check when to stop falling
        if (destroying == null)
            if (Mathf.Approximately(body.gravityScale, 1f))
                body.gravityScale = FloorCheck() ? 0f : 1f;
    }

    private void FixedUpdate()
    {
        //bobble when on ground
        if (destroying == null)
            if (Mathf.Approximately(body.gravityScale, 0f) && !waitOnstart)
                Bobble();
    }

    private bool FloorCheck() //todo can fall through the floor if it's moving faster than stopDistance
    {
        bool onFloor = Physics2D.Raycast((Vector2) transform.position - new Vector2(0f, colliderSize.y / 2f),
            Vector2.down, stopDistance, floorMask);
        if (onFloor)
        {
            body.velocity = Vector2.zero;
            if (boxCollider != null)
                boxCollider.isTrigger = true;
        }

        return onFloor;
    }

    private float desync;
    private float startY;

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
        //player pickup
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerInventory>().Add(item))
                destroying = StartCoroutine(DestroyMe(pickupParticles));
        }
        else if (other.transform.CompareTag("Danger"))
        {
            Lavaraiser doIExist = other.GetComponent<Lavaraiser>();
            if (doIExist != null)
            {
                destroying = StartCoroutine(DestroyMe(burnParticles));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //player pickup
        if (other.transform.CompareTag("Player"))
        {
            if (other.transform.GetComponent<PlayerInventory>().Add(item))
                destroying = StartCoroutine(DestroyMe(pickupParticles));
        }
        else if (other.transform.CompareTag("Danger"))
        {
            Lavaraiser doIExist = other.transform.GetComponent<Lavaraiser>();
            if (doIExist != null)
            {
               destroying = StartCoroutine(DestroyMe(burnParticles));
            }
        }
    }

    private IEnumerator DestroyMe(ParticleSystem particles)
    {
        particles.Play();
        Destroy(spriteRenderer);
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<ShadowCaster2D>());
        Destroy(body);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void Start()
    {
        if (waitOnstart)
            StartCoroutine(WaitOnStart());

        SetItemValues();
        floorMask = LayerMask.GetMask("Floor");
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

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pickupParticles = transform.GetChild(0).GetComponent<ParticleSystem>();
        burnParticles = transform.GetChild(1).GetComponent<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider2D>();
        colliderSize = boxCollider.size;

        pickupParticles.GetComponent<Renderer>().sortingLayerName = "Particles";
        burnParticles.GetComponent<Renderer>().sortingLayerName = "Particles";
    }
}