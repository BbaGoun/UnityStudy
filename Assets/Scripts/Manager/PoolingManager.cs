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
    public int initCount = 3;
    public int goal = 5;
    int count = 0;
    public Transform spawnRegion;
    MeshCollider col;
    Vector3 center;
    Bounds bounds;

    public GameObject obstacle;

    private void Awake()
    {
        Instance = this;
        col = spawnRegion.GetComponent<MeshCollider>();
        center = spawnRegion.position;
        bounds = col.bounds;
        Initialize(initCount);
    }

    private void Start()
    {
        for(int i=0; i<initCount; i++)
        {
            Debug.Log("Pooling");
            GetObject();
        }
    }

    GameObject GetObject()
    {
        if(enemyQueue.Count > 0)
        {
            GameObject obj = enemyQueue.Dequeue();
            obj.transform.position = GetRandomPosition();
            obj.SetActive(true);
            return obj;
        }
        return null;
    }

    Vector3 GetRandomPosition()
    {
        if (spawnRegion == null)
            return Vector3.zero;

        float randX = Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
        float randZ = Random.Range(center.z - bounds.extents.z, center.z + bounds.extents.z);
        Vector3 randomPosition = new Vector3(randX, 0.2f, randZ);
        return randomPosition;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        enemyQueue.Enqueue(obj);
        count++;
        if (count < goal)
            GetObject();
        else
        {
            Destroy(obstacle);
            Destroy(this.gameObject);
        }
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
        GameObject newEnemy = Instantiate(enemyPrefab) as GameObject;
        newEnemy.SetActive(false);
        newEnemy.transform.SetParent(transform);

        return newEnemy;
    }
}
