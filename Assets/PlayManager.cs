using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] Grass grassPrefab;
    [SerializeField] Road roadPrefab;
    [SerializeField] int InitialGrassCount = 5; 
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance = -4;
    [SerializeField] int forwardViewDistance = 15;
    [SerializeField, Range(0,1)] float treeProbability;

    private void Start() 
    {
        // create initial Grass posisition -4 --- 4
        for (int zPos = backViewDistance; zPos < InitialGrassCount; zPos++)
        {
          var grass = Instantiate (grassPrefab);

          grass.transform.position = new Vector3(0,0,zPos);

          grass.SeTreePercentage(zPos< -1 ? 1 : 0);

          grass.Generate(horizontalSize);
        }

      //4 --- 15
       for (int zPos = InitialGrassCount; zPos < forwardViewDistance; zPos++)
        {
          var terrain = Instantiate (roadPrefab);

          terrain.transform.position = new Vector3(0,0,zPos);



          terrain.Generate(horizontalSize);
        }

    }
}
