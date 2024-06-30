using Zenject;
using UnityEngine;

public sealed class NotifyService : MonoBehaviour
{
    [SerializeField] private NotifyController _controller;

    private LevelSystemService _levelSystemService;

    [Inject]
    public void Construct(LevelSystemService levelSystemService)
    {
        _levelSystemService = levelSystemService;
        _levelSystemService.OnLevelChanged += ShowNewLevelNotify;
    }

    private void ShowNewLevelNotify()
    {
        var info = _levelSystemService.GetInfo();

        _controller.ShowNotify(new NewLevelNotifySettings
        {
            Info = info,
        });
    }
}
