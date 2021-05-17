using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /// <summary>
    /// write the id of the doorway trigger
    /// </summary>
    [SerializeField] int id;
    [SerializeField] float distFactor =1;
    
    [System.Serializable]
    public class EnemiesToSpawn
    {
        public GameObject enemyPrefab;
        public int spawnAmount;
    }
    
    public EnemiesToSpawn[] enemies;

    
    [Header("Debugging")]
    public List<Vector2> gridPositions;
    public List<Transform> enemiesList;
    [SerializeField] SpriteRenderer spawnerSprite;
    

    bool _spawned;
    
    private void Start()
    {
        spawnerSprite = GetComponent<SpriteRenderer>();
        Invoke(nameof(DelayedGridPos),0.1f);
        GameEvents.Current.OnDoorwayTriggerEnterSpawner += StartCor;
        GameEvents.Current.OnEnemyDeath += CheckList;
        _spawned = false;
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

    
    public event Action<int,Spawner> OnEnemiesListEmpty;
    void CheckList(Transform t)
    {
        if (enemiesList.Count == 1)
        {
            enemiesList.Remove(t);
            OnEnemiesListEmpty?.Invoke(id,this);
        }
        else
        {
            enemiesList.Remove(t);
        }
    }

    [ContextMenu("Spawn Enemies")]
    void StartCor(int id)
    {  if(id == this.id && !_spawned)
        {
            StartCoroutine(nameof(SpawnEnemies));
        }
    }
    
    IEnumerator SpawnEnemies()
    {
        _spawned = true;
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

    private void OnDestroy()
    {
        GameEvents.Current.OnDoorwayTriggerEnterSpawner -= StartCor;
        GameEvents.Current.OnEnemyDeath -= CheckList;
    }
}
