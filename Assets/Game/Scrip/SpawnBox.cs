using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBox : MonoBehaviour
{
    public Vector3 initPoint;
    public float amount;
    [SerializeField] private Image box;
    [SerializeField] private BoxPlay boxPlay;
    [SerializeField] private GameObject parentBG;
    [SerializeField] private GameObject parentBoxPlay;
    [SerializeField] private List<GameObject> listBox;
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
                    var a = SimplePool.Spawn(box, new Vector3(initPoint.x + i * amount, initPoint.y + j * amount, 0), Quaternion.identity);
                    a.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                    a.transform.SetParent(parentBG.transform);
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

                Debug.Log("conteniu = " + coutLose);
            }


        }
        if (coutLose == 0)
        {
            Debug.Log("full");
            return;
        }
        do
        {
            cow = Random.Range(0, matrix.GetLength(0));
            row = Random.Range(0, matrix.GetLength(1));
        }
        while (matrix[cow, row] == 1);
        matrix[cow, row] = 1;

        _SpawnBoxPlay(cow, row);


    }


    private void _SpawnBoxPlay(int a, int b)
    {

        if (matrix[a, b] == 1)
        {
            var c = SimplePool.Spawn(boxPlay.gameObject, new Vector3(initPoint.x + a * amount, initPoint.y + b * amount, 0), Quaternion.identity).GetComponent<BoxPlay>();
            c.transform.SetParent(parentBoxPlay.transform);
            c.transform.localScale = new Vector3(0.4f, 0.4f, 1);
            c.scoreText.text = "2";
            listBox.Add(c.gameObject);

        }
    }
    public void _CheckMatrix()
    {
        foreach (int item in matrix)
        {
            if (item == 0)
            {
                Debug.Log("full");


            }
            else
            {
                Debug.Log("conteniu");
            }
        }

    }
}
