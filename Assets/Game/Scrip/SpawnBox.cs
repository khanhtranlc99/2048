using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SpawnBox : MonoBehaviour
{
    public Vector3 initPoint;
    public float amount;
    [SerializeField] private BoxBG box;
    [SerializeField] private BoxPlay boxPlay;
    [SerializeField] private GameObject parentBG;
    [SerializeField] private GameObject parentBoxPlay;
    [SerializeField] private List<BoxPlay> listBox;
    [SerializeField] private List<BoxBG> listBG;
    private int[,] matrix;
    int cow;
    int row;
    int coutLose;

    // Start is called before the first frame update
    void Start()
    {
        _SpawnBG();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _RandomAndSpawn();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _Test();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _CheckMatrix();
        }
    }
    public void _SpawnBG()
    {
        if (matrix == null)
        {
            matrix = new int[4, 4];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    var a = SimplePool.Spawn(box, new Vector3(initPoint.x + i * amount, initPoint.y + j * amount, 0), Quaternion.identity).GetComponent<BoxBG>();
                    //a.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                    a.transform.SetParent(parentBG.transform);
                    a.post.x = i;
                    a.post.y = j;
                    listBG.Add(a);
                    matrix[i, j] = 0;

                }
            }
        }
    }
    public void _RandomAndSpawn()
    {
        coutLose = 0;
        foreach (int item in matrix)
        {
            if (item == 0)
            {

                coutLose += 1;

                //Debug.Log("conteniu = " + coutLose);
            }


        }
        if (coutLose == 0)
        {
            Debug.Log("full");  ////end Game
            return;
        }
        do
        {
            cow = Random.Range(0, matrix.GetLength(0));
            row = Random.Range(0, matrix.GetLength(1));
        }
        while (matrix[cow, row] == 2);
        matrix[cow, row] = 2;

        _SpawnBoxPlay(cow, row);


    }


    private void _SpawnBoxPlay(int a, int b)
    {
        Debug.Log("post = " +  a + b);
        if (matrix[a, b] == 2)
        {
            var c = SimplePool.Spawn(boxPlay.gameObject, new Vector3(initPoint.x + a * amount, initPoint.y + b * amount, 0), Quaternion.identity).GetComponent<BoxPlay>();
            c.transform.SetParent(parentBoxPlay.transform);
            //c.transform.localScale = new Vector3(0.4f, 0.4f, 1);
            c.idBox = 2;
            //c.scoreText.text = "" + c.idBox;
            c.postX = a;
            c.postY = b;
            matrix[c.postX, c.postY] = 2;
            listBox.Add(c);

        }
    }
    public void _MoveLeft()
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                
                for (int a = 0; a < listBox.Count; a++)
                {     if (i - 1 > 0 )
                    {
                  
                        if (listBox[a].idBox == matrix[i - 1, j] || matrix[i - 1, j] == 0)
                        {
                            matrix[i , j] = 0;
                            matrix[i - 1, j] = 2;
                            listBox[a].transform.DOMoveX(matrix[i -1, j] , 1);
                            Debug.Log("post(i -1) = " + (i - 1));
                      
                        }
                    }
                 
                    
                         
              }                  
            }
             
        }
       
    }
    private void _Test()
    {

        for (int i = 0; i < listBox.Count; i++)
        {

            if (listBox[i].postX - 1 >= 0)
            {
               
                    for (int a = 0; a < listBG.Count; a++)
                    {
                        if (listBG[a].post.x == listBox[i].postX - 1)
                        {
                        if (matrix[listBox[i].postX - 1, listBox[i].postY] == 0 )
                        {
                            listBox[i].transform.DOMoveX(listBG[a].gameObject.transform.position.x, 0.5f);
                            matrix[listBox[i].postX, listBox[i].postY] = 0;
                            matrix[listBox[i].postX - 1, listBox[i].postY] = 2;
                            listBox[i].postX -= 1;
                        }
                        if (matrix[listBox[i].postX - 1, listBox[i].postY] == listBox[i].idBox)
                        {
                            listBox[i].transform.DOMoveX(listBG[a].gameObject.transform.position.x, 0.5f);
                            matrix[listBox[i].postX, listBox[i].postY] = 0;
                            matrix[listBox[i].postX - 1, listBox[i].postY] += listBox[i].idBox;
                            listBox[i].postX -= 1;


                        }
                    }
                    }
                    
             
            }
             
        }


    }
    private void _CheckMatrix()
    {
        Debug.Log("" + matrix[3, 0] + matrix[3, 1] + matrix[3, 2] + matrix[3, 3]);
        Debug.Log("" + matrix[2, 0] + matrix[2, 1] + matrix[2, 2] + matrix[2, 3]);
        Debug.Log("" + matrix[1, 0] + matrix[1, 1] + matrix[1, 2] + matrix[1, 3]);
        Debug.Log("" + matrix[0, 0] + matrix[0, 1] + matrix[0, 2] + matrix[0, 3]);
 
   







    }    


    
}
