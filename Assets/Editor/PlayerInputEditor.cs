using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(PlayerInput))]
public class PlayerInputEditor : Editor
{
    private ReorderableList list;

    private int listeningIndex = -1;

    private void OnEnable()
    {
        SerializedProperty actions = serializedObject.FindProperty("actions");

        list = new ReorderableList(serializedObject, actions, true, true, true, true);

        list.drawHeaderCallback = rect =>
        {
            EditorGUI.LabelField(rect, "Input Actions");
        };

        list.elementHeight = EditorGUIUtility.singleLineHeight * 2 + 12;

        list.drawElementCallback = DrawElement;
    }

    private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

        SerializedProperty action = element.FindPropertyRelative("action");
        SerializedProperty key = element.FindPropertyRelative("key");

        rect.y += 2;

        Rect actionRect = new Rect(
            rect.x,
            rect.y,
            rect.width,
            EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(actionRect, action, GUIContent.none);

        Rect labelRect = new Rect(
            rect.x,
            rect.y + EditorGUIUtility.singleLineHeight + 6,
            30,
            EditorGUIUtility.singleLineHeight);

        EditorGUI.LabelField(labelRect, "Key");

        Rect keyRect = new Rect(
            rect.x + 35,
            rect.y + EditorGUIUtility.singleLineHeight + 6,
            100,
            EditorGUIUtility.singleLineHeight);

        GUI.enabled = false;
        EditorGUI.EnumPopup(keyRect, (KeyCode)key.intValue);
        GUI.enabled = true;

        Rect buttonRect = new Rect(
            rect.x + 145,
            rect.y + EditorGUIUtility.singleLineHeight + 6,
            rect.width - 145,
            EditorGUIUtility.singleLineHeight);

        if (listeningIndex == index)
        {
            GUI.backgroundColor = Color.yellow;

            GUI.Button(buttonRect, "Pressione uma tecla...");

            GUI.backgroundColor = Color.white;

            Event e = Event.current;

            if (e.type == EventType.KeyDown)
            {
                key.intValue = (int)e.keyCode;

                listeningIndex = -1;

                serializedObject.ApplyModifiedProperties();

                GUI.changed = true;

                e.Use();
            }
        }
        else
        {
            if (GUI.Button(buttonRect, "Alterar"))
            {
                listeningIndex = index;
            }
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        list.DoLayoutList();

        serializedObject.ApplyModifiedProperties();

        if (listeningIndex != -1)
            Repaint();
    }
}