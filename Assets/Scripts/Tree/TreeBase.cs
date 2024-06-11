using UnityEngine;

public enum ETreeType : sbyte
{
    None = 0, 
    Left = 1,
    Right = 2,
}

public class TreeBase : MonoBehaviour
{
    private protected ETreeType _type;

    public virtual void SetupType (ETreeType type) => _type = type;
    public virtual void SetupTransform(float x, float y) => gameObject.transform.position = new Vector2(x, y);
}
