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
    [SerializeField] bool _animateTerrain;

    private Terrain _terrain;
    private Vector2 _animateDirection;

    void Awake()
    {
        _terrain = GetComponent<Terrain>();
    }

    void Start()
    {
        _xOffset = UnityEngine.Random.Range(0, 9999f);
        _yOffset = UnityEngine.Random.Range(0, 9999f);

        if (_animateTerrain)
        {
            InvokeRepeating("SortTerrain", 0f, 1f);
        }
    }

    void Update()
    {
        _terrain.terrainData = GenerateTerrain(_terrain.terrainData);
        if (_animateTerrain)
        {
            _xOffset += _animateDirection.x * Time.deltaTime;
            _yOffset += _animateDirection.y * Time.deltaTime;
        }
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

    private void SortTerrain()
    {
        _xOffset = UnityEngine.Random.Range(0, 9999f);
        _yOffset = UnityEngine.Random.Range(0, 9999f);

        _depth = UnityEngine.Random.Range(0, 35);
        _scale = UnityEngine.Random.Range(5, 15);

        _animateDirection = new Vector2(UnityEngine.Random.Range(-0.6f, 0.6f), UnityEngine.Random.Range(-0.6f, 0.6f));
    }
}
