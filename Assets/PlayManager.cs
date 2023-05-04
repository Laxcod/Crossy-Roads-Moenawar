using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayManager : MonoBehaviour
{
    [SerializeField] Duck duck;
    [SerializeField] List<Terrain> terrainList ;
    [SerializeField] int InitialGrassCount = 5; 
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance = -4;
    [SerializeField] int forwardViewDistance = 15;
    [SerializeField, Range(0,1)] float treeProbability;

    Dictionary<int, Terrain> activeTerrainDict = new Dictionary<int, Terrain>(20);
    [SerializeField] private int travelDistance;

    public UnityEvent<int, int> OnUpdateTerrainLimit;
    private void Start() 
    {

        // create initial Grass posisition -4 --- 4
        for (int zPos = backViewDistance; zPos < InitialGrassCount; zPos++)
        {

          var terrain = Instantiate (terrainList[0]);

          terrain.transform.position = new Vector3(0,0,zPos);

          if(terrain is Grass grass)
            grass.SeTreePercentage(zPos< -1 ? 1 : 0);

          terrain.Generate(horizontalSize);

          activeTerrainDict[zPos] = terrain;
        }

      //4 --- 15
       for (int zPos = InitialGrassCount; zPos < forwardViewDistance; zPos++)
        {
          
          SpawnRandomTerrain(zPos);
        }
    }

    private Terrain SpawnRandomTerrain(int zPos)
    {
      Terrain comparatorTerrain = null;
      int randomIndex;
      for (int z = -1; z >= -3; z--)
      {
        var checkPos = zPos + z;
        System.Type comparatorType = comparatorTerrain.GetType();
        System.Type checkType = activeTerrainDict[checkPos].GetType();

        if(comparatorTerrain == null)
        {
          comparatorTerrain = activeTerrainDict[checkPos];
          continue;
        }
        else if(comparatorType != checkType)
        {
           randomIndex = Random.Range(0, terrainList.Count);
           return SpawnTerrain(terrainList[randomIndex], zPos);
        }
        else
        {
          continue;
        }
      }

      var candidateTerrain = new List<Terrain>(terrainList);
     
      for (int i = 0; i < candidateTerrain.Count; i++)
      {
        System.Type comparatorType = comparatorTerrain.GetType();
        System.Type checkType = candidateTerrain[i].GetType();
        if(comparatorType == checkType)
          {
            candidateTerrain.Remove(candidateTerrain[i]);
            break;
          }
      }

           randomIndex = Random.Range(0, candidateTerrain.Count);
          return SpawnTerrain(candidateTerrain[randomIndex], zPos);
    }

    public Terrain SpawnTerrain(Terrain terrain, int zPos)
    {
           terrain = Instantiate (terrain);
           terrain.transform.position = new Vector3(0,0,zPos);
           terrain.Generate(horizontalSize);
           activeTerrainDict[zPos] = terrain;
           return terrain;
    }


    public void UpdateTravelDistance(Vector3 targetPosition)
    {
      
      if (targetPosition.z > travelDistance)
      {
        travelDistance = Mathf.CeilToInt (targetPosition.z);
        UpdateTerrain();
      }
    }

    public void UpdateTerrain()
    {
      var destroyPos = travelDistance- 1 + backViewDistance;
      Destroy(activeTerrainDict[destroyPos].gameObject);
      activeTerrainDict.Remove(destroyPos);

     var spawnPosition = travelDistance - 1 + forwardViewDistance; 
      SpawnRandomTerrain(spawnPosition);

      OnUpdateTerrainLimit.Invoke(horizontalSize,travelDistance + backViewDistance);
    }
}
