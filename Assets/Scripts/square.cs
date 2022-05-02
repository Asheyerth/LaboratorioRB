using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class square : MonoBehaviour
{
    [SerializeField] private GameObject spritesqre;
    [SerializeField] private GameObject Inner;
    private Frame frame;
    public bool canwalk;
    public int x, y;
    public int gcost, hcost, fcost;
    public square pastbox;



    public void Init(Frame frame, int x, int y, bool canwalk)
    {
        this.frame = frame;
        this.x = x;
        this.y = y;
        this.canwalk = canwalk;

    }

    public Vector2 Position => transform.position;



    public void SetInner()
    {
        Inner.GetComponent<SpriteRenderer>();
    }

    internal void CalculateFCost()
    {
        fcost = gcost + hcost;
    }

    internal void SetWalkable(bool v)
    {
        canwalk = v;
        SetInner();

    }

    public void setsprite()
    {
        spritesqre.GetComponent<Sprite>();

    }

    public int getX()
    {
        return this.x;
    }
    public int getY()
    {
        return this.y;
    }

}