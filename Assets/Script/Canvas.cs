using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    public GameObject canvasPrefab;
    public GameObject textPrefab;
    private GameObject _canvasObject;
    private GameObject _textObject;
    private TextMeshPro _textMeshPro;

    void Start()
    {
        _canvasObject = Instantiate(canvasPrefab);
        _textObject = Instantiate(textPrefab, _canvasObject.transform);
        _textMeshPro = _textObject.GetComponent<TextMeshPro>();

        if (_textMeshPro == null)
        {
            Debug.LogError("text prefab does not have TextMeshPro component");
        }
        else
        {
            _textMeshPro.text = "Hello World";
            _textMeshPro.alignment = TextAlignmentOptions.Center;
            _textMeshPro.rectTransform.anchoredPosition = new Vector2(0, 0);
            _textMeshPro.rectTransform.localScale = new Vector3(1, 1, 1);
        }
    }
}
