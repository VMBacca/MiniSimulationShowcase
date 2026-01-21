using System.Collections.Generic;
using UnityEngine;

public class DynamicSimulationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SimulationConfig config;
    [SerializeField] private ObjectCounterUI counterUI;
    [SerializeField] private Transform spawnParent;

    private GridSystem grid;
    private Queue<GameObject> activeObjects;
    private ColorProvider colorProvider;

    private float spawnTimer;

    private void Start()
    {
        if (config == null)
        {
            Debug.LogError("SimulationConfig not assigned", this);
            enabled = false;
            return;
        }

        if (counterUI == null)
        {
            Debug.LogError("ObjectCounterUI not assigned", this);
            enabled = false;
            return;
        }

        if (spawnParent == null)
        {
            spawnParent = transform;
        }

        grid = new GridSystem(config.gridWidth, config.gridDepth, config.cellSize, Vector3.zero);
        activeObjects = new Queue<GameObject>();
        colorProvider = new ColorProvider();

        counterUI.UpdateCounter(activeObjects.Count, config.maxObjects);
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= config.spawnInterval)
        {
            spawnTimer = 0f;
            SpawnStep();
        }
    }

    private void SpawnStep()
    {
        // Enforce max object limit (Firs In First Out)
        if (activeObjects.Count >= config.maxObjects)
        {
            RemoveOldest();
        }

        if (!grid.TryGetFreePosition(out Vector3 position))
            return;

        GameObject prefab = config.spawnPrefabs[Random.Range(0, config.spawnPrefabs.Length)];

        GameObject obj = Instantiate(prefab, position, Quaternion.identity, spawnParent);

        SpawnedObject spawned = obj.GetComponent<SpawnedObject>();
        if (spawned != null)
        {
            spawned.Initialize(position, colorProvider.GetRandomColor());
        }

        activeObjects.Enqueue(obj);

        counterUI.UpdateCounter(activeObjects.Count, config.maxObjects);
    }

    private void RemoveOldest()
    {
        if (activeObjects.Count == 0)
            return;

        GameObject oldest = activeObjects.Dequeue();

        SpawnedObject spawned = oldest.GetComponent<SpawnedObject>();
        if (spawned != null)
        {
            grid.ReleasePosition(spawned.GridPosition);
        }
        else
        {
            // Fallback safety
            grid.ReleasePosition(oldest.transform.position);
        }

        Destroy(oldest);
    }
}
