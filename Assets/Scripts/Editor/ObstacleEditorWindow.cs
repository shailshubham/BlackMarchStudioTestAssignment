using UnityEditor;
using UnityEngine;

public class ObstacleEditorWindow : EditorWindow
{
    private ObstacleData obstacleData;

    [MenuItem("Tools/Obstacle Editor")]
    public static void ShowWindow()
    {
        GetWindow<ObstacleEditorWindow>("Obstacle Editor");
    }

    private void OnEnable()
    {
        obstacleData = AssetDatabase.LoadAssetAtPath<ObstacleData>("Assets/ObstacleData.asset");
    }

    private void OnGUI()
    {
        if (obstacleData == null)
        {
            EditorGUILayout.HelpBox("No Obstacle Data found. Please create an ObstacleData asset in the Assets folder.", MessageType.Warning);
            return;
        }

        for (int x = 0; x < 10; x++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int y = 0; y < 10; y++)
            {
                bool currentValue = obstacleData.GetObstacle(x, y);
                bool newValue = GUILayout.Toggle(currentValue, "");
                if (newValue != currentValue)
                {
                    obstacleData.SetObstacle(x, y, newValue);
                    EditorUtility.SetDirty(obstacleData);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Save Obstacles"))
        {
            AssetDatabase.SaveAssets();
        }
    }
}
