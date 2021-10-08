using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FG
{
    public class Collisions : MonoBehaviour
    {
        [SerializeField] private Collider2D collider;
        [SerializeField] private PlayerInventory inventory;
        [SerializeField] private float invultime = 1f;

        private SpriteRenderer spriteRenderer;
        private PlayerMovmentController movementController;
        [HideInInspector] private bool invul = false;
        [HideInInspector] private Coroutine invulroutine;

        private IEnumerator Invulperiod()
        {
            yield return new WaitForSeconds(invultime);
            invul = false;
        }
        
        private IEnumerator InvulAnim()
        {
            for(float currTime = 0; currTime < invultime; currTime += Time.fixedDeltaTime)
            {
                Color newColor = spriteRenderer.color;
                newColor.a = Mathf.Sin((currTime + .835f) * 18.8f) * .5f + .5f;
                spriteRenderer.color = newColor;
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
            invul = false;
            spriteRenderer.color = Color.white;
        }

        private void Collision(Vector2 attackersPos)
        {
            if (!invul)
            {
                invul = true;
                invulroutine = StartCoroutine(InvulAnim());
                inventory.Drop();
                movementController.Knockback(transform.position.x > attackersPos.x, transform.position.y > attackersPos.y);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name == "Lava")
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            else if (collision.CompareTag("Danger"))
            {
                Collision(collision.transform.position);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Danger"))
            {
                Collision(collision.transform.position);
            }
        }

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            movementController = GetComponent<PlayerMovmentController>();
        }
    }
}