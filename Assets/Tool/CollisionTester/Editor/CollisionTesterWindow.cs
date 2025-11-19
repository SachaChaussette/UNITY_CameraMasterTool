using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionTesterWindow : EditorWindow
{
    Camera selectedCamera = null;
    //CollisionTesterComponent collisionTesterComponent = null;
    public const string WINDOW_TITLE = "Collision Tester";

    [MenuItem("Tools/Collision Tester", false, 13)]
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
        EditorGUILayout.ObjectField(selectedCamera, typeof(Camera), true);
        if (selectedCamera)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            // TODO Remove When Finished
            EditorGUILayout.HelpBox("Ne Fonctionne Pas", MessageType.Error);
            GUI.enabled = false;

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Sphere Collider"))
            {
                Ray _ray = new Ray(selectedCamera.transform.position, selectedCamera.transform.forward);
                Physics.SphereCast(_ray, 1.0f, out RaycastHit _hitInfo);

                if(_hitInfo.transform)
                {
                    Debug.Log("Hit");
                }

                //if (!collisionTesterComponent)
                //{
                //    collisionTesterComponent = selectedCamera.AddComponent<CollisionTesterComponent>();
                //    collisionTesterComponent.AddSphereCollider();
                //
                //}
            }
            if (GUILayout.Button("Near Plane Border"))
            {

            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();
        //Rect _rect = selectedCamera.pixelRect;
        //Handles.DrawCamera(_rect, selectedCamera);
        //Handles.DrawDottedLine(selectedCamera.transform.position, selectedCamera.transform.position + selectedCamera.transform.forward * 100.0f, 1.0f);
    }

    void RefreshCurrentCamera()
    {
        //if (collisionTesterComponent)
        //{
        //    collisionTesterComponent.onCollisionTrigger = null;
        //}
        selectedCamera = CameraFinder.GetCurrentSelectedCamera();
        //collisionTesterComponent = selectedCamera.GetComponent<CollisionTesterComponent>();
        //if (collisionTesterComponent)
        //{
        //    collisionTesterComponent.onCollisionTrigger += OnCollisionTrigger;
        //}
        Repaint();
    }
    
    //void OnCollisionTrigger(bool _isEnter, GameObject _other)
    //{
    //    Debug.Log(_isEnter);
    //    Debug.Log(_other.name);
    //}
}
