using UnityEngine;

public class DynamicSpawner : ISpawner
{
    private readonly SimulationConfig config;
    private readonly IGridProvider grid;
    private readonly Transform parent;

    public bool TrySpawn()
    {
        if (!grid.TryGetFreePosition(out Vector3 position))
            return false;

        GameObject prefab =
            config.spawnPrefabs[Random.Range(0, config.spawnPrefabs.Length)];

        Object.Instantiate(prefab, position, Quaternion.identity, parent);
        return true;
    }
}
