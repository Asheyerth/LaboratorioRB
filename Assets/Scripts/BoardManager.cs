using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    public static BoardManager Instance;
    [SerializeField] private square squareprefab;
    [SerializeField] private square obstacleprefab;
    [SerializeField] private Enemy enemyprefab;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Goal goalprefab;
    private Frame frame;
    private Enemy enemy;
    private Player player;
    private Goal goal;
    [SerializeField]
    private float mspeed = 2f;

    [SerializeField] private static int level = 1; 
    private GameObject padre;


    private void Awake()
    {
        Instance = this;
    }



    private void Update()
    {
        if (level != 1)
        {
            if (player.juguemosEnElBosqueMientarsElLoboNoEsta())
            {
                level = level + 1;
            }
        }
    }

    private void Start()
    {
        level = PlayerPrefs.GetInt("levelP");
        run();
    }

    private void run()
    {
        if (level != 1)
        {
            Destroy(padre);
        }
        starting();
    }

    private void starting()
    {
        padre = new GameObject("Board");
        frame = new Frame(10, 10, 1, squareprefab, obstacleprefab, level, padre);
        int i = 0; //Level of the game
        while (i != level)
        {
            int x = Random.Range(3, frame.GetHeight() - 1);
            int y = Random.Range(3, frame.GetWidth() - 1);
            Debug.Log(x);
            Debug.Log(y);
            Debug.Log(frame.GetFrameObject(x, y).canwalk);
            if (frame.GetFrameObject(x, y).canwalk)
            {
                enemy = Instantiate(enemyprefab, new Vector2(x, y), Quaternion.identity, padre.transform);
                i++;
            }
        }


        player = Instantiate(playerPrefab, new Vector2(1, 1), Quaternion.identity, padre.transform);
        player.setFrame(frame);
        goal = Instantiate(goalprefab, new Vector2(frame.GetWidth() - 2, frame.GetHeight() - 2), Quaternion.identity, padre.transform);
    }


    public Vector2 nextMovement(int xi, int yi, int xn, int yn)
    {
        square next = PathManager.Instance.FindPath(frame, xi, yi, xn, yn)[1];
        return new Vector2(next.x, next.y);
    }


}
