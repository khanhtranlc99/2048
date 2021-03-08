using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxPlay : MonoBehaviour
{
    public SpriteRenderer spriteRender;
    //public Sprite[] sprite;
    public int idBox;
    public int postX;
    public int postY;
    private void OnMouseDown()
    {
        Debug.Log("idBox= " + idBox + " postX= " + postX + " postY= " + postY);
  
     
    }
}
