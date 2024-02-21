using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerAttributes playerAttributes;
    private float healthRegenTimer = 1f;
    private float regenHealthCooldown;
    private float dashTime; // Duration of the dash in seconds
    private float cooldownTimeRemaining =0f;
    private bool isDashing = false;
    private bool canShoot = true;

    private Projectile playerProjectile;
    private Rigidbody2D rigidBody2D;
    private Vector2 directionToMouse;
    private Vector2 movementVector;
    private Vector2 dashVector;
    private float angleToMouse;
    void Start()
    {
        InitPlayer();
    }

    private void InitPlayer()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerProjectile = GetComponent<Projectile>();
        playerProjectile.SetDamage(playerAttributes.attackPower);
        playerAttributes.maxHealthPoints = playerAttributes.healthPoints;
    }

    void Update()
    {
        PlayerRotationFollowMousePostion();
        DashAbility();

        if (canShoot && Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(ShootCooldown());
            ShootProjectile();
        }

        ActivateHealthRegen();
    }


    private void FixedUpdate()
    {
        ArrowMovement();
    }
    private void ActivateHealthRegen()
    {
        if (playerAttributes.healthRegen > 0)
        {
            if (regenHealthCooldown <= 0)
            {
                playerAttributes.HealthRegen();
                regenHealthCooldown = healthRegenTimer;
            }
            regenHealthCooldown -= Time.deltaTime;
        }
    }

    private void DashAbility()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && movementVector != Vector2.zero && cooldownTimeRemaining==0f)
        {
            isDashing = true;
            dashTime = playerAttributes.dashDuration;
            dashVector = movementVector.normalized * playerAttributes.dashForce;
            StartCoroutine(DashCooldown());
        }

        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                isDashing = false;
                dashVector = Vector2.zero; // Reset dash vector
            }
            else
            {
                rigidBody2D.velocity = dashVector; // Apply dash velocity
            }
        }
    }

    IEnumerator DashCooldown()
    {
        cooldownTimeRemaining = playerAttributes.dashCooldown;
        while (cooldownTimeRemaining > 0)
        {
            cooldownTimeRemaining -= Time.deltaTime;
            yield return null;
        }
        isDashing = false;
        cooldownTimeRemaining = 0f;
    }

    public float GetCooldownTime()
    {
        return cooldownTimeRemaining;
    }

    private void ArrowMovement()
    {
        if (!isDashing)
        {
            movementVector.x = Input.GetAxisRaw("Horizontal");
            movementVector.y = Input.GetAxisRaw("Vertical");
            movementVector.Normalize();

            Vector2 movement = movementVector * playerAttributes.movementSpeed * Time.fixedDeltaTime;
            rigidBody2D.MovePosition(rigidBody2D.position + movement);
        }
    }

    private void PlayerRotationFollowMousePostion()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //getting the position of the ouse from the screen to world point

        // Calculate the direction from the player to the mouse
        directionToMouse = mousePosition - rigidBody2D.position;

        // Calculate the angle
        angleToMouse = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        // Rotate the player to face the mouse cursor
        rigidBody2D.rotation = angleToMouse - 90f;
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false; // Prevent further shooting
        yield return new WaitForSeconds(playerAttributes.attackRate);
        canShoot = true; // Allow shooting again after the delay
    }

    private void ShootProjectile()
    {
        float spawnDistance = 0.25f; // from the player
        // Calculate the spawn position with the offset
        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y) + directionToMouse * spawnDistance;
        //Angle for rotation for the projectile
        Quaternion projectileRotation = Quaternion.Euler(new Vector3(0, 0, angleToMouse - 90));
        // Instantiate the projectile at the calculated spawn position
        GameObject projectile = Instantiate(playerProjectile.projectile, spawnPosition, projectileRotation);
        // Add a Rigidbody2D to the circle
        Rigidbody2D rb = projectile.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        // Set the velocity of the circle towards the mouse direction
        rb.velocity = directionToMouse * playerProjectile.projectileSpeed;
    }

    public Vector3 GetPlayerPosition()
    {
        if (this != null)
        {
            return transform.position;
        }
        return Vector3.zero;
    }

}
