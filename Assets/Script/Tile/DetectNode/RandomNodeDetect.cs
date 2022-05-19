using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNodeDetect : NodeDetect
{

    public RandomNodeDetect(OponentPlayer player) : base(player)
    {

    }


    public override Node DetectTiles()
    {

      
        List<Node> detectNode = new List<Node>();


        for (int i = 0; i < player.maxY; i++)
        {
             for(int j = 0; j < player.maxX; j++)
            {

                 if(player.nodes[j, i].nodetype == NodeType.Oponent)
                {
                    detectNode.Add(player.nodes[j, i]);
                    
                }

            }
            
        }


        int Randomindex = Random.Range(0 , detectNode.Count -1);

        

        return detectNode[Randomindex];

    }



}
