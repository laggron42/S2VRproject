using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    #region Singleton
    public static TilesManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    // Map renderer
    public MeshRenderer map;
    // Map visualisation texture
    private Texture2D tex;
    // If the map was updated (and the tex needs updating)
    private bool texUpdated = true;
    // If the visualisation tex is active
    //(needs to be set to true each time we want to add a tower)
    private bool active = false;
    public int width = 40;
    public int height = 40;
    private int size;

    private int[,] grid;
    public float cellsize = 1f;

    void Start()
    {
        grid = new int[width, height];
        grid[26, 24] = 1;
        size = 600 / width;


        tex = new Texture2D(1000, 1000, TextureFormat.ARGB32, false);
        tex.filterMode = FilterMode.Point;
        GenerateTex();
        map.material.SetTexture("_Tilemap", tex);

        SetValue(2, 1, 0);
    }


    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * 3 + new Vector3(2, 0, 1);
    }
    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = (int) worldPosition.x;
        z = (int) worldPosition.z;
        Debug.Log(x + " " + z);
        x = (x - 1) / 3;
        z = (z) / 3;
        
    }


    public void SetValue(int x, int z, int value)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            grid[x, z] = value;
            texUpdated = true;
        }
    }
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        SetValue(x, z, value);
    }

    public bool CanPlaceTower(int x, int z)
    {
        return grid[x, z] == 0;
    }
    
    public bool CanPlaceTower(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return CanPlaceTower(x, z);
    }

    

    /**
     * Enable Visualisation map and updates it if necessary
     */
    public void EnterEditMode()
    {
        if (texUpdated)
        {
            GenerateTex();
            map.material.SetTexture("_Tilemap", tex);
        }

        map.material.SetFloat("_TilemapActive", 1);
    }

    /**
     * Disable Visualisation map
     */
    public void ExitEditMode()
    {
        map.material.SetFloat("_TilemapActive", 0);
    }

    /**
     * Updates the visualisation texture if necessary
     * Writes a green square if cell available, red othewise
     */
    public void GenerateTex()
    {
        //int size = 600 / width;
        for (int z = 0; z < 1000; z++)
        { 
            for (int x = 0; x < 1000; x++)
            {
                Color c;   
                if (x <= 200 || z <= 200 || x >= 800 || z >= 800)
                    c = Color.red;
                else
                {
                    int wx = width - (x - 200) / size - 1;
                    int hz = height - (z - 200) / size - 1;
                    c = (grid[hz, wx] == 0) ? Color.green : Color.red;
                    
                    if ((x - 20) % size == 0 || (z - 20) % size == 0)
                        c = Color.gray;          
                }

                tex.SetPixel(x, z, c);
            }
        }

        texUpdated = false;
        tex.Apply();
    }
}
