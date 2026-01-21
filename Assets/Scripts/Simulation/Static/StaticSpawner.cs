using UnityEngine;

public class StaticSpawner : ISpawner
{
    private readonly SimulationConfig config;
    private readonly IGridProvider grid;
    private readonly Transform parent;
    private readonly ColorProvider colorProvider;

    public StaticSpawner(
    SimulationConfig config,
    IGridProvider grid,
    Transform parent,
    ColorProvider colorProvider)
    {
        this.config = config;
        this.grid = grid;
        this.parent = parent;
        this.colorProvider = colorProvider;
    }

    public bool TrySpawn()
    {
        if (!grid.TryGetFreePosition(out Vector3 position))
            return false;

        GameObject prefab =
            config.spawnPrefabs[Random.Range(0, config.spawnPrefabs.Length)];

        GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity, parent);

        SpawnedObject spawned = obj.GetComponent<SpawnedObject>();
        if (spawned != null)
        {
            spawned.Initialize(position, colorProvider.GetRandomColor());
        }

        return true;
    }
}
