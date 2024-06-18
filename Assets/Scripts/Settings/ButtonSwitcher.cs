using UnityEngine;
using JetBrains.Annotations;

public sealed class ButtonSwitcher : MonoBehaviour
{
    [UsedImplicitly] public void QuitGame() => Application.Quit();
}
