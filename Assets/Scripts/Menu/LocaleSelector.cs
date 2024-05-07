using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class Localization : MonoBehaviour
{
    private void Start()
    {
       int savedLocaleID = PlayerPrefs.GetInt("LocaleKey", 0);
       ChangeLocale(savedLocaleID);
    }

    private bool active = false;
    public void ChangeLocale(int localeID)
    {
        if (active == true)
            return;
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("LocaleKey", _localeID); // Save selected locale to PlayerPrefs
        active = false;
    }
}
