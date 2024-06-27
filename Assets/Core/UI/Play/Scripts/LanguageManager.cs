using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{

    private string _language;
    private TMP_Text _text;
    public delegate void OnLanguageChange(string lang);
    public static event OnLanguageChange onLanguageChange;

    void Awake() {
        _language = "de";
        string currLanguage = PlayerPrefs.GetString("lang");
        if(currLanguage != null) _language = currLanguage;
        _text = gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        _text.text = _language.ToUpper();
    }

    void OnEnable() {
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {
        if(_language == "de") {
            PlayerPrefs.SetString("lang", "en");
            _language = "en";
            onLanguageChange?.Invoke("en");
            _text.text = "EN";
            return;
        }

        PlayerPrefs.SetString("lang", "de");
        _language = "de";
        onLanguageChange?.Invoke("de");
        _text.text = "DE";
        return;
    }
}
