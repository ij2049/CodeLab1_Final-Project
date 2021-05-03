using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseEditor : Editor
{
    private ItemDatabase source;
    private SerializedProperty s_items, s_itemsName;

    void OnEnable()
    {
        source = (ItemDatabase) target;
        s_items = serializedObject.FindProperty("items");
        s_itemsName = serializedObject.FindProperty("itemsNames");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //base.OnInspectorGUI();

        if (GUILayout.Button("Add Item"))
        {
            Item newItem = new Item(s_items.arraySize, "", "");
            source.AddItem(newItem);
        }
        
        for (int i = 0; i < s_items.arraySize; i++)
        {
            //draw the item entry
            DrawItemEntry(s_items.GetArrayElementAtIndex(i));
        }

        //we need to add this serialize property to use it
        serializedObject.ApplyModifiedProperties();
    }

    void DrawItemEntry(SerializedProperty item)
    {
        GUILayout.BeginVertical("box");
        
        GUILayout.BeginHorizontal();
        
        EditorGUILayout.LabelField
            ("Item Id: " + item.FindPropertyRelative("itemId").intValue, 
            GUILayout.Width(75f));
        EditorGUILayout.PropertyField((item.FindPropertyRelative("itemName")));

        //delete the item
        if (GUILayout.Button("X", GUILayout.Width(20f)))
        {
            //delete the item
            s_itemsName.DeleteArrayElementAtIndex(item.FindPropertyRelative("itemId").intValue);
            s_items.DeleteArrayElementAtIndex(item.FindPropertyRelative("itemId").intValue);
            
            ReCalculateID();
            return;
        }

        GUILayout.EndHorizontal();
        
        EditorGUILayout.PropertyField((item.FindPropertyRelative("itemDescription")));
        
        GUILayout.BeginHorizontal();
        item.FindPropertyRelative("itemSprite").objectReferenceValue
            = EditorGUILayout.ObjectField("Item Sprite : ", 
                item.FindPropertyRelative("itemSprite").objectReferenceValue, 
                typeof(Sprite), false);
        
        EditorGUILayout.PropertyField((item.FindPropertyRelative("allowMultiple")));
        GUILayout.EndHorizontal();
        
        GUILayout.EndVertical();
    }

    void ReCalculateID()
    {
        for (int i = 0; i < s_items.arraySize; i++)
        {
            s_items.GetArrayElementAtIndex(i).FindPropertyRelative("itemId").intValue = i;
            s_itemsName.GetArrayElementAtIndex(i).stringValue =
                s_items.GetArrayElementAtIndex(i).FindPropertyRelative("itemName").stringValue;
        }
    }
}
