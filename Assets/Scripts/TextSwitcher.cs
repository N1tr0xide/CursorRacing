using TMPro;
using UnityEngine;

public class TextSwitcher : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void TextSwitch(string text)
    {
        _text.text = text;
    }
}
