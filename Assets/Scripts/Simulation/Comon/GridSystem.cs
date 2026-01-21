using System.Collections.Generic;
using UnityEngine;

public class GridSystem : IGridProvider
{
    private readonly int width;
    private readonly int depth;
    private readonly float cellSize;
    private readonly Vector3 origin;

    private HashSet<Vector2Int> occupiedCells;
    private List<Vector2Int> freeCells;

    public GridSystem(int width, int depth, float cellSize, Vector3 origin)
    {
        this.width = width;
        this.depth = depth;
        this.cellSize = cellSize;
        this.origin = origin;

        occupiedCells = new HashSet<Vector2Int>();
        freeCells = new List<Vector2Int>();

        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                freeCells.Add(new Vector2Int(x, z));
            }
        }
    }

    public bool TryGetFreePosition(out Vector3 worldPosition)
    {
        worldPosition = Vector3.zero;

        if (freeCells.Count == 0)
            return false;

        int index = Random.Range(0, freeCells.Count);
        Vector2Int cell = freeCells[index];

        freeCells.RemoveAt(index);
        occupiedCells.Add(cell);

        worldPosition = origin + new Vector3(
            cell.x * cellSize,
            0f,
            cell.y * cellSize
        );

        return true;
    }

    public void ReleasePosition(Vector3 worldPosition)
    {
        Vector2Int cell = new Vector2Int(
            Mathf.RoundToInt((worldPosition.x - origin.x) / cellSize),
            Mathf.RoundToInt((worldPosition.z - origin.z) / cellSize)
        );

        if (occupiedCells.Remove(cell))
        {
            freeCells.Add(cell);
        }
    }
}
