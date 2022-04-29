using TMPro;

public class TextMeshPro_TextShared : TextMeshProUGUI
{
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();

        if (UIDialogs.Instance)
        {
            font = UIDialogs.Instance.TMPSettings.FontAsset;
            m_characterSpacing = UIDialogs.Instance.TMPSettings.CharacterSpacing;

            UpdateFontAsset();
        }
    }
#endif
}