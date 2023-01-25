using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    public GameObject canvasPrefab;
    public GameObject textPrefab;
    private GameObject canvasObject;
    private GameObject textObject;
    private TextMeshPro textMeshPro;

    void Start()
    {
        canvasObject = Instantiate(canvasPrefab);
        textObject = Instantiate(textPrefab, canvasObject.transform);
        textMeshPro = textObject.GetComponent<TextMeshPro>();

        if (textMeshPro == null)
        {
            Debug.LogError("text prefab does not have TextMeshPro component");
        }
        else
        {
            textMeshPro.text = "Hello World";
            textMeshPro.alignment = TextAlignmentOptions.Center;
            textMeshPro.rectTransform.anchoredPosition = new Vector2(0, 0);
            textMeshPro.rectTransform.localScale = new Vector3(1, 1, 1);
        }
    }
}
