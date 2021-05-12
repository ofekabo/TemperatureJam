using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = System.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Spawner : MonoBehaviour
{
    

    [System.Serializable]
    public class EnemiesToSpawn
    {
        public GameObject enemyPrefab;
        public int spawnAmount;
    }
    
    public EnemiesToSpawn[] enemies;
    
    [SerializeField] float distFactor;
    
    [Header("Debugging")]
    public List<Vector2> gridPositions;
    public List<Transform> enemiesList;
    [SerializeField] SpriteRenderer spawnerSprite;
    

    private void Start()
    {
        spawnerSprite = GetComponent<SpriteRenderer>();
       Invoke(nameof(DelayedGridPos),0.1f);
    }

    void DelayedGridPos()
    {
        SetGridPositions(spawnerSprite);
    }
    

    void SetGridPositions(SpriteRenderer Board)
    {
        float minX = Board.bounds.min.x;
        float maxX = Board.bounds.max.x;
        float minY = Board.bounds.min.y;
        float maxY = Board.bounds.max.y;

        for (float xIndex = minX; xIndex < maxX; xIndex += distFactor)
        {
            for (float yIndex = minY; yIndex < maxY; yIndex += distFactor)
            {
                Vector2 newSlot = new Vector2(xIndex,yIndex);
                gridPositions.Add(newSlot);
            }
        }
    }

    [ContextMenu("Spawn Enemies")]
    void StartCor()
    {
        StartCoroutine(nameof(SpawnEnemies));
    }
    
   
    IEnumerator SpawnEnemies()
    {
        foreach (var enemy in enemies)
        {
            for (int i = 0; i < enemy.spawnAmount; i++)
            {
                
                Vector2 spawnPos = GetRandomPos();
                if (spawnPos != Vector2.zero)
                {
                    GameObject newEnemy = Instantiate(enemy.enemyPrefab,spawnPos,Quaternion.identity);
                    enemiesList.Add(newEnemy.transform);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }

    Vector3 GetRandomPos()
    {
        for (int i = 0; i < gridPositions.Count; i++)
        {
            int randIndex = UnityEngine.Random.Range(0,gridPositions.Count);
            Vector2 spawnPos = gridPositions[randIndex];
            Collider2D hit = Physics2D.OverlapBox(spawnPos,Vector2.one * distFactor,90f);

            if (!hit)
            {
                Vector2 newPos = spawnPos;
                
                return new Vector3(newPos.x,newPos.y,0);
            }
        }
        return Vector3.zero;
    }
}
