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

    public int width = 10;
    public int height = 10;
    private int[,] grid;
    public float cellsize = 1f;

    void Start()
    {
        grid = new int[width, height];
        grid[16, 16] = 1;

        tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
        tex.filterMode = FilterMode.Point;
        GenerateTex();
        map.material.SetTexture("_Tilemap", tex);

        SetValue(2, 1, 0);
    }


    private Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellsize;
    }
    public void GetXY(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellsize);
        z = Mathf.FloorToInt(worldPosition.z / cellsize);
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
        GetXY(worldPosition, out x, out z);
        SetValue(x, z, value);
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
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Color c = (grid[x, z] == 0) ? Color.green : Color.red;

                tex.SetPixel(x, z, c);
            }
        }

        texUpdated = false;
        tex.Apply();
    }
}
