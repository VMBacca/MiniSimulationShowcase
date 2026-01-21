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
        // Defensive checks
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

        // Initialize helpers
        grid = new GridSystem(config.gridWidth, config.gridDepth, config.cellSize, Vector3.zero);
        activeObjects = new Queue<GameObject>();
        colorProvider = new ColorProvider();

        // Initial counter
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
        // Enforce max object limit (FIFO)
        if (activeObjects.Count >= config.maxObjects)
        {
            RemoveOldest();
        }

        // Try to get a free grid position
        if (!grid.TryGetFreePosition(out Vector3 position))
            return;

        // Choose prefab
        GameObject prefab = config.spawnPrefabs[Random.Range(0, config.spawnPrefabs.Length)];

        // Instantiate
        GameObject obj = Instantiate(prefab, position, Quaternion.identity, spawnParent);

        // Initialize spawned object
        SpawnedObject spawned = obj.GetComponent<SpawnedObject>();
        if (spawned != null)
        {
            spawned.Initialize(position, colorProvider.GetRandomColor());
        }

        activeObjects.Enqueue(obj);

        // Update counter
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
