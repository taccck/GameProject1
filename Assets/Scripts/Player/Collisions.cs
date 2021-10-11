using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FG
{
    public class Collisions : MonoBehaviour
    {
        [SerializeField] private PlayerInventory inventory;
        [SerializeField] private float invultime = 1f;

        private SpriteRenderer spriteRenderer;
        private PlayerMovmentController movementController;
        [HideInInspector] private bool invul = false;
        [HideInInspector] private Coroutine invulroutine;
        private Coroutine noPickupRoutine;

        private IEnumerator Invulperiod()
        {
            yield return new WaitForSeconds(invultime);
            invul = false;
        }

        private IEnumerator InvulAnim()
        {
            for (float currTime = 0; currTime < invultime; currTime += Time.fixedDeltaTime)
            {
                Color newColor = spriteRenderer.color;
                newColor.a = Mathf.Sin((currTime + .835f) * 18.8f) * .5f + .5f;
                spriteRenderer.color = newColor;
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            invul = false;
            inventory.CanPickup = true;
            spriteRenderer.color = Color.white;
        }
        
        private IEnumerator NoPickup()
        {
            inventory.CanPickup = false;
            yield return new WaitForSeconds(invultime);
            inventory.CanPickup = true;
        }

        private void DangerCollision(Vector2 attackersPos)
        {
            if (!invul)
            {
                if (!inventory.Drop()) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                inventory.CanPickup = false;
                invul = true;
                invulroutine = StartCoroutine(InvulAnim());
                movementController.Knockback(transform.position.x > attackersPos.x,
                    transform.position.y > attackersPos.y);
            }
        }

        private void LavaCollision()
        {
            if (!inventory.Drop()) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            movementController.LavaKnockback();
            movementController.bonking = false;
            
            if (noPickupRoutine != null) StopCoroutine(noPickupRoutine);
            noPickupRoutine = StartCoroutine(NoPickup());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name == "Lava")
                LavaCollision();
            else if (collision.CompareTag("Danger"))
            {
                DangerCollision(collision.transform.position);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Danger"))
            {
                DangerCollision(collision.transform.position);
            }
        }

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            movementController = GetComponent<PlayerMovmentController>();
        }
    }
}