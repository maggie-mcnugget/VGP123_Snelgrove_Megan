using UnityEngine;

[RequireComponent(typeof(Shoot))]
public class TurretEnemy : BaseEnemy
{
    Shoot shoot;

    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float distanceThreshold = 5f;

    private float timeSinceLastFire = 0f;
    private Transform player;

    public override void Start()
    {
        base.Start();

        shoot = GetComponent<Shoot>();

        // subscribe to player spawn event
        GameManager.Instance.OnPlayerSpawned += OnPlayerSpawned;

        // ALSO handle case where player already exists
        if (GameManager.Instance.PlayerInstance != null)
        {
            player = GameManager.Instance.PlayerInstance.transform;
        }
    }

    private void OnPlayerSpawned(PlayerController pc)
    {
        player = pc.transform;
    }
    void Update()
    {
        if (player == null) return;

    
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > distanceThreshold +0.5f)
            return;

    
        sr.flipX = player.position.x < transform.position.x;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Idle"))
        {
            if (Time.time >= timeSinceLastFire + fireRate)
            {
                anim.SetTrigger("Fire");
                timeSinceLastFire = Time.time;
            }
        }
    }
}
