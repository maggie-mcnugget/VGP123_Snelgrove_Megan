using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField, Range(0.5f, 10f)]
    private float lifetime = 10f;

    private Rigidbody2D _rb;
    private Collider2D _collider;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rb.linearVelocity = velocity;
    }

    public void IgnoreOwnerTemporarily(Collider2D owner, float duration = 0.1f)
    {
        StartCoroutine(IgnoreOwnerCoroutine(owner, duration));
    }

    private IEnumerator IgnoreOwnerCoroutine(Collider2D owner, float duration)
    {
        Physics2D.IgnoreCollision(_collider, owner, true);
        yield return new WaitForSeconds(duration);
        Physics2D.IgnoreCollision(_collider, owner, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}