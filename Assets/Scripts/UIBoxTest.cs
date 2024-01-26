using System.Collections;
using TMPro;
using UnityEngine;

public class UIBoxTest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI BoxText;
    [SerializeField] Sprite BoxTextSprite;

     int textSpeed;

    int pluswidth = 0;

    IEnumerator TypeSentence(string frase)
    {
        Debug.Log("inizio a scrivere");
        BoxText.text = "";

        foreach (char letter in frase.ToCharArray())
        {
            pluswidth++;

            BoxText.text += letter;
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(pluswidth,0);
            yield return new WaitForSeconds(1 / textSpeed);

        }  

    }

    public void StartSentece(string frase)
    {
        StartCoroutine(TypeSentence(frase));
    }

    public void SetTextSpeed(int speed)
    {
        textSpeed = speed;
    }


}
