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

    // Map visualisation texture
    private Texture2D tex;
    // If the map was updated (and the tex needs updating)
    private bool texUpdated = false;
    // If the visualisation tex is active
    //(needs to be set to true each time we want to add a tower)
    private bool active = false;

    public int width = 10;
    public int height = 10;
    private int[,] grid;
    public float cellsize = 10f;

    //TOREMOVE
    private TextMesh[,] debugtextaray;

    void Start()
    {
        grid = new int[width, height];

        GenerateTex();
        Shader.SetGlobalTexture("_Tilemap", tex);

        /**
        FOR DEBUG ONLY
        to visulize the differtents value
        **/
        debugtextaray = new TextMesh[width, height];
        Debug.Log(width + " " + height);
        for(int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                debugtextaray[x,y] = createText(grid[x, y].ToString(), Color.white, null, GetWorldPosition(x, y) + new Vector3(cellsize, cellsize) * .5f, 20,  TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

        SetValue(2, 1, 0);
    }


    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellsize;
    }
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellsize);
        y = Mathf.FloorToInt(worldPosition.y / cellsize);
    }

    private TextMesh createText(string text, Color color, Transform parent = null,  Vector3 localPosi = default(Vector3),  int fontSize = 40, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 0)
    {
        if (color == null)
            color = Color.white;
        GameObject go = new GameObject("Word_text", typeof(TextMesh));
        go.transform.SetParent(parent, false);
        go.transform.localPosition = localPosi;
        TextMesh textMesh = go.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        // mettre a l'horizontale
        textMesh.transform.localEulerAngles = new Vector3(1, 0, 1);
        return textMesh;
    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            grid[x, y] = value;
            debugtextaray[x, y].text = grid[x, y].ToString();
        }
    }
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    /**
     * Updates the visualisation texture if necessary
     * Writes a green square if cell available, red othewise
     */
    public async void GenerateTex()
    {
        if (!texUpdated)
            return;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color c = (grid[x / (int)cellsize, y / (int)cellsize] == 0) ?
                    Color.green : Color.red;
                
                tex.SetPixel(x, y, c);
            }
        }

        tex.Apply();
    }
}
