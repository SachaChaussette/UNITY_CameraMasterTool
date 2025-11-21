using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraShakePreset", menuName = "CameraTool/Camera Shake Preset", order = 1)] // Obligatoire pour qye Unity reconnaisse le Scriptable Object
public class CameraShakePreset : ScriptableObject
{
    public float amplitude;
    public float frequency;
    public float duration;

    // Stocke des données de manière persitente et ordonnée en dehors des Scenes et contrairement aux MonoBehaviour, ils ne sont pas liés aux GameObject
    // Classe qui hérite de ScriptableObject au lieu de MonoBehaviour
    // Elle est utilisé pour stocker des données et les réutilisé dans plusieurs Scene sans créer d'Instance
    // Point + : Réduit l'utilisation de mémoire (vu qu'on duplique pas la donnée), facilite la gestion des données persistente (variable est unique), permet d'être modifié dans l'editor sans toucher aux codes
    // Point - : Ne peut pas être attaché à un GameObject comme pour un Component
}

[Serializable]
public class CameraShake : ICameraTool
{
    public string Name => "Camera Shake";

    public void DrawScene(Camera _camera)
    {
        
    }

    public void DrawUI(Camera _camera)
    {
        
    }
}
