using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Script that is Instantiated for a visual floating text for .TakeDamage() and .Die()
public class FloatingText : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float fadeInDuration = 0.2f;
    [SerializeField] private float stayDuration = 1f;
    [SerializeField] private float fadeOutDuration = 0.5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 1f, 0);
    
    private TMP_Text _textMesh;
    private Color _textColor;
    private float _lifetime;
    private float _alpha = 0f;
    private bool _fadingOut = false;
    
    public TMP_Text TextMesh => _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TMP_Text>();
        _textColor.a = 0; // Start invisible
    }

    private void Start()
    {
        _lifetime = fadeInDuration + stayDuration + fadeOutDuration;
        _textMesh.alignment = TextAlignmentOptions.Bottom;
        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        if (!_fadingOut)
        {
            if (_alpha < 1f) _alpha += Time.deltaTime / fadeInDuration; // Fade in
            else if (Time.timeSinceLevelLoad > fadeInDuration + stayDuration) _fadingOut = true; // Start fade out
        }
        else
        {
            if (_alpha > 0f) _alpha -= Time.deltaTime / fadeOutDuration; // Fade out
        }

        _textColor.a = Mathf.Clamp01(_alpha);
        _textMesh.color = _textColor;
    }

    public void SetText(string text, Color color)
    {
        _textMesh.text = text;
        _textColor = color;
    }
}

