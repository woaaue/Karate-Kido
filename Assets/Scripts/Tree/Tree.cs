using Zenject;
using UnityEngine;

public sealed class Tree : TreeBase
{
    [SerializeField] private GameObject _branch;

    public override void SetupType(ETreeType type)
    {
        base.SetupType(type);
        EditSettings();
    }

    private void ResetSettings() => gameObject.transform.rotation = Quaternion.identity;
    private void LeftSettings() => gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));

    private void EditSettings()
    {
        if (Type == ETreeType.None)
        {
            _branch.gameObject.SetActive(false);
            ResetSettings();
        }
        else if (Type == ETreeType.Left)
        {
            LeftSettings();
        }
        else if (Type == ETreeType.Right) 
        {
            _branch.gameObject.SetActive(true);
            ResetSettings();
        }
    }
}
   
