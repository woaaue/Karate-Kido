using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class LevelView : MonoBehaviour
{
    [SerializeField] private Image _levelBar;
    [SerializeField] private TextMeshProUGUI _currentLevel;
    [SerializeField] private LevelSystemService _levelService;

    private void Start()
    {
        _levelService.OnLevelChange += EditView;
        _levelService.OnCurrentExperienceChanged += EditView;

        EditView();
    }

    private void EditView()
    {
        var levelData = _levelService.GetData();

        _currentLevel.text = levelData.CurrentLevel.ToString();
        _levelBar.fillAmount = (float)levelData.CurrentExperience / (float)levelData.ExperienceNextLevel;
    }
}
