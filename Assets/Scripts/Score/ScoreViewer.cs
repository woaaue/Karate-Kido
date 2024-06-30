using TMPro;
using UnityEngine;

public sealed class ScoreViewer : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private ScoreAnimator _animator;

    private void OnEnable()
    {
        transform.localPosition = Vector3.zero;
        Show();
    }

    public void Setup(int scoreValue) => _text.text = $"+{scoreValue.ToString()}";

    private void Show() => _animator.PlayAnimation(TurnOff);
    private void TurnOff() => gameObject.SetActive(false);
}
