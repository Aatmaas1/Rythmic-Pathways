using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_GRID : MonoBehaviour
{
    public int gridSizeX = 10;
    public int gridSizeY = 10;
    public float cellSize = 1f;

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 cellPosition = new Vector3(x * cellSize, 0f, y * cellSize);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = cellPosition;
                cube.transform.localScale = new Vector3(cellSize, cellSize, cellSize);
                cube.transform.parent = transform;
            }
        }
    }
}
