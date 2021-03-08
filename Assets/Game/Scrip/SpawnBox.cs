using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SpawnBox : MonoBehaviour
{
    public Vector3 initPoint;
    [SerializeField] private float amount;
    [SerializeField] private SpriteRenderer spriteRender;
    public Sprite[] sprite;
    [SerializeField] private BoxBG box;
    [SerializeField] private BoxPlay boxPlay;
    [SerializeField] private GameObject parentBG;
    [SerializeField] private GameObject parentBoxPlay;
    [SerializeField] private List<BoxPlay> listBox;
    [SerializeField] private List<BoxBG> listBG;
    [SerializeField] private List<BoxPlay> testList;
    private int[,] matrix;
    int cow;
    int row;
    int coutLose;
    private Coroutine destroyCorutin;
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
      
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _CheckMatrix();
        }
        if (Input.GetMouseButton(0))
        {
            _CheckSprite();



            //_Test();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            _CheckTest();
      
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
        while (matrix[cow, row] >= 2);
        matrix[cow, row] = 2;

        //_SpawnTest();
        _SpawnBoxPlay(cow, row);
 
    }


    private void _SpawnBoxPlay(int a, int b)
    {
        Debug.Log("post = " + a + b);
        if (matrix[a, b] == 2)
        {
            var c = SimplePool.Spawn(boxPlay, new Vector3(initPoint.x + a * amount, initPoint.y + b * amount, 0), Quaternion.identity).GetComponent<BoxPlay>();
            c.transform.SetParent(parentBoxPlay.transform);
            //c.transform.localScale = new Vector3(0.4f, 0.4f, 1);
            c.idBox = 2;
            //c.scoreText.text = "" + c.idBox;
            c.postX = a;
            c.postY = b;
            matrix[c.postX, c.postY] = 2;
            listBox.Add(c);
            _CheckSprite();
        }
    }
    

    private void _CheckSprite()
    {


        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int a = 0; a < listBox.Count; a++)
                {


                    if (matrix[i, j] == 2 && listBox[a].postX == i && listBox[a].postY == j)
                    {
                        listBox[a].spriteRender.sprite = sprite[0];
                    }
                    if (matrix[i, j] == 4 && listBox[a].postX == i && listBox[a].postY == j)
                    {
                        listBox[a].spriteRender.sprite = sprite[1];
                    }

                    if (matrix[i, j] == 8 && listBox[a].postX == i && listBox[a].postY == j)
                    {
                        listBox[a].spriteRender.sprite = sprite[2];
                    }
                    if (matrix[i, j] == 16 && listBox[a].postX == i && listBox[a].postY == j)
                    {
                        listBox[a].spriteRender.sprite = sprite[3];
                    }
                    if (matrix[i, j] == 32 && listBox[a].postX == i && listBox[a].postY == j)
                    {
                        listBox[a].spriteRender.sprite = sprite[4];
                    }
                    if (matrix[i, j] == 64 && listBox[a].postX == i && listBox[a].postY == j)
                    {
                        listBox[a].spriteRender.sprite = sprite[5];
                    }
                    if (matrix[i, j] == 128 && listBox[a].postX == i && listBox[a].postY == j)
                    {
                        listBox[a].spriteRender.sprite = sprite[6];
                    }
                    if (matrix[i, j] == 256 && listBox[a].postX == i && listBox[a].postY == j)
                    {
                        listBox[a].spriteRender.sprite = sprite[7];
                    }
                }
            }

        }





    }
    private void _CheckMatrix()
    {
        Debug.Log("" + matrix[0, 3] + matrix[1, 3] + matrix[2, 3] + matrix[3, 3]);
        Debug.Log("" + matrix[0, 2] + matrix[1, 2] + matrix[2, 2] + matrix[3, 2]);
        Debug.Log("" + matrix[0, 1] + matrix[1, 1] + matrix[2, 1] + matrix[3, 1]);
        Debug.Log("" + matrix[0, 0] + matrix[1, 0] + matrix[2, 0] + matrix[3, 0]);
       
    }
    private void _CheckTest()
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                //if (i - 4 >= 0)
                //{
                //    if (matrix[i, j] != 0 && matrix[i - 4, j] == 0)
                //    {
                //        var d = matrix[i, j];
                //        matrix[i - 4, j] = d;
                //        matrix[i, j] = 0;
                //        _Move(i, j);

                //    }
                //    if (matrix[i, j] != 0 && matrix[i, j] == matrix[i - 4, j])
                //    {

                //        var d = matrix[i, j];
                //        matrix[i - 4, j] += d;
                //        matrix[i, j] = 0;
                //        _MoveDelete(i, j);


                //    }
                //}
                //if (i - 3 >= 0)
                //{
                //    if (matrix[i, j] != 0 && matrix[i - 3, j] == 0)
                //    {
                //        var d = matrix[i, j];
                //        matrix[i - 3, j] = d;
                //        matrix[i, j] = 0;
                //        _Move(i, j);

                //    }
                //    if (matrix[i, j] != 0 && matrix[i, j] == matrix[i - 3, j])
                //    {

                //        var d = matrix[i, j];
                //        matrix[i - 3, j] += d;
                //        matrix[i, j] = 0;
                //        _MoveDelete(i, j);


                //    }
                //}
                //if (i - 2 >= 0)
                //{
                //    if (matrix[i, j] != 0 && matrix[i - 2, j] == 0)
                //    {
                //        var d = matrix[i, j];
                //        matrix[i - 2, j] = d;
                //        matrix[i, j] = 0;
                //        _Move(i, j);

                //    }
                //    if (matrix[i, j] != 0 && matrix[i, j] == matrix[i - 2, j])
                //    {

                //        var d = matrix[i, j];
                //        matrix[i - 2, j] += d;
                //        matrix[i, j] = 0;
                //        _MoveDelete(i, j);


                //    }
                //}
                if (i - 1 >= 0)
                {
                    if (matrix[i, j] != 0 && matrix[i - 1, j] == 0)
                    {
                        var d = matrix[i, j];
                        matrix[i - 1, j] = d;
                        matrix[i, j] = 0;
                        _Move(i, j);

                    }
                    if (matrix[i, j] != 0 && matrix[i, j] == matrix[i - 1, j])
                    {

                        var d = matrix[i, j];
                        matrix[i - 1, j] += d;
                        matrix[i, j] = 0;
                        _MoveDelete(i, j);


                    }
                }
                
               
            }
        }

    }
    private void _Move(int a, int b)
    {
  
        for (int i = 0; i < listBox.Count; i++)
        {
            for (int p = 0; p < listBG.Count; p++)
            {
                if (listBox[i].postX == a && listBox[i].postY == b)
                {
                    if (listBG[p].post.x == listBox[i].postX - 1)
                    {
                        listBox[i].postX -= 1;
                        listBox[i].transform.DOMoveX(listBG[p].transform.position.x, 0.5f).OnComplete(() => {/*SimplePool.Despawn(listBox[i].gameObject);*/ });

                    }
                
                }
            }
        }
        Debug.Log("trường hợp đằng sau không có gì");

    }
    private void _MoveDelete(int a, int b)
    {

        for (int i = 0; i < listBox.Count; i++)
        {
            for (int p = 0; p < listBG.Count; p++)
            {
                if (listBox[i].postX == a && listBox[i].postY == b)
                {
                    if (listBG[p].post.x == listBox[i].postX - 1)
                    {
                        listBox[i].postX -= 1;
                        listBox[i].transform.DOMoveX(listBG[p].transform.position.x, 0.5f).OnComplete(() => { });
                     if ( destroyCorutin != null)
                        {
                            StopCoroutine(destroyCorutin);
                            destroyCorutin = null;
                        }
                        destroyCorutin = StartCoroutine(_Destroy(listBox[i].gameObject));
                    }

                }
            }
        }
        Debug.Log("haha2");

    }
    private void _CheckDelete()
    {
       
        foreach (BoxPlay item in listBox)
        {
            var T = listBox.Count;
        }       
    
    }

    private IEnumerator _Destroy(GameObject a)
    {
        yield return new WaitForSeconds(0.5f);
        {
            SimplePool.Despawn(a);
            _CheckSprite();
        }
    }
    private void _SpawnTest()
    {
        for ( int i = 0; i < matrix.GetLength(0); i ++)
        {
             for (int j = 0; j < matrix.GetLength(1); j++)
            {
               for( int a = 0; a < testList.Count; a ++)
                {
                        if( matrix[i,j] > 0)
                    {
                        Debug.Log("hehe");
                        if (matrix[i, j] == testList[i].idBox)
                        {
                           var c =  SimplePool.Spawn(testList[i], new Vector3(initPoint.x + i * amount, initPoint.y + j * amount, 0), Quaternion.identity);
                            listBox.Add(c);
                            Debug.Log("hehe");
                      }               
                    }
                  
                }


            }
        }

         
    }
    private void _SpawnBoxPlayhaha(int a, int b)
    {
        Debug.Log("post = " + a + b);
        if (matrix[a, b] > 0)
        {
            var c = SimplePool.Spawn(boxPlay, new Vector3(initPoint.x + a * amount, initPoint.y + b * amount, 0), Quaternion.identity).GetComponent<BoxPlay>();
            c.transform.SetParent(parentBoxPlay.transform);
            c.postX = a;
            c.postY = b;

            matrix[c.postX, c.postY] = 2;
            listBox.Add(c);
            _CheckSprite();
        }
    }
}
