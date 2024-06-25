using UnityEngine;

public sealed class PopupService : MonoBehaviour
{
    [SerializeField] private GameService _gameService;
    [SerializeField] private TimerService _timerService;
    [SerializeField] private PopupController _popupController;

    private void Start()
    {
        ShowStartGamePopup();

        _timerService.OnUpTimed += ShowEndGamePopup;
        _gameService.OnGameEnded += ShowEndGamePopup;
    }

    private void OnDestroy()
    {
        _timerService.OnUpTimed -= ShowEndGamePopup;
        _gameService.OnGameEnded -= ShowEndGamePopup;
    }

    private void ShowStartGamePopup()
    {
        _popupController.ShowPopup(new StartGamePopupSettings());
    }

    private void ShowEndGamePopup(string message)
    {
        _timerService.StopTimer();

        _popupController.ShowPopup(new EndGamePopupSettings
        {
            LocalizationKey = message,
        });
    }

}
