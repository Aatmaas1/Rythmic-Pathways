using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_GridShiny : MonoBehaviour
{
    public int pathLength; // Longueur du chemin
    public float cellSize; // Taille de la cellule
    public GameObject pathPrefab; // Préfabriqué du chemin

    private List<GameObject> pathBlocks;

    void Start()
    {
        GeneratePath();
    }

    void GeneratePath()
    {
        pathBlocks = new List<GameObject>();

        Vector3 currentPosition = Vector3.zero;

        for (int i = 0; i < pathLength; i++)
        {
            // Instancier le bloc du chemin à la position actuelle
            GameObject pathBlock = Instantiate(pathPrefab, currentPosition, Quaternion.identity);
            pathBlocks.Add(pathBlock);

            // Calculer la prochaine position du chemin
            Vector3 nextPosition = GetNextPosition(currentPosition);

            // Passer à la prochaine position
            currentPosition = nextPosition;
        }
    }

    Vector3 GetNextPosition(Vector3 currentPosition)
    {
        // Générer aléatoirement la direction de la prochaine position
        int direction = Random.Range(0, 4); // 0: haut, 1: bas, 2: gauche, 3: droite

        switch (direction)
        {
            case 0:
                return currentPosition + Vector3.forward * cellSize;
            case 1:
                return currentPosition - Vector3.forward * cellSize;
            case 2:
                return currentPosition - Vector3.right * cellSize;
            case 3:
                return currentPosition + Vector3.right * cellSize;
            default:
                return currentPosition;
        }
    }

    public void CheckPathBlocks()
    {
        foreach (GameObject block in pathBlocks)
        {
            if (block != null)
            {
                // Au moins un bloc existe encore, donc le chemin n'est pas encore complètement détruit
                return;
            }
        }

        // Tous les blocs ont été détruits, régénérez le chemin
        RegeneratePath();
    }

    void RegeneratePath()
    {
        foreach (GameObject block in pathBlocks)
        {
            Destroy(block);
        }

        // Générer un nouveau chemin sinueux
        GeneratePath();
    }
}
