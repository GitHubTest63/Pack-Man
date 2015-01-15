using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator instance;
    private int[,] map;

    void awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        map = new int[,] { 
        {1, 5, 5, 5, 5, 5, 1},
        {1, 0, 0 ,-2, 0, 10, 1},
        {1, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 1},
        {1, 0, 5, 5, 5, 0, 1},
        {1, 1, -1, 1, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 1},
        {1, 5, 5, 5, 5, 5, 1}
        };
        createMap();
    }

    private void createMap()
    {
        MapGenerator mapGenerator = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
        for (int j = 0; j < map.GetLength(0); j++)
        {
            for (int i = 0; i < map.GetLength(1); i++)
            {
                int type = map[j, i];
                if (type < 0)
                {
                    //players
                    if (type == -1)
                    {
                        GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/Player_1"), new Vector3(i + 0.5f, 0.0f, map.GetLength(0) - j - 0.5f), Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/Player_2"), new Vector3(i + 0.5f, 0.0f, map.GetLength(0) - j - 0.5f), Quaternion.identity) as GameObject;
                    }
                }
                else
                {
                    string path = "Prefabs/" + map[j, i].ToString();
                    //Debug.Log(path);
                    GameObject go = GameObject.Instantiate(Resources.Load(path), new Vector3(i, 0.0f, map.GetLength(0) - j), Quaternion.identity) as GameObject;
                    go.transform.parent = transform.parent;

                    //if (type == 0)
                    //{
                    //    //Debug.Log("pop collectible");
                    //    GameObject collectible = GameObject.Instantiate(Resources.Load("Prefabs/Collectible"), new Vector3(i, 0.0f, map.GetLength(0) - j ), Quaternion.identity) as GameObject;
                    //    collectible.transform.parent = go.transform;
                    //}
                }

            }
        }
    }

    public int getType(int row, int column)
    {
        return this.map[row, column];
    }

    public int getType(Vector3 pos)
    {
        int row = (int)pos.z;
        int column = (int)pos.x;
        return this.map[row, column];
    }

    public void setType(int row, int column, int type)
    {
        this.map[row, column] = type;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
