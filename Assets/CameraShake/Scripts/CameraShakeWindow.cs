using System.Globalization;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraShakeWindow : EditorWindow
{
    public const string WINDOW_TITLE = "Manager Shake";
    const string TARGET_CAMERA_TO_CHOOSE = "Choose camera";
    const string CHOOSE_PRESET = "Choose preset (Optional)";
    const string DURATION_SHAKE = "Choose duration for shake";
    const string MAGNITUDE_SHAKE = "Choose magnitude for shake";
    const string PRESET_NAME = "Choose preset name";
    float duration = 1f;
    float magnitude = 1f;
    Camera camera = null;
    CameraShakePreset choosenPreset = null;
    string presetName = "DefaultNamePreset";

    [MenuItem("Tools/Camera Shake", false, 14)]
    public static void OpenWindow()
    {
        CameraShakeWindow _window = GetWindow<CameraShakeWindow>();
        _window.titleContent = new GUIContent(WINDOW_TITLE);
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 250f;
        camera = (Camera)EditorGUILayout.ObjectField(TARGET_CAMERA_TO_CHOOSE, camera, typeof(Camera), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 250f;
        choosenPreset = (CameraShakePreset)EditorGUILayout.ObjectField(CHOOSE_PRESET, choosenPreset, typeof(CameraShakePreset), true);
        EditorGUILayout.EndHorizontal();

        if(!choosenPreset)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Create preset");
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 250f;
            duration = EditorGUILayout.FloatField(DURATION_SHAKE, duration);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 250f;
            magnitude = EditorGUILayout.FloatField(MAGNITUDE_SHAKE, magnitude);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 250f;
            presetName = EditorGUILayout.TextField(PRESET_NAME, presetName);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Test shake"))
        {
            if (camera == null) return;
            if(choosenPreset)
            {
                duration = choosenPreset.duration;
                magnitude = choosenPreset.magnitude;
            }
            CameraShake.TestShake(camera, duration, magnitude);
        }

        if (GUILayout.Button("Save shake parameters"))
        {
            if(choosenPreset !=  null)
            {
                Debug.Log("Preset already exist");
                return;
            }
            CameraShake.Save(duration, magnitude, presetName);
        }

    }
}
