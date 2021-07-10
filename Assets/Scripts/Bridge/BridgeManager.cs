using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour
{
    public GameObject[] bridgePrefab;

    private Transform playerTransform;
    private float spawnZ = -6f;
    private float bridgeLength = 16.5f;
    private float safeZone = 20f;
    private int amountBridgeOnScreen = 4;
    private int lastPrefabIndex = 0;

    private List<GameObject> activeBridge;

    // Start is called before the first frame update
    private void Start()
    {
        activeBridge = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        for(int i = 0; i < amountBridgeOnScreen; i++)
        {
            if(i < 2)
            {
                SpawnBridge(0);
            } else
            {
                SpawnBridge();
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(playerTransform.position.z - safeZone > (spawnZ - amountBridgeOnScreen * bridgeLength))
        {
            SpawnBridge();
            RemoveBridge();
        }
    }

    private void SpawnBridge(int prefabIndex = -1)
    {
        GameObject go;
        if(prefabIndex == -1)
        {
            go = Instantiate(bridgePrefab [RandomPrefabIndex()]) as GameObject;
        } else
        {
            go = Instantiate(bridgePrefab[prefabIndex]) as GameObject;
        }
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += bridgeLength;
        activeBridge.Add(go);
    }

    private void RemoveBridge()
    {
        Destroy(activeBridge[0]);
        activeBridge.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if(bridgePrefab.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, bridgePrefab.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
