using UnityEngine;

public interface ICameraTool
{
    string Name {  get; }
    void DrawUI(Camera _camera);
    void DrawScene(Camera _camera);
}
