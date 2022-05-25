using System.Collections.Generic;
using UnityEngine;
public class StageNode<T>
{
    public T data { get; set; }
    public List<StageNode<T>> parent { get; set; } = new List<StageNode<T>>();
    public List<StageNode<T>> children { get; set; } = new List<StageNode<T>>();
    public Vector3 nodePos;
}
