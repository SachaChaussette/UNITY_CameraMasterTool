using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CameraToolWindow : EditorWindow
{

    List<ICameraTool> allTools = new List<ICameraTool>();
    int currentTab = 0;
    Camera selectedCamera = null;

    private void OnEnable()
    {
        allTools = FindAllTools();
    }

    List<ICameraTool> FindAllTools()
    {
        List<ICameraTool> _allTools = new List<ICameraTool>();
        Type[] _types = typeof(CameraToolWindow).Assembly.GetTypes();

        foreach (Type _type in _types)
        {
            if (!_type.IsAbstract && typeof(ICameraTool).IsAssignableFrom(_type) && _type.GetConstructor(Type.EmptyTypes) != null)
            {
                ICameraTool _instance = (ICameraTool)Activator.CreateInstance(_type);
                _allTools.Add(_instance);
            }
        }

        return _allTools;
    }

    [MenuItem("CameraTools/Camera Tools")]
    public static void OpenWindow()
    {
        CameraToolWindow _cameraToolWindow = GetWindow<CameraToolWindow>(); 
    }

    private void OnGUI()
    {
        DrawUI();
    }

    void DrawUI()
    {
        if (allTools.Count < 1) return;

        string[] _tabNames = allTools.Select(t => t.Name).ToArray();

        currentTab = GUILayout.Toolbar(currentTab, _tabNames);

        allTools[currentTab].DrawUI(selectedCamera);
    }
}
