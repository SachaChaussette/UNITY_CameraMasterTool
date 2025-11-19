using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[Serializable]
public class CameraFinder
{
   public static List<Camera> GetAllCameras()
   {
        return GameObject.FindObjectsByType<Camera>(FindObjectsSortMode.None).OrderBy(t => t.name).ToList();
   }

    public static Camera GetCurrentSelectedCamera()
    {
        GameObject _activeGameObject = Selection.activeGameObject;
        return _activeGameObject ? _activeGameObject.GetComponent<Camera>() : null;
    }
}
