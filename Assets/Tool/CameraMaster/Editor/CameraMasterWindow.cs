using UnityEditor;
using UnityEngine;

public class CameraMasterWindow : EditorWindow
{
    [MenuItem("Tools/Camera Master Tool")]
    public static void OpenWindow()
    {
        QuickSwitcherWindow _quickSwictcherWindow = GetWindow<QuickSwitcherWindow>(QuickSwitcherWindow.WINDOW_TITLE, false);
        FOVEditorWindow _fovEditorWindow = GetWindow<FOVEditorWindow>(FOVEditorWindow.WINDOW_TITLE, false, typeof(QuickSwitcherWindow));
        _quickSwictcherWindow.Show();
    }
}
