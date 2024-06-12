using UnityEngine;

public sealed class PopupController : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private Transform _popupParent;

    private PopupBase _currentPopup;

    public void ShowPopup<T>(T settings) where T : PopupBaseSettings
    {
        if (_currentPopup == null)
        {
            var popupPrefab = SettingsProvider.Get<PrefabSettings>().GetPopup<T>();
            var instance = Instantiate(popupPrefab, _popupParent, false);
            instance.Setup(settings);
            _currentPopup = instance;
            _currentPopup.OnPopupClosed += HidePopup;
            _background.SetActive(true);
        }
    }

    private void HidePopup()
    {
        _currentPopup.OnPopupClosed -= HidePopup;
        _currentPopup = null;
        _background.SetActive(false);
    }
}
