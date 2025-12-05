using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] stagePiecePrefabs;
    [SerializeField] float xSpawnPos;
    [SerializeField] float ySpawnPos;
    [SerializeField] float stagePieceHeight;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            InstantiateStagePiece();
        }
    }

    public void InstantiateStagePiece()
    {
        int randomNum = Random.Range(0, stagePiecePrefabs.Length);

        Instantiate(stagePiecePrefabs[randomNum], new Vector3(xSpawnPos, ySpawnPos, 0.0f), Quaternion.identity);
        ySpawnPos += stagePieceHeight;
    }
}
