using UnityEngine;

public sealed class Tree : TreeBase
{
    [SerializeField] private GameObject _branch;

    public override void SetupType(ETreeType type)
    {
        base.SetupType(type);
        EditSettings();
    }

    private void ResetSettings()
    {
        _branch.transform.rotation = Quaternion.identity;

        if (_branch.transform.position.x < 0)
            _branch.transform.position = new Vector2(-_branch.transform.position.x, _branch.transform.position.y);
    }
    
    private void LeftSettings()
    {
        _branch.transform.rotation = Quaternion.Euler(new Vector3(0, 180));
        _branch.transform.position = new Vector2(-_branch.transform.position.x, _branch.transform.position.y);
    }

    private void EditSettings()
    {
        if (Type == ETreeType.None)
        {
            _branch.gameObject.SetActive(false);
            ResetSettings();
        }
        else if (Type == ETreeType.Left)
        {
            _branch.gameObject.SetActive(true);
            ResetSettings();
            LeftSettings();
        }
        else if (Type == ETreeType.Right) 
        {
            _branch.gameObject.SetActive(true);
            ResetSettings();
        }
    }
}
   
