using UnityEngine;

public class Skip : MonoBehaviour
{
    private bool _canSkip;
    private void OnEnable()
    {
        EventManager.TimerTriggered += CanSkip;
    }
    private void OnDisable()
    {
        EventManager.TimerTriggered -= CanSkip;
    }
    private void CanSkip()
    {
        _canSkip = true;
    }
    public void SkipPuzzle()
    {
        if (_canSkip)
        {
            EventManager.OnPuzzleComplete();
        }
        else
        {
            EventManager.OnMapReturn();
        }
    }
}
