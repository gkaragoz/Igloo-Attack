using System;
using UnityEngine;

public class Utils : MonoBehaviour {

    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class ReadOnlyAttribute : PropertyAttribute { }
#if UNITY_EDITOR
    [UnityEditor.CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeDrawer : UnityEditor.PropertyDrawer {
        public override void OnGUI(Rect rect, UnityEditor.SerializedProperty prop, GUIContent label) {
            bool wasEnabled = GUI.enabled;
            GUI.enabled = false;
            UnityEditor.EditorGUI.PropertyField(rect, prop);
            GUI.enabled = wasEnabled;
        }
    }
#endif

    public static void DrawCircle(Vector3 centerPosition, float frequency, float radiusX, float radiusY, float dotSize) {
        float x;
        float z;

        if (frequency <= 0) {
            frequency = 1;
        }

        float angle = 20f;

        for (int i = 0; i < (frequency + 1); i++) {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusY;

            Gizmos.DrawSphere(centerPosition + new Vector3(x, 0, z), dotSize);

            angle += (360f / frequency);
        }
    }

}
