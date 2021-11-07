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

    [Space]
    public thumb_up ThumbUp, ThumbDown;
    public int spawnCount = 3;

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

    public Vector2 GetRandomLocation()
    {
        return gridCellList[random.Next(gridCellList.Count)].location;
        
    }

    
    public int GetRandomNum()
    {
        return random.Next(1, 2);
    }

    IEnumerator CreatePathRoutine() {

        gridCellList.Clear();
        Vector2 currentPosition = startLocation.transform.position;
        Vector2 currentPosition1 = startLocation.transform.position;
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
            
        }

    }
}
