using System;
public class EventManager
{
    public static event Action RopesInitialized;
    public static event Action KnotPosUpdated;
    public static event Action KnotGrabbed;
    public static event Action KnotReleased;
    public static event Action TimerTriggered;
    public static event Action ReturnedToMap;
    public static event Action Completed;
    public static void OnRopesInitializationComplete()
    {
        RopesInitialized?.Invoke();
    }
    public static void OnKnotPosUpdate()
    {
        KnotPosUpdated?.Invoke();
    }
    public static void OnKnotGrab()
    {
        KnotGrabbed?.Invoke();
    }
    public static void OnKnotRelease()
    {
        KnotReleased?.Invoke();
    }
    public static void OnTimerTrigger()
    {
        TimerTriggered?.Invoke();
    }
    public static void OnMapReturn()
    {
        ReturnedToMap?.Invoke();
    }
    public static void OnPuzzleComplete()
    {
        Completed?.Invoke();
    }
}
