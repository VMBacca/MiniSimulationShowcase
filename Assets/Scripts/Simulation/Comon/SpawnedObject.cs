using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    public Vector3 GridPosition { get; private set; }

    private Renderer cachedRenderer;
    private MaterialPropertyBlock propertyBlock;

    private void Awake()
    {
        cachedRenderer = GetComponentInChildren<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
    }

    public void Initialize(Vector3 gridPosition, Color color)
    {
        GridPosition = gridPosition;
        ApplyColor(color);
    }

    private void ApplyColor(Color color)
    {
        if (cachedRenderer == null)
            return;

        cachedRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor("_Color", color);
        cachedRenderer.SetPropertyBlock(propertyBlock);
    }
}
