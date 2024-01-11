using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int width, int height, float scale)
    {
        float[,] noiseMap = new float[width, height];

        if (scale <= 1.1f)
        {
            scale = 1.1f;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / scale;
                float yCoord = (float)y / scale;

                float perlinValue = Mathf.PerlinNoise(xCoord, yCoord);

                noiseMap[x, y] = perlinValue;
            }
        }

        return noiseMap;
    }
}
