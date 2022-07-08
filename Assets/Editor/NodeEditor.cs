using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    int index = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Node generator = (Node)target;


        string[] strType = { "Player", "Oponent", "None" };

        

        index = EditorGUILayout.Popup((int)generator.nodetype, strType, GUILayout.Width(100f));

        
        generator.NodeTypeChage(index);
       


    }
}
