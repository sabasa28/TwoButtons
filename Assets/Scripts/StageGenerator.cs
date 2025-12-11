using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] stagePiecePrefabs;
    [SerializeField] float xSpawnPos;
    [SerializeField] float ySpawnPos;
    [SerializeField] float stagePieceHeight;
    [SerializeField] List<GameObject> instantiatedStagePieces = new List<GameObject>();
    [SerializeField] Transform lava;
    [SerializeField] float deepnessInLavaToDestroyStagePiece;
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

        instantiatedStagePieces.Add(Instantiate(stagePiecePrefabs[randomNum], new Vector3(xSpawnPos, ySpawnPos, 0.0f), Quaternion.identity));
        ySpawnPos += stagePieceHeight;
    }

    private void Update()
    {
        if (instantiatedStagePieces.Count > 1 && lava.position.y > instantiatedStagePieces[0].transform.position.y + deepnessInLavaToDestroyStagePiece)
        {
            GameObject stagePieceToDestroy = instantiatedStagePieces[0];
            instantiatedStagePieces.RemoveAt(0);
            Destroy(stagePieceToDestroy);
        }
    }
}
