using UnityEngine;

public class StaticSimulationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SimulationConfig config;
    [SerializeField] private ObjectCounterUI counterUI;
    [SerializeField] private Transform spawnParent;

    private GridSystem grid;
    private ISpawner spawner;

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

        grid = new GridSystem(config.gridWidth, config.gridDepth, config.cellSize, Vector3.zero);
        ColorProvider colorProvider = new ColorProvider();

        spawner = new StaticSpawner(config, grid, spawnParent, colorProvider);

        int spawnCount = Mathf.Min(config.maxObjects, config.gridWidth * config.gridDepth);

        for (int i = 0; i < spawnCount; i++)
        {
            if (!spawner.TrySpawn())
                break;

            // Update counter after each spawn
            counterUI.UpdateCounter(i + 1, spawnCount);
        }
    }
}
