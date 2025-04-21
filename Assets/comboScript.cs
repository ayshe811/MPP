using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class comboScript : MonoBehaviour
{
    private TextMeshProUGUI comboText;
    [SerializeField] private float fadeDuration = 1.5f;

    public float maxAlpha = 1f;
    GameObject player;

    public int _currentCombo;
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

        if (_currentCombo >= 5 && !hasOther) { otherRoutine = StartCoroutine(spawnerScript.otherSpawn()); hasOther = true; }
        if (_currentCombo < 5) 
        {
            if (otherRoutine != null)
            {
                StopCoroutine(otherRoutine);
                otherRoutine = null;
            }
            hasOther = false; 
        }
    }
}
