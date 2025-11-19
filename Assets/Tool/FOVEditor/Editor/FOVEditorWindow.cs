using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FOVEditorWindow : EditorWindow
{
    Camera selectedCamera = null;

    public const string WINDOW_TITLE = "FOV Editor";

    [MenuItem("Tools/Fov Editor")]
    public static void OpenWindow()
    {
        FOVEditorWindow _window = GetWindow<FOVEditorWindow>();

        _window.titleContent = new GUIContent(WINDOW_TITLE);
    }
        
    private void Awake()
    {
        Init();
    }

    void Init()
    {
        selectedCamera = null;

        RefreshCurrentCamera();

        Selection.selectionChanged += RefreshCurrentCamera;
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();

            EditorGUILayout.Separator();
            string _currentSceneName = SceneManager.GetActiveScene().name;
            EditorGUILayout.HelpBox(new GUIContent($"Selected Camera"));
            EditorGUILayout.Separator();
            EditorGUILayout.ObjectField(selectedCamera, typeof(Camera), false);
            if (selectedCamera)
            {
                EditorGUILayout.Separator();
                selectedCamera.fieldOfView = EditorGUILayout.Slider(new GUIContent("Field Of View"), selectedCamera.fieldOfView, 0.0f, 180.0f);
                EditorGUILayout.Separator();
                EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(new GUIContent("Clipping Planes"), GUILayout.MaxWidth(100.0f));
                    EditorGUILayout.Space(45.0f);
                    EditorGUILayout.BeginVertical();
                        selectedCamera.nearClipPlane = EditorGUILayout.FloatField("Near", selectedCamera.nearClipPlane);
                        selectedCamera.farClipPlane = EditorGUILayout.FloatField("Far", selectedCamera.farClipPlane);
                    EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Separator();
                selectedCamera.usePhysicalProperties = EditorGUILayout.Toggle("Physical Camera", selectedCamera.usePhysicalProperties);
                EditorGUILayout.Separator();
            }

        EditorGUILayout.EndVertical();
    }

    void RefreshCurrentCamera()
    {
        GameObject _activeGameObject = Selection.activeGameObject;
        selectedCamera = _activeGameObject ? _activeGameObject.GetComponent<Camera>() : null;
        Repaint();
    }
}
