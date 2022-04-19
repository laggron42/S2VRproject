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


    private int width;
    private int height;
    private int[,] grid;

    public TilesManager(int width, int height)
    {
        this.height = height;
        this.width = width;

        grid = new int[width, height];
    }
}
