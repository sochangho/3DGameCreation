using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
   
    public NodeType nodetype;
    public MeshRenderer renderer;

    public Material[] materials;

    private bool is_select = false;


    public float boundSizeX { get; set; }
    public float boundSizeY { get; set; }

    public int nodeX;

    public int nodeY;




    public bool is_ExistMonster = false;

    public void Start()
    {
        if (renderer == null)
        {
            renderer = GetComponent<MeshRenderer>();

        }


        Vector3 boundSize = renderer.bounds.size;

        boundSizeX = boundSize.x;
        boundSizeY = boundSize.y;
    }


    public bool MonsterExist()
    {
  
        RaycastHit hit;


        if(Physics.Raycast(transform.position, Vector3.up, out hit))
        {

            if (hit.collider.GetComponent<Charater>() != null)
            {
                
                return true;
            }



        }




        return false;

    }


    public void OnMouseEnter()
    {
      
      
        if(GameSceneManager.Instance.spwanObjet != null &&  nodetype == NodeType.Player && !MonsterExist())
        {
            is_select = true;
            renderer.enabled = true;

        }

    }

    private void OnMouseDown()
    {
        if (is_select)
        {

            is_select = false;
            renderer.enabled = false;
            GameSceneManager.Instance.MonsterSpwan(this.transform);
            

        }


    }


    private void OnMouseExit()
    {
        if (is_select)
        {
            is_select = false;
            renderer.enabled = false;

        }


    }





    public void NodeTypeChage(int value)
    {
       
        renderer = GetComponent<MeshRenderer>();

        if (value == 0)
        {
            nodetype = NodeType.Player;
            
        }
        else if (value == 1)
        {
            nodetype = NodeType.Oponent;
            
        }
        else if (value == 2)
        {
            nodetype = NodeType.None;
          
        }

        renderer.material = materials[(int)nodetype];
    }



}

public enum NodeType
{
    Player,
    Oponent,
    None,

    Size
}
