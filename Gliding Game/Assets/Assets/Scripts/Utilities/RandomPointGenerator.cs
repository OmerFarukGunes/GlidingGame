using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class RandomPointGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 xPosLimits;
    [SerializeField] private Vector2 zPosLimits;

    [SerializeField] private int pointCount;
    [SerializeField] private float minDistance;

    public List<Vector3> PositionList = new List<Vector3>();

    [ContextMenu("Generate Random Positions")]
    public void GeneratePositions()
    {
        PositionList.Clear();

        int attempts = 0;
        int maxAttempts = pointCount * 10;

        while (PositionList.Count < pointCount && attempts < maxAttempts)
        {
            attempts++;
            float randomX = Random.Range(xPosLimits.x, xPosLimits.y);
            float randomZ = Random.Range(zPosLimits.x, zPosLimits.y);
            Vector3 randomPosition = new Vector3(randomX, 0, randomZ);

            bool isValid = true;

            foreach (var pos in PositionList)
            {
                if (Vector3.Distance(pos, randomPosition) < minDistance)
                {
                    isValid = false;
                    break;
                }
            }
            if (isValid)
            {
                PositionList.Add(randomPosition);
            }
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(RandomPointGenerator))]
public class RandomPositionGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RandomPointGenerator script = (RandomPointGenerator)target;
        if (GUILayout.Button("Generate Random Positions"))
        {
            script.GeneratePositions();
        }
    }
}
#endif