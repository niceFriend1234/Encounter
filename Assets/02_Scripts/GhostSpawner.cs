using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;

public class GhostSpawner : MonoBehaviour
{
    public float SpawnTimer = 1f;
    public GameObject prefabToSpawner;

    public float minEdgeDistace = 0.3f;
    public MRUKAnchor.SceneLabels spawnLabels;
    public float normalOffset;

    public int spawnTry = 1000;

    private float timer;
    void Start()
    {

    }

    void Update()
    {
        if (!MRUK.Instance && !MRUK.Instance.IsInitialized) return;

        timer += Time.deltaTime;
        if (timer >= SpawnTimer)
        {
            SpawnGhost();
            timer -= SpawnTimer;
        }
    }

    public void SpawnGhost()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        int currentTry = 0;
        while (currentTry < spawnTry)
        {

        bool hasFoundPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.VERTICAL, minEdgeDistace, LabelFilter.Included(spawnLabels), out Vector3 pos,out Vector3 norm);

        if (hasFoundPosition)
        {
            Vector3 randomPositionNormalOffset = pos + norm * normalOffset;
            randomPositionNormalOffset.y = 0;

            Instantiate(prefabToSpawner, randomPositionNormalOffset, Quaternion.identity);
                return;
        } else
            {
                currentTry++;
            }
        }
    }
}