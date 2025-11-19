using System;
using System.Globalization;
using UnityEditor;
using UnityEngine;



[Serializable]
public class CameraShake : MonoBehaviour
{
    static bool isShaking = false;
    static Vector3 initialPosition = Vector3.zero;
    static float duration = 3.0f;
    static float magnitude = 1.0f;
    static GameObject targetCamera = null;
    static double startTime = 0.0;


    public static void Save(float _duration, float _magnitude, string _presetName)
    {
        CameraShakePreset _preset = ScriptableObject.CreateInstance<CameraShakePreset>();
        _preset.duration = _duration;
        _preset.magnitude = _magnitude;

        string _pathToSave = $"Assets/Presets/{_presetName}.asset";
        _pathToSave = AssetDatabase.GenerateUniqueAssetPath(_pathToSave);

        AssetDatabase.CreateAsset(_preset, _pathToSave);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static void TestShake(GameObject _targetCamera, float _duration, float _magnitude)
    {
        if (_targetCamera == null) return;
        if(isShaking) StopShake();
        targetCamera = _targetCamera;
        duration = _duration;
        magnitude = _magnitude;
        initialPosition = _targetCamera.transform.position;
        startTime = EditorApplication.timeSinceStartup;
        isShaking = true;


        EditorApplication.update += UpdateShake;
    }

    private static void UpdateShake()
    {
        if(!isShaking || targetCamera == null)
        {
            StopShake();
            return;
        }

        float _elapsed = (float)(EditorApplication.timeSinceStartup - startTime);
        float _time = _elapsed / duration;

        if(_time >= 1f)
        {
            StopShake();
            return;
        }

        float _damping = EaseInOutBounce(_time);

        Vector3 _randomOffset = UnityEngine.Random.insideUnitSphere * magnitude * _damping;

        targetCamera.transform.localPosition = initialPosition + _randomOffset;

        SceneView.RepaintAll();
    }

    private static void StopShake()
    {
        isShaking = false;
        EditorApplication.update -= UpdateShake;

        if(targetCamera != null)
        {
            targetCamera.transform.localPosition = initialPosition;
        }
    }


    private static float EaseInOutBounce(float _x)
    {
        return _x < 0.5f
            ? (1 - EaseOutBounce(1 - 2 * _x)) / 2
            : (1 + EaseOutBounce(2 * _x - 1)) / 2;
    }

    private static float EaseOutBounce(float _x)
    {
        float _n1 = 7.5625f;
        float _d1 = 2.75f;

        if (_x < 1 / _d1)
        {
            return _n1 * _x * _x;
        }
        else if (_x < 2 / _d1)
        {
            return _n1 * (_x -= 1.5f / _d1) * _x + 0.75f;
        }
        else if (_x < 2.5 / _d1)
        {
            return _n1 * (_x -= 2.25f / _d1) * _x + 0.9375f;
        }
        else
        {
            return _n1 * (_x -= 2.625f / _d1) * _x + 0.984375f;
        }
    }
}
