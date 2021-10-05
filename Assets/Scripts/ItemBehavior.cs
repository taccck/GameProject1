using UnityEngine;
using Random = UnityEngine.Random;

public class ItemBehavior : MonoBehaviour
{
    public Item item;

    [SerializeField] private LayerMask floorMask;
    [SerializeField] private float stopDistance = .75f;

    private bool falling = true;

    private void Update()
    {
        if (falling)
            falling = !FloorCheck();
    }

    private void FixedUpdate()
    {
        if (Time.time > 1)
            if (falling)
                transform.Translate(Vector2.up * Physics2D.gravity * Time.deltaTime * .5f);
            else
                Bobble();
    }

    private bool FloorCheck()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, stopDistance, floorMask);
    }

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = item.sprite;
        name = item.name;
    }

    float desync;
    float startY;

    private void Bobble()
    {
        if (desync == 0)
        {
            desync = Random.Range(0f, Mathf.PI * 2f);
            startY = transform.position.y - Mathf.Sin(Time.time * Mathf.PI + desync) / 5;
        }

        float newYPos = Mathf.Sin(Time.time * Mathf.PI + desync) / 5;
        transform.position = new Vector3(transform.position.x, startY + newYPos, transform.position.z);
    }
}