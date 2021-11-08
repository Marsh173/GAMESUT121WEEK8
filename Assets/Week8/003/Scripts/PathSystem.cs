using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathSystem : MonoBehaviour {

    public enum SeedType { RANDOM, CUSTOM }
    [Header("Random Related Stuff")]
    public SeedType seedType = SeedType.RANDOM;
    System.Random random;
    public int seed = 0;

    [Space]
    public bool animatedPath;
    public List<MyGridCell> gridCellList = new List<MyGridCell>();
    public int pathLength = 10;
    [Range(1.0f, 10.0f)]
    public float cellSize = 1.0f;

    public Transform startLocation;
    public GameObject thingToSpawn;
    public GameObject myCoolObject;

    [Space]
    public thumb_up ThumbUp, ThumbDown;
    public int spawnCount = 3;

    public List<GameObject> pegList = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {
        
    }

    void SetSeed() {
        if (seedType == SeedType.RANDOM) {
            random = new System.Random();
        }
        else if (seedType == SeedType.CUSTOM) {
            random = new System.Random(seed);
        }
    }

    void CreatePath() {

        gridCellList.Clear();
        Vector2 currentPosition = startLocation.transform.position;
        gridCellList.Add(new MyGridCell(currentPosition));

        

        for (int i = 0; i < pathLength; i++) {

            int n = random.Next(100);

            if (n.IsBetween(0, 49)) {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }

            gridCellList.Add(new MyGridCell(currentPosition));

        }
    }
    /*
    public Vector2 GetRandomLocation()
    {
        return gridCellList[random.Next(gridCellList.Count)].location;
        
    }
    
    
    public int GetRandomNum()
    {
        return random.Next(1, 2);
    }
    */

    IEnumerator CreatePathRoutine() {

        gridCellList.Clear();

        for(int i = 0; i < pegList.Count; i++)
        {
            Destroy(pegList[i]);
        }
        pegList.Clear();




        Vector2 currentPosition = startLocation.transform.position;
        Vector2 currentPosition1 = startLocation.transform.position;
        gridCellList.Add(new MyGridCell(currentPosition));

        //addCollider
        GameObject go = Instantiate(new GameObject("Block"), currentPosition, Quaternion.identity);
        BoxCollider2D bcLeft = go.AddComponent<BoxCollider2D>();
        bcLeft.transform.localPosition = new Vector3(go.transform.position.x - (0.5f * cellSize), bcLeft.transform.position.y);
        bcLeft.size = new Vector2(0.1f, cellSize);



        //add prefab
        Instantiate(thingToSpawn, currentPosition, Quaternion.identity);
        pegList.Add(thingToSpawn);

        for (int i = 0; i < pathLength; i++) {

            int n = random.Next(100);

            if (n>0 && n < 49) {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }

            gridCellList.Add(new MyGridCell(currentPosition));
            yield return new WaitForSeconds(0.02f);
        }

        for (int i = 0; i < pathLength; i++)
        {

            int n = random.Next(100);

            if (n.IsBetween(0, 49))
            {
                currentPosition1 = new Vector2(currentPosition1.x - cellSize, currentPosition1.y);
            }
            else
            {
                currentPosition1 = new Vector2(currentPosition1.x, currentPosition1.y - cellSize);
            }

            gridCellList.Add(new MyGridCell(currentPosition1));

            int y = random.Next(100);
            Debug.Log($"y is equal to {y}");
            if (y>0 && y < 49)
            {
                Instantiate(thingToSpawn, currentPosition, Quaternion.identity);
                Debug.Log("Spawn!");
                pegList.Add(go);
            }
            else
            {
                Debug.Log("Didn't spawn.");
            }
            go = Instantiate(new GameObject("Block"), currentPosition, Quaternion.identity);

            yield return new WaitForSeconds(0.02f);
        }
    }



    private void OnDrawGizmos() {
        for (int i = 0; i < gridCellList.Count; i++) {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(gridCellList[i].location, Vector3.one * cellSize);
            Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            Gizmos.DrawCube(gridCellList[i].location, Vector3.one * cellSize);
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SetSeed();

            if (animatedPath)
                StartCoroutine(CreatePathRoutine());
            else
                CreatePath();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            int x = UnityEngine.Random.Range(0, gridCellList.Count - 1);
            myCoolObject.transform.position = gridCellList[x].location;
        }

        /*
        for (int i = 0; i < spawnCount; i++)
        {
            
            
            if (GetRandomNum() == 1)
            {
                Instantiate(ThumbUp, GetRandomLocation(), Quaternion.identity);
            }
            else if(GetRandomNum() == 2)
            {
                Instantiate(ThumbDown, GetRandomLocation(), Quaternion.identity);
            }
            
        }*/

    }
}
