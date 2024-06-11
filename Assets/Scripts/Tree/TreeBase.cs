using Zenject;
using UnityEngine;

public enum ETreeType : sbyte
{
    None = 0, 
    Left = 1,
    Right = 2,
}

public class TreeBase : MonoBehaviour
{
    [SerializeField] private TreeAnimator _animator;

    public ETreeType Type { get; private set; }

    private GameService _gameService;

    [Inject]
    public void Construct(GameService gameService) => _gameService = gameService;

    private void OnEnable()
    {
        _animator.OnAnimationCompleted += AnimationComplete;
        //_gameService.OnTreeAnimationRequested += PlayAnimation;
    }

    private void OnDisable()
    {
        _animator.OnAnimationCompleted -= AnimationComplete;
        //_gameService.OnTreeAnimationRequested -= PlayAnimation;
    }

    public virtual void SetupType (ETreeType type) => Type = type;
    public virtual void SetupTransform(float x, float y) => gameObject.transform.position = new Vector2(x, y);

    private void PlayAnimation(Tree tree, float direction)
    {
        if (tree == this)
            _animator.FlyTree(direction);
    }

    private void AnimationComplete() => gameObject.SetActive(false);
}
