using UnityEngine;

public interface IGridProvider
{
    bool TryGetFreePosition(out Vector3 position);
    void ReleasePosition(Vector3 position);
}
