using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstantiateManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [Header("Objects")]
    [SerializeField] GameObject roads;

    [SerializeField] GameObject[] coinOrBuild;

    private float roadLenght = 690;
    private float roadM = 30;


    private float coinLenght = 50;

    private void Update()
    {
        if (player != null)
        {
            EnviromentsInstantiate();
            CollettibleInstantiate();
        }
    }


    private void EnviromentsInstantiate()
    {
        if (player.position.z >= roadLenght / 5)
        {
            Instantiate(roads, transform.position + new Vector3(0, 0, roadLenght), Quaternion.identity);
            roadLenght += roadM;
        }
    }

    private void CollettibleInstantiate()
    {
        if (player.position.z >= coinLenght / 5)
        {
            List<int> usedIndices = new List<int>();

            Vector3[] positions = new Vector3[]
            {
                new Vector3(0, 0, coinLenght),
                new Vector3(6, 0, coinLenght),
                new Vector3(-6, 0, coinLenght)
            };

            foreach (Vector3 pos in positions)
            {
                int objectIndex;

                do
                {
                    objectIndex = Random.Range(0, coinOrBuild.Length);
                }
                while (usedIndices.Contains(objectIndex));

                usedIndices.Add(objectIndex);

                Instantiate(coinOrBuild[objectIndex], transform.position + pos, Quaternion.identity);
            }

            coinLenght += 60;
        }

    }
}

