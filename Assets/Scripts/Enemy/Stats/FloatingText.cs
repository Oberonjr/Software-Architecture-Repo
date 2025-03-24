using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float fadeInDuration = 0.2f;
    [SerializeField] private float stayDuration = 1f;
    [SerializeField] private float fadeOutDuration = 0.5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 1f, 0);
    
    private TMP_Text textMesh;
    private Color textColor;
    private float lifetime;
    private float alpha = 0f;
    private bool fadingOut = false;
    
    public TMP_Text TextMesh => textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
        textColor.a = 0; // Start invisible
    }

    private void Start()
    {
        lifetime = fadeInDuration + stayDuration + fadeOutDuration;
        textMesh.alignment = TextAlignmentOptions.Bottom;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        if (!fadingOut)
        {
            if (alpha < 1f) alpha += Time.deltaTime / fadeInDuration; // Fade in
            else if (Time.timeSinceLevelLoad > fadeInDuration + stayDuration) fadingOut = true; // Start fade out
        }
        else
        {
            if (alpha > 0f) alpha -= Time.deltaTime / fadeOutDuration; // Fade out
        }

        textColor.a = Mathf.Clamp01(alpha);
        textMesh.color = textColor;
    }

    public void SetText(string text, Color color)
    {
        textMesh.text = text;
        textColor = color;
    }
}

