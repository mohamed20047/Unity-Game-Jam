using UnityEngine;

public class DebugPanel : MonoBehaviour
{
#if UNITY_EDITOR
    bool _open;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) _open = !_open;
    }

    void OnGUI()
    {
        if (!_open) return;
        GUILayout.BeginArea(new Rect(12, 12, 230, 320), GUI.skin.box);
        if (GUILayout.Button("Kill all enemies")) ;// Wave.KillAll();
        if (GUILayout.Button("Give 1000 gold")) ;// Wallet.Add(1000);
        if (GUILayout.Button("Skip level")) GameFlow.I.Go(Phase.GameOver);
        Time.timeScale = GUILayout.HorizontalSlider(Time.timeScale, 0f, 4f);
        GUILayout.EndArea();
    }
#endif
}