using System;
using UnityEngine;

public class ProceduralTerrainGenerator : MonoBehaviour
{
    [SerializeField] int _width;
    [SerializeField] int _height;
    [SerializeField] int _depth;
    [SerializeField] float _scale;

    [SerializeField] float _xOffset;
    [SerializeField] float _yOffset;

    private Terrain _terrain;

    void Awake()
    {
        _terrain = GetComponent<Terrain>();
    }

    void Update()
    {
        _terrain.terrainData = GenerateTerrain(_terrain.terrainData);
    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = _width + 1;

        terrainData.size = new Vector3(_width, _depth, _height);
        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                heights[x, y] = GetHighAtPoint(x, y);
            }
        }

        return heights;
    }

    private float GetHighAtPoint(int x, int y)
    {
        float xCoord = (float)x / _width * _scale + _xOffset;
        float yCoord = (float)y / _height * _scale + _yOffset;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
