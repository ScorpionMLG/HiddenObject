using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameManager _gm;
    [SerializeField] private float _radius;
    private Transform[] _knots;
    private Transform[] _ropes;
    private const int KNOTS_COUNT = 14;
    private const int ROPES_COUNT = 20;
    private const int DEG_IN_CIRCLE = 360;
    private const float NORMALIZE_SIZE = 0.055f;
    void Start()
    {
        StartCoroutine(WaitRopesInitialization());
        _ropes = new Transform[ROPES_COUNT];
        for (int i = 0; i < ROPES_COUNT; i++) _ropes[i] = transform.GetChild(0).GetChild(i);
        _knots = new Transform[KNOTS_COUNT];
        for (int i = 0; i < KNOTS_COUNT; i++)
        {
            Transform currentKnot = transform.GetChild(1).GetChild(i);
            _knots[i] = currentKnot;
            float angle = Mathf.Deg2Rad * DEG_IN_CIRCLE / KNOTS_COUNT * i;
            currentKnot.position = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _radius;
        }
        foreach (Transform knot in _knots)
        {
            List<Transform> connectedKnots = knot.GetComponent<Connections>().GetRelatedKnots();
            List<Transform> relatedRopes = new();
            foreach (Transform connectedKnot in connectedKnots)
            {
                Transform currentRope = knot.GetComponent<Connections>().GetRelatedRope(connectedKnot);
                relatedRopes.Add(currentRope);
                List<Transform> attachedKnots = new();
                attachedKnots.Add(knot);
                attachedKnots.Add(connectedKnot);
                currentRope.GetComponent<Rope>().SetAttachedKnots(attachedKnots);
                currentRope.position = (connectedKnot.position - knot.position) / 2 + knot.position + Vector3.forward / 10;
                currentRope.rotation = Quaternion.LookRotation(Vector3.forward, connectedKnot.position - knot.position);
                currentRope.Rotate(Vector3.forward, 90);
                currentRope.localScale = new Vector3(Vector2.Distance(connectedKnot.position, knot.position) * NORMALIZE_SIZE, 1, 1);
            }
            foreach (Transform connectedKnot in connectedKnots)
            {
                Transform currentRope = knot.GetComponent<Connections>().GetRelatedRope(connectedKnot);
                currentRope.GetComponent<Rope>().AddRelatedRopes(relatedRopes);
            }
        }
        List<Transform> tempList = new();
        foreach (Transform rope in _ropes) tempList.Add(rope);
        _gm.SetRopes(tempList);
    }
    private IEnumerator WaitRopesInitialization()
    {
        yield return new WaitForEndOfFrame();
        EventManager.OnRopesInitializationComplete();
    }
}
