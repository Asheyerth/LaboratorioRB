using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class PathManager : MonoBehaviour
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    public static PathManager Instance;
    private List<square> openList;
    private List<square> closedList;
    private Frame frame;

    private void Awake()
    {
        Instance = this;
    }

    public List<square> FindPath(Frame frame, int startx, int starty, int endx, int endy)
    {
        this.frame = frame;
        square startsquare = frame.GetFrameObject(startx, starty);
        square endCell = frame.GetFrameObject(endx, endy);

        openList = new List<square> { startsquare };
        closedList = new List<square>();

        for (int x = 0; x < frame.GetWidth(); x++)
        {
            for (int y = 0; y < frame.GetHeight(); y++)
            {

                square pathNode = frame.GetFrameObject(x, y);
                if (pathNode.canwalk)
                    pathNode.SetInner();
                pathNode.gcost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.pastbox = null;
            }
        }

        startsquare.gcost = 0;
        startsquare.hcost = CalculateDistanceCost(startsquare, endCell);
        startsquare.CalculateFCost();

        while (openList.Count > 0)
        {
            square currentNode = GetLowestFCostNode(openList);
            if (currentNode == endCell)
            {
                return CalculatePath(endCell);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (square neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.canwalk)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gcost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gcost)
                {
                    neighbourNode.pastbox = currentNode;
                    neighbourNode.gcost = tentativeGCost;
                    neighbourNode.hcost = CalculateDistanceCost(neighbourNode, endCell);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        Debug.Log("Did not reach the end");
        return null;
    }

    private List<square> CalculatePath(square endCell)
    {
        List<square> path = new List<square>();
        path.Add(endCell);
        square currentNode = endCell;
        while (currentNode.pastbox != null)
        {
            path.Add(currentNode.pastbox);
            currentNode = currentNode.pastbox;
        }
        path.Reverse();

        return path;
    }

    private List<square> GetNeighbourList(square currentNode)
    {
        List<square> neighbourList = new List<square>();

        if (currentNode.x - 1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
        }
        if (currentNode.x + 1 < frame.GetWidth())
        {
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
        }
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        if (currentNode.y + 1 < frame.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    public square GetNode(int x, int y)
    {
        return frame.GetFrameObject(x, y);
    }

    private int CalculateDistanceCost(square a, square b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private square GetLowestFCostNode(List<square> pathNodeList)
    {
        square lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fcost < lowestFCostNode.fcost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

}