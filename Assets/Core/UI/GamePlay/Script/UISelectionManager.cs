using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISelectionManager : MonoBehaviour
{
    public Button buttonSelect;
    public Button buttonLeft;
    public Button buttonRight;
    public Button buttonBack;
    public GameObject pKindergarten;
    public GameObject pyramid;
    private Coroutine scaleCoroutine;

    void Start()
    {
        buttonSelect.onClick.AddListener(OnSelectButtonClicked);
        buttonBack.onClick.AddListener(OnBackButtonClicked);

        // Initially hide the Back button
        buttonBack.gameObject.SetActive(false);
    }

    void OnSelectButtonClicked()
    {
        // Hide the Select, Left, and Right buttons and show the Back button
        buttonSelect.gameObject.SetActive(false);
        buttonLeft.gameObject.SetActive(false);
        buttonRight.gameObject.SetActive(false);
        buttonBack.gameObject.SetActive(true);

        // Scale P_Kindergarten to 1 over 1 second
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleOverTime(pKindergarten, Vector3.one, 1f, true));
    }

    void OnBackButtonClicked()
    {
        // Show the Select, Left, and Right buttons and hide the Back button
        buttonSelect.gameObject.SetActive(true);
        buttonLeft.gameObject.SetActive(true);
        buttonRight.gameObject.SetActive(true);
        buttonBack.gameObject.SetActive(false);

        // Show the pyramid before starting the animation
        pyramid.SetActive(true);

        // Scale P_Kindergarten to 0.1 over 1 second
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleOverTime(pKindergarten, new Vector3(0.1f, 0.1f, 0.1f), 1f, false));
    }

    private IEnumerator ScaleOverTime(GameObject target, Vector3 toScale, float duration, bool hidePyramidAfter)
    {
        Vector3 originalScale = target.transform.localScale;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            target.transform.localScale = Vector3.Lerp(originalScale, toScale, currentTime / duration);
            yield return null;
        }

        target.transform.localScale = toScale;

        // Hide the pyramid if needed
        if (hidePyramidAfter)
        {
            pyramid.SetActive(false);
        }
    }
}
