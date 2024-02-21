using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public EnemyAttributes enemyAttributes;
    private bool canShoot = true;
    private Projectile enemyProjectile;
    private Rigidbody2D rigidBody2D;
    private PlayerController player;
    private Vector2 playerPosition;
    private Vector2 directionToPlayer;
    private float angleToPlayer;

    private NavMeshAgent agent;

    void Start()
    {
        InitEnemy();
    }

    private void InitEnemy()
    {
        enemyProjectile = GetComponent<Projectile>();
        player = FindObjectOfType<PlayerController>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        enemyProjectile.SetDamage(enemyAttributes.attackPower);

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = enemyAttributes.moveSpeed;
        
    }

    void Update()
    {
        playerPosition = player.GetPlayerPosition();
        EnemyRotationFollowPlayerPostion();
        Movement();
        EnemyAttackPlayer();      
    }

    public virtual void EnemyAttackPlayer()
    {
        if (canShoot)
        {
            StartCoroutine(ShootCooldown());
            ShootProjectile();
        }
    }

    private void Movement()
    {
        agent.SetDestination(playerPosition);
        //rigidBody2D.MovePosition(rigidBody2D.position+directionToPlayer*enemyAttributes.moveSpeed*Time.deltaTime);
    }

    private void EnemyRotationFollowPlayerPostion()
    {
        // Calculate the direction from the player to the mouse
        directionToPlayer = playerPosition - rigidBody2D.position;

        angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        // Rotate the player to face the mouse cursor
        rigidBody2D.rotation = angleToPlayer - 90f;
    }
    IEnumerator ShootCooldown()
    {
        canShoot = false; // Prevent further shooting
        yield return new WaitForSeconds(enemyAttributes.attackRate);
        canShoot = true; // Allow shooting again after the delay
    }

    private void ShootProjectile()
    {
        float spawnDistance = 0.15f; // from the Enemy
        // Calculate the spawn position with the offset
        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y) + directionToPlayer * spawnDistance;
        
        //Angle for rotation for the projectile
        Quaternion projectileRotation = Quaternion.Euler(new Vector3(0, 0, angleToPlayer - 90));
        
        // Instantiate the projectile at the calculated spawn position
        GameObject projectile = Instantiate(enemyProjectile.projectile, spawnPosition, projectileRotation);

        // giving to the hitcontrol script the shooter game object
        ProjectileHitControl projectileScript = projectile.GetComponent<ProjectileHitControl>();
        if (projectileScript != null)
        {
            projectileScript.projectileDamage = enemyAttributes.attackPower;
        }
        // Add a Rigidbody2D to the circle
        Rigidbody2D rb = projectile.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        // Set the velocity of the circle towards the mouse direction
        rb.velocity = directionToPlayer * enemyProjectile.projectileSpeed;
    }

    public void OnDeath()
    {       
        Destroy(gameObject);
        GameObject expObj = Instantiate(enemyAttributes.expObject,transform.localPosition,Quaternion.identity);
        expObj.GetComponent<ExperienceObject>().experienceValue = enemyAttributes.expValue;
    }
}
