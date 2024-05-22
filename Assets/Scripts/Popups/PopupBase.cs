using UnityEngine;
using JetBrains.Annotations;

public class PopupBase : MonoBehaviour
{
    [SerializeField] private PopupAnimator _animator;

    private void Start()
    {
        _animator.Show();
    }

    [UsedImplicitly]
    public void Close()
    {
        _animator.Hide(Destroy);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
