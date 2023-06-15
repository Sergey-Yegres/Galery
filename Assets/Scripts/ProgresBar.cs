using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgresBar : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ChangeProgresBar());
    }
    [SerializeField] private float _loadTimeInSeconds;
    [SerializeField] private int _iterationsNumber;
    [SerializeField] private TextMeshProUGUI _percentsText;
    private IEnumerator ChangeProgresBar()
    {
        var scrollbar = GetComponent<Scrollbar>();
        for (int i = 1; i < _iterationsNumber; i++) 
        {
            scrollbar.size = (float)i / (float)_iterationsNumber;
            _percentsText.text = 100f / ((float)_iterationsNumber / (float)i) + "%";
            yield return new WaitForSeconds(_loadTimeInSeconds / (float)_iterationsNumber);
        }
        SceneController.Instance.GoToNextScene();
    }
}
