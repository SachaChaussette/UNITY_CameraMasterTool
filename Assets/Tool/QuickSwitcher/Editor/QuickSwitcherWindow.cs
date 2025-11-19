using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickSwitcherWindow : EditorWindow
{
    const string WINDOW_TITLE = "Quick Switcher",
                 MAIN_TITLE = "Camera Viewer";

    List<Camera> allCameras = new List<Camera>();
    List<Camera> camerasToDelete = new List<Camera>();
    int size = 0;

    Vector2 scrollPosition = Vector2.zero;

    bool IsValid => allCameras.Count > 0 && allCameras.Count == size;

    [MenuItem("Tools/Camera Master Tool")]
    public static void OpenWindow()
    {
        QuickSwitcherWindow _window = GetWindow<QuickSwitcherWindow>();

        _window.titleContent = new GUIContent(WINDOW_TITLE);
    }

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        allCameras = CameraFinder.GetAllCameras();
        size = allCameras.Count;
    }

    private void OnGUI()
    {
        DrawCamerasList();
    }

    void DrawCamerasList() 
    {
        EditorGUILayout.BeginVertical();

            EditorGUILayout.Separator();
            string _currentSceneName = SceneManager.GetActiveScene().name;
            EditorGUILayout.HelpBox(new GUIContent($"All Cameras In Scene '{_currentSceneName}' "));
            EditorGUILayout.Separator();


            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
                for (int i = 0; i < size; i++)
                {
                    Camera _camera = allCameras[i];

                    if (_camera == null || _camera.IsDestroyed())
                    {
                        camerasToDelete.Add(_camera);
                        continue;
                    }

                    GUILayout.BeginHorizontal();

                    EditorGUILayout.Separator();

                        string _displayName = _camera.name;
                        Vector3 _position = _camera.transform.position;

                        if (!_camera.gameObject.activeInHierarchy) GUI.enabled = false;
                        EditorGUILayout.ObjectField(_camera, typeof(Camera), false);
                        if (GUILayout.Button("Select"))
                        {
                            Selection.activeObject = _camera;
                        }
                        if (GUILayout.Button("Align View"))
                        {
                            SceneView _view = SceneView.lastActiveSceneView;
                            _view.AlignViewToObject(_camera.transform);
                        }
                        //_camera.transform.position = EditorGUILayout.Vector3Field("Position", _position);
                        GUI.enabled = true;
                        _camera.gameObject.SetActive(GUILayout.Toggle(_camera.gameObject.activeInHierarchy, "Is Active"));

                    GUILayout.EndHorizontal();

                    EditorGUILayout.Separator();

                }
            GUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        RemoveInvalidCameras();
    }

    void RemoveInvalidCameras()
    {
        for (int i = 0; i < camerasToDelete.Count; i++)
        {
            Camera _toDelete = camerasToDelete[i];
            for (int j = 0; j < size; j++)
            {
                if (allCameras[j] == _toDelete)
                {
                    allCameras.Remove(_toDelete);
                    camerasToDelete.Remove(_toDelete);
                    size--;
                }
            }
        }
    }

}
