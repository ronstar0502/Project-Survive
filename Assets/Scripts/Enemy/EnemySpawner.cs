using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public int spawnAmount = 10;
    private int spawnCount = 0;
    public float spawnInterval = 1f;
    public float borderOffset = 1f;
    //public float spawnRadius = 5f;

    private Camera playerCamera;
    private float timer;
    void Start()
    {
        playerCamera = Camera.main;
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && spawnAmount > 0)
        {
            SpawnEnemy();
            spawnAmount--;
            timer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = GetSidePosition();
        Vector2 validPosition = FindValidSpawnPosition(spawnPosition);
        if (validPosition!= Vector2.zero)
        {
            Instantiate(enemyPrefab, validPosition, Quaternion.identity);  
            spawnCount++;
        }
    }

    public int GetEnemySpawnCount()
    {
        return spawnCount;
    }
    private Vector2 FindValidSpawnPosition(Vector2 proposedPosition, float checkRadius = 0.5f, int maxAttempts = 16)
    {
        int attempts = 0;
        while (attempts < maxAttempts)
        {
            if (Physics2D.OverlapCircle(proposedPosition, checkRadius) == null)
            {
                return proposedPosition;
            }
            // Adjust proposedPosition by a small random amount within a certain range
            proposedPosition += new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
            attempts++;
        }
        return Vector2.zero; // Return a default position if a valid position isn't found
    }

    private Vector2 GetSidePosition()
    {
        List<Vector2> availableSides= GetRandomAvailableCameraSidesToSpawn();
        Vector2 selectedSide;
        if (availableSides.Count !=0)
        {
            if(availableSides.Count==1)
            {
                return availableSides[0];
            }
            if (availableSides.Count > 1)
            {
                int randomSide = Random.Range(0, availableSides.Count-1);
                selectedSide = availableSides[randomSide];
                return selectedSide;
            }
                    
        }
        return Vector2.zero;
        
    }
    private bool HasHitMapBorder(Vector2 cameraBorder)
    {
        Vector2 cameraCenter = playerCamera.transform.position;
        Vector2 direction = (cameraBorder - cameraCenter).normalized;
        float distance = Vector2.Distance(cameraCenter, cameraBorder);
        RaycastHit2D[] hitPoints = Physics2D.RaycastAll(cameraCenter, direction, distance);
        for (int i = 0; i < hitPoints.Count(); i++)
        {
            if (hitPoints[i].collider != null)
            {
                if (hitPoints[i].collider.tag == "MapBorder")
                {
                    return true;
                }
            }
        }
        return false;
    }
    private List<Vector2> GetRandomAvailableCameraSidesToSpawn()
    {
        float cameraHeight = 2f * playerCamera.orthographicSize;
        float cameraWidth = cameraHeight * playerCamera.aspect;
        Vector2 cameraCenter = playerCamera.transform.position;
        List<Vector2> sidesToSpawn = new List<Vector2>();
        for(int i = 1; i <= 4; i++)
        {
            float x, y;
            Vector2 cameraBorder;
            switch (i)
            {
                case 1: // Top
                    x = Random.Range(-cameraWidth / 2, cameraWidth / 2);
                    y = (cameraHeight / 2)+ borderOffset;
                    cameraBorder = new Vector2(x, y) + cameraCenter;

                    if (!HasHitMapBorder(cameraBorder))
                    {
                        sidesToSpawn.Add(cameraBorder);
                    }
                    break;
                case 2: // Bottom
                    x = Random.Range(-cameraWidth / 2, cameraWidth / 2);
                    y = (-cameraHeight / 2)- borderOffset;
                    cameraBorder = new Vector2(x, y) + cameraCenter;

                    if (!HasHitMapBorder(cameraBorder))
                    {
                        sidesToSpawn.Add(cameraBorder);
                    }
                    break;
                case 3: // Left
                    x = (-cameraWidth / 2) - borderOffset;
                    y = Random.Range(-cameraHeight / 2, cameraHeight / 2);
                    cameraBorder = new Vector2(x, y) + cameraCenter;

                    if (!HasHitMapBorder(cameraBorder))
                    {
                        sidesToSpawn.Add(cameraBorder);
                    }
                    break;
                case 4: // Right
                    x = (cameraWidth / 2)+ borderOffset;
                    y = Random.Range(-cameraHeight / 2, cameraHeight / 2);
                    cameraBorder = new Vector2(x, y) + cameraCenter;

                    if (!HasHitMapBorder(cameraBorder))
                    {
                        sidesToSpawn.Add(cameraBorder);
                    }
                    break;
            }
        }
        return sidesToSpawn;
    } 
}
