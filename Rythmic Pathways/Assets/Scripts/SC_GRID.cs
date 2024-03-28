using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class SC_GRID : MonoBehaviour
{
    //fait par IA
    public int gridSizeX;
    public int gridSizeY;
    public float cellSize;
   
    //fait par humain
    public float cellSizeY;
    public bool cubeColor;
    public Color color1;
    public Color color2;


    //fait par IA
    private void Start()
    {
        CreateGrid();
        cubeColor = false;
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            //fait par humain
            if(cubeColor == false)
            {
                cubeColor = true;
            }
            else
            {
                cubeColor = false;
            }

            //fait par IA
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 cellPosition = new Vector3(x * cellSize, 0f, y * cellSize);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                //fait par humain
                Renderer renderer = cube.GetComponent<Renderer>();

                if (cubeColor == false)
                {
                    renderer.material.color = color1;
                    cubeColor = true;
                }
                else
                {
                    renderer.material.color = color2;
                    cubeColor = false;
                }
                
                //fait par IA
                cube.transform.position = cellPosition;
                cube.transform.localScale = new Vector3(cellSize, cellSizeY, cellSize);
                cube.transform.parent = transform;
            }
        }
    }
}
