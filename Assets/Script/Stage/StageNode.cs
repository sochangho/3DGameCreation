using System.Collections.Generic;
using UnityEngine;
public class StageNode<T>
{
    public T data { get; set; }
    public int level;
    public int index;
    public Vector2 nodePos;

    public Clear clear { get; set; }

    public List<StageNode<T>> parent { get; set; } = new List<StageNode<T>>();
    public List<StageNode<T>> children { get; set; } = new List<StageNode<T>>();
        
}

public enum Clear
{
    No, Yes

}