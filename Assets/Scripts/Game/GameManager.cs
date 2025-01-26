using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _skipTime;
    [SerializeField] private Slider _slider;
    private List<Transform> _ropes;
    private int _ropesOverlap;
    private float _timer;
    private bool _canSkip;
    private void Update()
    {
        if (_timer <= _skipTime) 
        {
            _timer += Time.deltaTime;
            _slider.value = _timer / _skipTime;
        }
        else if (!_canSkip)
        {
            _canSkip = true;
            EventManager.OnTimerTrigger();
        }
        EventManager.KnotPosUpdated += GetRopesOverlapCount;
    }
    public void SetRopes(List<Transform> ropes)
    {
        _ropes = ropes;
    }
    private void GetRopesOverlapCount()
    {
        _ropesOverlap = 0;
        foreach (Transform rope in _ropes) _ropesOverlap += rope.GetComponent<Rope>().GetOverlapRopesCount();
        if (_ropesOverlap == 0) EventManager.OnPuzzleComplete();
    }
}
