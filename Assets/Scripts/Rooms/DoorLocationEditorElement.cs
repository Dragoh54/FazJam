using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(Room))]
public class DoorLocationEditorElement : Editor
{
    public VisualTreeAsset VisualTreeAsset;

    public override VisualElement CreateInspectorGUI()
    {
        var room = (Room)target;

        var root = VisualTreeAsset.CloneTree();

        var property = serializedObject.FindProperty("doorSetup");

        root.TrackPropertyValue(property, p =>
        {
            Undo.RecordObject(room, "Changed Door Setup");

            room.ApplyDoorSetup();

            EditorUtility.SetDirty(room);
        });

        return root;
    }
}
