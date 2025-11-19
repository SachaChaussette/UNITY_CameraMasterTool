using System;
using UnityEngine;

[Serializable]
public class CamPlacementCompo : MonoBehaviour
{

    public static void GenerateCamArroundTarget(int _numberOfCam, Vector3 _offSet, GameObject _target)
    {
        if (_target == null) return;


        float _angleStep = 360/_numberOfCam;
        Vector3 _targetPos = _target.transform.position;

        for (int i = 0; i < _numberOfCam; i++)
        {
            Quaternion _rotation = Quaternion.Euler(0, _angleStep * i, 0);

            Vector3 _rotatedOffset = _rotation * _offSet;

            Vector3 _finalPosition = _targetPos + _rotatedOffset;

            GameObject _object = new GameObject($"CameraArroundTarget_{i}");
            _object.transform.position = _finalPosition;
            _object.AddComponent<Camera>();

            _object.transform.LookAt(_targetPos);
        }
    }
}
