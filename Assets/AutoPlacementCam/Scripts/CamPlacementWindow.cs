using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

public class CamPlacementWindow : EditorWindow
{
    public const string WINDOW_TITLE = "Auto Camera Placement";
    string CAMERA_TO_PLACE_TITLE = "Enter number of cam to spawn";
    string CAMERA_OFFSET_TITLE = "Enter offset camera to spawn";
    string TARGET_TO_CHOOSE = "Choose a target to spawn camera arround";
    int numberCameraToPlace = 0;
    Vector3 offSetCam = Vector3.zero;
    GameObject target = null;

    [MenuItem("Cams/Auto Camera Placement")]
    public static void OpenWindow()
    {
        CamPlacementWindow _window = GetWindow<CamPlacementWindow>();
        _window.titleContent = new GUIContent(WINDOW_TITLE);
    }

    private void OnGUI()
    {
        DrawContent();
    }

    void DrawContent()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 250f;
        numberCameraToPlace = EditorGUILayout.IntField(CAMERA_TO_PLACE_TITLE, numberCameraToPlace);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 250f;
        EditorGUIUtility.wideMode = true;
        offSetCam = EditorGUILayout.Vector3Field(CAMERA_OFFSET_TITLE, offSetCam);
        EditorGUIUtility.wideMode = false;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 250f;
        target = (GameObject)EditorGUILayout.ObjectField(TARGET_TO_CHOOSE, target, typeof(Camera), true);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Place camera arround target"))
        {
            CamPlacementCompo.GenerateCamArroundTarget(numberCameraToPlace, offSetCam, target);
        }

    }
}
