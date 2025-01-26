using System.Collections;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private Transform _completePanel;
    [SerializeField] private Transform _completeImage;
    [SerializeField] private Transform _map;
    [SerializeField] private TextMeshProUGUI _score;
    private void OnEnable()
    {
        EventManager.Completed += CompletePuzzle;
        EventManager.ReturnedToMap += BackToMap;
    }
    private void OnDisable()
    {
        EventManager.Completed -= CompletePuzzle;
        EventManager.ReturnedToMap -= BackToMap;
    }
    private void CompletePuzzle()
    {
        int result;
        int.TryParse(_score.text, out result);
        _score.text = $"Score: {result + 350}";
        StartCoroutine(ShowComplete());
    }
    private void BackToMap()
    {
        _map.gameObject.SetActive(true);
    }
    private IEnumerator ShowComplete()
    {
        _completePanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        _completePanel.gameObject.SetActive(false);
        _completeImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        _completeImage.gameObject.SetActive(false);
        _map.gameObject.SetActive(true);
    }
}
