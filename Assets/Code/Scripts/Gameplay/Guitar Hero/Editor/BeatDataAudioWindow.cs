using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Gameplay.GuitarHero
{
    public class BeatDataAudioWindow : EditorWindow
    {
        private BeatDataScriptable beatData;

        private Vector2 scrollPos;
        private float[] samples;
        private int displayResolution = 1024;

        [MenuItem("Window/Audio/Beat Marker")]
        public static void ShowWindow() => GetWindow<BeatDataAudioWindow>("Audio Beat Marker");
        private void OnGUI()
        {
            GUILayout.Label("Audio Beat Marker", EditorStyles.boldLabel);
            beatData = (BeatDataScriptable)EditorGUILayout.ObjectField("Beat Data", beatData, typeof(BeatDataScriptable), false);

            if (beatData == null) return;

            if (GUILayout.Button("Load Audio Data")) LoadAudioData();
            if (samples != null && samples.Length > 0)
            {
                if (GUILayout.Button("Clear Marked Times")) ClearMarkedTimes();
                DrawAudioGraph();
            }

            GUILayout.Label("Marked Times", EditorStyles.boldLabel);
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));
            for (int i = 0; i < beatData.BeatTimes.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"Time {i + 1}: {beatData.BeatTimes[i]:F2}s", GUILayout.Width(200));
                if (GUILayout.Button("Remove", GUILayout.Width(100))) beatData.BeatTimes.RemoveAt(i);
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }

        private void LoadAudioData()
        {
            if (beatData.Clip == null) { Debug.LogError("No AudioClip selected!"); return; }

            samples = new float[beatData.Clip.samples * beatData.Clip.channels];
            beatData.Clip.GetData(samples, 0);

            if (samples.All(s => s == 0)) Debug.LogError("All samples are zero.");
        }
        private void DrawAudioGraph()
        {
            GUILayout.Label("Audio Waveform", EditorStyles.boldLabel);

            Rect rect = GUILayoutUtility.GetRect(512, 100);
            EditorGUI.DrawRect(rect, Color.black);

            if (samples == null || samples.Length == 0) return;

            int step = Mathf.Max(1, samples.Length / displayResolution);
            float[] displaySamples = new float[displayResolution];

            for (int i = 0; i < displayResolution; i++) displaySamples[i] = samples[i * step];

            Handles.color = Color.green;
            for (int i = 0; i < displaySamples.Length - 1; i++)
            {
                float x1 = rect.x + (rect.width * i / displaySamples.Length);
                float y1 = rect.y + rect.height / 2 - displaySamples[i] * rect.height / 2;

                float x2 = rect.x + (rect.width * (i + 1) / displaySamples.Length);
                float y2 = rect.y + rect.height / 2 - displaySamples[i + 1] * rect.height / 2;

                Handles.DrawLine(new Vector3(x1, y1), new Vector3(x2, y2));
            }

            Handles.color = Color.red;
            foreach (float time in beatData.BeatTimes)
            {
                float normalizedTime = time / beatData.Length;
                float x = rect.x + rect.width * normalizedTime;
                float y1 = rect.y;
                float y2 = rect.y + rect.height;

                Handles.DrawLine(new Vector3(x, y1), new Vector3(x, y2));
            }

            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 0 && rect.Contains(e.mousePosition))
            {
                float time = (e.mousePosition.x - rect.x) / rect.width * beatData.Length;
                if (beatData.BeatTimes.Contains(time)) return;
                beatData.BeatTimes.Add(time);
                beatData.BeatTimes.Sort();
                SaveMarkedTimes();
                Repaint();
            }
        }
        private void ClearMarkedTimes() => beatData.BeatTimes.Clear();
        private void SaveMarkedTimes()
        {
            Undo.RecordObject(beatData, "Modified ScriptableObject");
            EditorUtility.SetDirty(beatData);
        }
    }
}