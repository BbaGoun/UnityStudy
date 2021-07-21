using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager instance;
    public static PoolingManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(PoolingManager)) as PoolingManager;

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    Queue<GameObject> enemyQueue = new Queue<GameObject>();

    [SerializeField]
    private GameObject enemyPrefab;
    public int initCount = 5;
    public Transform spawnRegion;

    private void Awake()
    {
        Instance = this;
        Initialize(initCount);
    }

    private void Update()
    {
        for(int i=0; i<initCount; i++)
        {
            GetObject();
        }
    }

    GameObject GetObject()
    {
        if(enemyQueue.Count > 0)
        {
            var obj = enemyQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.transform.position = GetRandomPosition();
            obj.SetActive(true);
            obj.GetComponent<EnemyController>().enabled = true;
            return obj;
        }
        return null;
    }

    Vector3 GetRandomPosition()
    {
        if (spawnRegion == null)
            return Vector3.zero;

        MeshCollider col = spawnRegion.GetComponent<MeshCollider>();
        Vector3 center = spawnRegion.position;
        var bounds = col.bounds;

        float randX = Random.Range(center.x - bounds.extents.x * 0.7f, center.x + bounds.extents.x * 0.7f);
        float randZ = Random.Range(center.z - bounds.extents.z * 0.7f, center.z + bounds.extents.z * 0.7f);
        Vector3 randomPosition = new Vector3(randX, 2f, randZ);
        return randomPosition;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        enemyQueue.Enqueue(obj);
    }

    void Initialize(int initCount)
    {
        for(int i=0; i<initCount; i++)
        {
            enemyQueue.Enqueue(CreateNewEnemy());
        }
    }

    GameObject CreateNewEnemy()
    {
        var newEnemy = Instantiate(enemyPrefab);
        newEnemy.SetActive(false);
        newEnemy.transform.SetParent(transform);

        return newEnemy;
    }
}
