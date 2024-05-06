using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

public class FontResponder : MonoBehaviour
{
    /*
    [SerializeField] private string IzLabel;
    [SerializeField] private Material fontMaterial;
    [SerializeField] private TMP_FontAsset fontEn; // Default font for English
    [SerializeField] private TMP_FontAsset fontSw; // Font for Swedish
    [SerializeField] private TMP_FontAsset fontSp; // Font for Spanish
    [SerializeField] private TMP_FontAsset fontDu; // Font for Dutch

    // Mapping between language codes and locale IDs
    private Dictionary<string, int> languageToLocaleID = new Dictionary<string, int>
    {
        { "en", 0 }, // English locale ID
        { "sw", 1 }, // Swedish locale ID
        { "es", 2 }, // Spanish locale ID
        { "nl", 3 }  // Dutch locale ID
    };

    private TMP_Text tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += OnLanguageChanged;
        OnLanguageChanged();
    }

    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLanguageChanged;
    }

    public void OnLanguageChanged(Locale obj)
    {
        // Get the locale ID of the selected locale
        int localeID = obj?.Identifier.CultureInfo.LCID ?? 0;

        // Set font based on the locale ID
        switch (localeID)
        {
            case 1:
                tmpText.font = fontSw != null ? fontSw : LanguageManager.Instance.fontSw;
                break;
            case 2:
                tmpText.font = fontSp != null ? fontSp : LanguageManager.Instance.fontSp;
                break;
            case 3:
                tmpText.font = fontDu != null ? fontDu : LanguageManager.Instance.fontDu;
                break;
            default:
                tmpText.font = fontEn != null ? fontEn : LanguageManager.Instance.fontEn;
                break;
        }

        if (!string.IsNullOrEmpty(IzLabel))
        {
            // Get the LocalizeStringEvent component attached to this GameObject
            LocalizeStringEvent localizeStringEvent = GetComponent<LocalizeStringEvent>();
            if (localizeStringEvent != null)
            {
                // Set the key for the localized string
                localizeStringEvent.StringReference.TableEntryReference = IzLabel;
                // Force update to get the latest localized string
                localizeStringEvent.UpdateString();
            }
        }
    }
}
    */
}