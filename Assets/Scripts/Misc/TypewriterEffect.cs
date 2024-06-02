using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private TMP_Text introTextField;

    private string introText;

    private void Start()
    {
        introText = introTextField.text;
        introTextField.text = "";

        StartCoroutine(TypeTextRoutine());
    }

    private IEnumerator TypeTextRoutine()
    {
        foreach (char letter in introText.ToCharArray())
        {
            introTextField.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
