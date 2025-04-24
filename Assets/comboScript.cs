using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class comboScript : MonoBehaviour
{
    private TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI highestText;
    [SerializeField] private float fadeDuration = 1.5f;

    public float maxAlpha = 1f;
    GameObject player;

    public int _currentCombo, _highestCombo;
    public float _alpha = 0f;
    private Vector3 _baseOffset = new Vector3(0, .1f, 0);
    RectTransform rectTransform;
    spawnerScript spawnerScript;

    bool hasOther;

    private Coroutine otherRoutine;
    // Start is called before the first frame update
    void Start()
    {
        comboText = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        player = GameObject.Find("player");
        spawnerScript = GameObject.Find("dummy1").GetComponent<spawnerScript>();
        _highestCombo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentCombo == 1) comboText.text = null;
        else if (_currentCombo > 1)
        {
            comboText.text = $"{_currentCombo} COMBO!";
            comboText.color = new Color(1, 1, 1, _alpha);
        }

        if (_alpha > 0)
        {
            _alpha -= Time.deltaTime / fadeDuration;
            comboText.color = new Color(comboText.color.r, comboText.color.g, comboText.color.b, _alpha);
        }

        if (_currentCombo >= 10 && !hasOther) { otherRoutine = StartCoroutine(spawnerScript.otherSpawn()); hasOther = true; }
        if (_currentCombo < 10)  
        {
            if (otherRoutine != null)
            {
                StopCoroutine(otherRoutine);
                otherRoutine = null;
            }
            hasOther = false; 
        }

        if (_currentCombo > _highestCombo) _highestCombo = _currentCombo; // for an obscure reason i thought this would be longer than one line of code
        highestText.text = $"HIGHEST COMBO: {_highestCombo}!";

    }
}
