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



    public Frame(int width, int height, int squareSize, square squarePrefab, square obstacleprefab)
    {
        this.width = width;
        this.height = height;
        this.squarePrefab = squarePrefab;
        this.obstacleprefab = obstacleprefab;
        this.squareSize = squareSize;

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
                if (i==0||i==width-1||j==0||j==height-1)
                {
                    var q = new Vector2(i, j) * squareSize;
                    squr = Instantiate(obstacleprefab, q, Quaternion.identity);
                    squr.Init(this, (int)q.x, (int)q.y, true);
                    squr.SetWalkable(false);
                    frameArray[i, j] = squr;
                }

                ran = Random.Range(0, 10);

                if (ran <= 2 && frameArray[i, j] == null && cont<30 && i!=1 && j!=1 && i!=height-1 && j!=width-1)
                {
                    cont++;
                    var q = new Vector2(i, j) * squareSize;
                    squr = Instantiate(obstacleprefab, q, Quaternion.identity);
                    squr.Init(this, (int)q.x, (int)q.y, true);
                    squr.SetWalkable(false);
                    frameArray[i, j] = squr;
                }
                else if(frameArray[i,j]==null) //&& !frameArray[height-1, width-1])
                {
                    var p = new Vector2(i, j) * squareSize;
                    squr = Instantiate(squarePrefab, p, Quaternion.identity);
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
