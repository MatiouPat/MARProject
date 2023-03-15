using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public GameObject terrain;

    public string bitmapFilePath;
    
    private MeshFilter _meshFilter;

    private Vector3[] _meshVertices;

    private int _tileDepth;

    private int _tileWidth;

    private Texture2D _heightMap;
    
    public void Awake()
    {
        Instantiate(terrain, new Vector3(0, 0, 0), Quaternion.identity);
        _meshFilter = terrain.GetComponent<MeshFilter>();
    }

    public void Start()
    {
        _meshVertices = _meshFilter.sharedMesh.vertices;
        _tileDepth = (int)Mathf.Sqrt (_meshVertices.Length);
        _tileWidth = _tileDepth;

        if (LoadBitmapHeightMap())
        {
            TerrainUpdate();
        }
        else
        {
            print("no");
        }
    }

    private bool LoadBitmapHeightMap()
    {
        string filePath = "Assets/Maps/" + bitmapFilePath;
        if (File.Exists(filePath))
        {
            Byte[] heightMapFileData = File.ReadAllBytes(filePath);
            _heightMap = new Texture2D(2,2);
            _heightMap.LoadRawTextureData(heightMapFileData);
            print(_heightMap.width);
            return true;
        }
        return false;
    }

    private void TerrainUpdate()
    {
        int vertexIndex = 0;
        for (int zIndex = 0; zIndex < _tileDepth; zIndex++) {
            for (int xIndex = 0; xIndex < _tileWidth; xIndex++) {
                Vector3 vertex = _meshVertices[vertexIndex];
                print(ComputeHeight(xIndex, zIndex));
                _meshVertices[vertexIndex] = new Vector3(vertex.x, ComputeHeight(xIndex, zIndex), vertex.z);
                vertexIndex++;
            }
        }
        _meshFilter.sharedMesh.vertices = _meshVertices;

    }

    private float ComputeHeight(int xIndex, int zIndex)
    {
        return _heightMap.GetPixel(xIndex, zIndex).grayscale;
    }
    
    
}
