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
    private Frame frame;
    private Enemy enemy;
    private Player player;
    [SerializeField]
    private float mspeed = 2f;

    private square[,] frameArray; //DELETE

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        frameArray = new square[1, 1];//DELETE 
        frame = new Frame(10, 10, 1, squareprefab, obstacleprefab);
        int i = 0; //Level of the game
        while (i != 2)
        {
            int x = Random.Range(3, frame.GetHeight() - 1);
            int y = Random.Range(3, frame.GetWidth() - 1);
            Debug.Log(x);
            Debug.Log(y);
            Debug.Log(frame.GetFrameObject(x, y).canwalk);
            if (frame.GetFrameObject(x, y).canwalk)
            {
                enemy = Instantiate(enemyprefab, new Vector2(x, y), Quaternion.identity);
                i++;
            }
        }


        player = Instantiate(playerPrefab, new Vector2(1, 1), Quaternion.identity);
        
    }
    //Etto... Senpai...
    public Vector2 nextMovement(int xi, int yi, int xn, int yn)
    {
        square next = PathManager.Instance.FindPath(frame, xi, yi, xn, yn)[1];
        return new Vector2(next.x, next.y);
    }




    //public void CellMouseClick(int x, int y)
    //{
    //    List<Cell> path = PathManager.Instance.FindPath(grid, (int)player.GetPosition.x, (int)player.GetPosition.y, x, y);

    //    player.SetPath(path);
    //}




    //private BoxCollider2D playerb;
    //private bool cblock = false;
    //public float speed = 10f;

    //public Animator playeranim;
    //private Vector3 moved;


    //public int tam;
    //public int obst;
    //public Boxes box;
    //public Player player;
    //public static GameManager Instance;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    playerb = this.GetComponent<BoxCollider2D>();
    //    generateFrames();

    //}

    //private void FixedUpdate()
    //{
    //    float x = Input.GetAxisRaw("Horizontal");
    //    float y = Input.GetAxisRaw("Vertical");

    //    moved = new Vector3(x, y, 0);

    //    if (moved.x>0)
    //    {
    //        transform.localScale = Vector3.one;
    //    }else if(moved.x<0){
    //        transform.localScale = new Vector3(-1, 1, 1);
    //    }

    //    transform.Translate(moved * Time.deltaTime*speed);
    //}


    //private void generateFrames()
    //{

    //    for (int i = 0; i < tam; i++)
    //    {
    //        for (int j = 0; j < tam; j++)
    //        {
    //            if (i==0 || i==tam-1)
    //            {
    //                var p = new Vector2(i, j);
    //                Instantiate(box, p, Quaternion.identity);
    //            }

    //            if (j==0 || j==tam-1)
    //            {
    //                var p = new Vector2(i, j);
    //                Instantiate(box, p, Quaternion.identity);
    //            }

    //        }
    //    }

    //    var q = new Vector2(1, tam - 2);
    //    Instantiate(player, q, Quaternion.identity);


    //    var center = new Vector2((float)tam / 2 - 0.5f, (float)tam / 2 - 0.5f);
    //    Camera.main.transform.position = new Vector3(center.x, center.y, -5);
    //}


}
