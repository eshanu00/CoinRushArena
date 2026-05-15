using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    private PlayerActions playerActions;

    private void Start()
    {
        playerActions = GetComponentInParent<PlayerActions>();
    }

    public void OnDeathAnimationFinished()
    {
        playerActions.OnDeathAnimationFinished();
    }
}