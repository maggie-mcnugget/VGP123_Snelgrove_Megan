using UnityEngine;

public class Shoot : MonoBehaviour
{
    private SpriteRenderer _sr;

    [SerializeField] private Vector2 initalShotVelocity = new Vector2(3, 3);
    [SerializeField] private Transform spawnPointLeft;
    [SerializeField] private Transform spawnPointRight;
    [SerializeField] private Projectile projectilePrefab;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();

        if (spawnPointLeft == null || spawnPointRight == null || projectilePrefab == null)
        {
            Debug.LogError("Shoot script missing references on " + gameObject.name);
        }
    }

    public void Fire()
    {

        Projectile currentProjectile;

        if (!_sr.flipX)
        {
            currentProjectile = Instantiate(
                projectilePrefab,
                spawnPointRight.position,
                Quaternion.identity
            );
        }
        else
        {
            currentProjectile = Instantiate(
                projectilePrefab,
                spawnPointLeft.position,
                Quaternion.identity
            );
        }

    
        Collider2D playerCollider = GetComponent<Collider2D>();
        currentProjectile.IgnoreOwnerTemporarily(playerCollider);

    
        Vector2 velocity = initalShotVelocity;
        velocity.x *= _sr.flipX ? -1 : 1;
        currentProjectile.SetVelocity(velocity);
    }
}