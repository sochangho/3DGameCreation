using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NodeMapCreate : MonoBehaviour
{

    public Node nodePrefab;
    public int mapSizeX;
    public int mapSizeY;
    private bool is_render;

    public List<Node> nodes;

    public void Awake()
    {

        foreach (Node node in nodes)
        {

            node.GetComponent<MeshRenderer>().enabled = false;


        }



    }

    public void MapCreate()
    {

        is_render = true;

        for(int i = 0; i < nodes.Count; i++)
        {

            DestroyImmediate(nodes[i].gameObject);

        }



        nodes = new List<Node>();

        for(int y = 0; y < mapSizeY; y++)
        {
          
            for (int x = 0; x < mapSizeX; x++)
            {

                Node node = Instantiate(nodePrefab);
                MeshRenderer nodeMr = node.GetComponent<MeshRenderer>();
                Vector3 nodeSize = nodeMr.bounds.size;

                node.transform.parent = this.transform;
                node.transform.localPosition = new Vector3(x * nodeSize.x , 0, -y * nodeSize.z );
               
                nodes.Add(node);

            }
        }

    }


    public void NodeRender()
    {

        if (is_render)
        {

            is_render = false;


            foreach(Node node in nodes)
            {

                node.GetComponent<MeshRenderer>().enabled = false;


            }

        }
        else
        {
            is_render = true;


            foreach (Node node in nodes)
            {

                node.GetComponent<MeshRenderer>().enabled = true;


            }

        }




    }



}
