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

        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
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
