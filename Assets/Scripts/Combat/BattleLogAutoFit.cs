using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BattleLogAutoFit : MonoBehaviour
{
    private TextMeshProUGUI textGUI;
    private Queue<string> logLines;

    [SerializeField]
    private bool autoScroll = true;

    private void Awake()
    {
        textGUI = GetComponent<TextMeshProUGUI>();
        logLines = new Queue<string>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLog(string text)
    {
        logLines.Enqueue(text);

        UpdateLogDisplay();
        while (IsOverflowing() && logLines.Count > 0)
        {
            logLines.Dequeue();
            UpdateLogDisplay();
        }

    }

    private void UpdateLogDisplay()
    {
        textGUI.text = string.Join("\n", logLines);

        // IsTextOverflowing is only updated after TextMesh updates geometry
        // Force this by using Mesh Update
        textGUI.ForceMeshUpdate();

        // Auto-scroll if inside a ScrollRect
        if (autoScroll && TryGetComponent(out UnityEngine.UI.ScrollRect scrollRect))
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }    
    }

    private bool IsOverflowing()
    {
        textGUI.ForceMeshUpdate();
        return textGUI.isTextOverflowing;
    }
}
