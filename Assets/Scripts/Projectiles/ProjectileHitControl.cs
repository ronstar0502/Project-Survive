using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileHitControl : MonoBehaviour
{
    //public GameObject projectileShooter;
    public float projectileDamage;
    private LevelManager levelManager;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        levelManager = FindObjectOfType<LevelManager>();

    }

    private void Update()
    {
        DestroyProjectileOutsideBounds();
    }

    private void DestroyProjectileOutsideBounds()
    {
        // Convert the projectile's position to viewport space
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Check if the projectile is outside the viewport
        //Viewports cords are between 0-1
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && this.tag == "Player Projectile") //player hit enemy
        {
            Enemy enemy = other.GetComponent<Enemy>();
            PlayerController player = FindObjectOfType<PlayerController>();
            enemy.enemyAttributes.healthPoints-= player.playerAttributes.attackPower;
            if (enemy.enemyAttributes.healthPoints <= 0)
            {
                enemy.OnDeath();
                levelManager.SetEnemyCount();
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "Player" && this.tag == "Enemy Projectile") //enemy hit player
        {
            PlayerController player = other.GetComponent<PlayerController>();
            //Enemy enemyShooter = projectileShooter.GetComponent<Enemy>();
            //float enemyDamage = enemyShooter.enemyAttributes.attackPower;
            player.playerAttributes.healthPoints -= projectileDamage;
            /*if (enemyShooter != null)
            {
                player.playerAttributes.healthPoints-= enemyShooter.enemyAttributes.attackPower;
            }*/
            if (player.playerAttributes.healthPoints <= 0)
            {
                Destroy(other.gameObject);
                SceneManager.LoadScene("MainMenu");
            }
            Destroy(this.gameObject);
        }

        /*if (other.tag == "Player Projectile" && this.tag == "Enemy Projectile") //projectile hit projectile
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }*/

        if(other.tag == "Wall" && (this.tag == "Enemy Projectile" || this.tag == "Player Projectile"))
        {
            Destroy(this.gameObject);
        }

        if (other.tag == "MapBorder" && (this.tag == "Enemy Projectile" || this.tag == "Player Projectile"))
        {
            Destroy(this.gameObject);
        }
    }
}
