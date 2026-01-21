using UnityEngine;

[CreateAssetMenu(
    fileName = "SimulationConfig",
    menuName = "MiniSimulationShowcase/Simulation Config"
)]
public class SimulationConfig : ScriptableObject
{
    [Header("Grid Settings")]
    [Min(1)] public int gridWidth = 5;
    [Min(1)] public int gridDepth = 5;
    [Min(0.1f)] public float cellSize = 1.5f;

    [Header("Object Settings")]
    [Min(1)] public int maxObjects = 50;
    public GameObject[] spawnPrefabs;

    [Header("Dynamic Mode Settings")]
    [Min(0.1f)] public float spawnInterval = 0.5f;
}
