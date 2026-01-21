using UnityEngine;

public class ColorProvider
{
    public Color GetRandomColor()
    {
        return Random.ColorHSV(
            0f, 1f,
            0.6f, 1f,
            0.6f, 1f
        );
    }
}
