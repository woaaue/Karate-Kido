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

    public virtual void Setup (ETreeType type)
    {
        _type = type;
    }
}
