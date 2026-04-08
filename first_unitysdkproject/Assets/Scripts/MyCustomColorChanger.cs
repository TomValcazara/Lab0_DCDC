using System.Collections;
using UnityEngine;

public class MyCustomColorChanger : MonoBehaviour
{
    [SerializeField]
    private Color normalColor = Color.red;

    [SerializeField]
    private Color hoveredColor = Color.blue;

    [SerializeField]
    private Color selectedColor = Color.green;

    [SerializeField]
    private float transitionTime = 0.5f;

    [SerializeField]
    private Renderer objectRenderer;

    [SerializeField]
    private Vector3 normalScale = Vector3.one;

    [SerializeField]
    private Vector3 hoveredScale = Vector3.one * 1.2f;

    [SerializeField]
    private Vector3 selectedScale = Vector3.one * 1.3f;

    private Material mat;
    private Coroutine currentLerp;

    void Awake()
    {
        mat = objectRenderer.material;
    }

    public void Transition(Color targetColor, Vector3 targetScale)
    {
        if (currentLerp != null)
            StopCoroutine(currentLerp);

        currentLerp = StartCoroutine(LerpColorAndScale(targetColor, targetScale, transitionTime));
    }

    private IEnumerator LerpColorAndScale(Color targetColor, Vector3 targetScale, float duration)
    {
        Color startColor = mat.color;
        Vector3 startScale = transform.localScale;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            t = Mathf.SmoothStep(0f, 1f, t);

            mat.color = Color.Lerp(startColor, targetColor, t);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        mat.color = targetColor;
        transform.localScale = targetScale;
    }

    public void Transition2Hover()
    {
        Transition(hoveredColor, hoveredScale);
    }

    public void Transition2Select()
    {
        Transition(selectedColor, selectedScale);
    }

    public void Transition2Normal()
    {
        Transition(normalColor, normalScale);
    }
}