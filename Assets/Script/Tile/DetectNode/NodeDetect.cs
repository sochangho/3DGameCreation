using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDetect
{

    public OponentPlayer player;
    public NodeDetect(OponentPlayer player)
    {
        this.player = player;
    }


    virtual public Node DetectTiles() { return null; }

    public List<Node> Find8Node(Node node)
    {

        List<Node> nodes = new List<Node>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int x = node.nodeX;
                int y = node.nodeY;

                x += i;
                y += j;

               
                if (x >= 0 && x < player.maxX && y>=0 && y < player.maxY && !(i == 0 && j == 0) 
                && !player.nodes[x , y].MonsterExist())
                {
                   
                    nodes.Add(node);
                }
            }
        }


      
        return nodes;
    }
}
