using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(NodeMapCreate))]
public class NodeMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        NodeMapCreate generator = (NodeMapCreate)target;
        if (GUILayout.Button("CreateMap"))
        {
            generator.MapCreate();
        }


        if (GUILayout.Button("Renderer"))
        {
            generator.NodeRender();
        }


    

    }
}
  
