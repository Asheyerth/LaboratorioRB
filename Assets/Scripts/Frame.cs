using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    private int width;
    private int height;
    private int squareSize;
    private square squarePrefab;
    private square obstacleprefab;
    private square[,] frameArray;

    private int level;
    private GameObject padre;



    public Frame(int width, int height, int squareSize, square squarePrefab, square obstacleprefab, int level, GameObject padre)
    {
        this.width = width;
        this.height = height;
        this.squarePrefab = squarePrefab;
        this.obstacleprefab = obstacleprefab;
        this.squareSize = squareSize;

        this.level = level;
        this.padre = padre;

        generateframe();
    }

    private void generateframe()
    {
        square squr;
        frameArray = new square[width, height];

        int cont = 0;

        int ran;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                {
                    var q = new Vector2(i, j) * squareSize;
                    squr = Instantiate(obstacleprefab, q, Quaternion.identity, padre.transform);
                    squr.Init(this, (int)q.x, (int)q.y, true);
                    squr.SetWalkable(false);
                    frameArray[i, j] = squr;
                }

                ran = Random.Range(0, 10);

                if (ran <= 2 && frameArray[i, j] == null && cont < 30 && i != 1 && j != 1 && i != height - 2 && j != width - 2)
                {
                    cont++;
                    var q = new Vector2(i, j) * squareSize;
                    squr = Instantiate(obstacleprefab, q, Quaternion.identity, padre.transform);
                    squr.Init(this, (int)q.x, (int)q.y, true);
                    squr.SetWalkable(false);
                    frameArray[i, j] = squr;
                }
                else if (frameArray[i, j] == null) 
                {
                    var p = new Vector2(i, j) * squareSize;
                    squr = Instantiate(squarePrefab, p, Quaternion.identity, padre.transform);
                    squr.Init(this, (int)p.x, (int)p.y, true);
                    frameArray[i, j] = squr;
                }




            }
        }

        var center = new Vector2((float)height / 2 - 0.5f, (float)width / 2 - 0.5f);

        Camera.main.transform.position = new Vector3(center.x, center.y, -5);
        Camera.main.orthographicSize = Mathf.Max(height, width) / 2 + 1;
    }



    internal int GetHeight()
    {
        return height;
    }

    internal int GetWidth()
    {
        return width;
    }


    public square GetFrameObject(int x, int y)
    {
        return frameArray[x, y];
    }

    internal float GetSquareSize()
    {
        return squareSize;
    }
}
