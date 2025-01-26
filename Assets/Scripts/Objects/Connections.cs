using System.Collections.Generic;
using UnityEngine;

public class Connections : MonoBehaviour
{
    [SerializeField] private List<Transform> _knots;
    [SerializeField] private List<Transform> _ropes;
    private Dictionary<Transform, Transform> _connections = new();
    private void Awake()
    {
        for (int i = 0; i < _knots.Count; i++) _connections.Add(_knots[i], _ropes[i]);
    }
    public List<Transform> GetRelatedKnots()
    {
        return new List<Transform>(_connections.Keys);
    }
    public Transform GetRelatedRope(Transform knot)
    {
        return _connections[knot];
    }
}
