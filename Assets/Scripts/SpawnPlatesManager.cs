using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlatesManager : MonoBehaviour
{
    private Transform _grid;
    [SerializeField] private GameObject _platePrefab;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Canvas _canvas;

    private string _url = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private Coroutine _spawnPlates;

    private void Start()
    {
        _grid = GetComponent<Transform>();
        _spawnPlates = StartCoroutine(SpawnPlates());

    }
    private const int _maxPlatesCount = 66;
    private int _createdPlatesCount = 1;
    private RectTransform _lastPlate;
    private IEnumerator SpawnPlates()
    {

        CreateNewPlate(_createdPlatesCount);
        CreateNewPlate(_createdPlatesCount);
        while (true)
        {
            if (_lastPlate.position.y > -30) 
            {
                CreateNewPlate(_createdPlatesCount);
                CreateNewPlate(_createdPlatesCount);
            }
            if(_createdPlatesCount > _maxPlatesCount)
            {
                StopCoroutine(_spawnPlates);
            }

            yield return new WaitForEndOfFrame();
        }
    }
    private void CreateNewPlate(int imageNumber)
    {
        var plate = Instantiate(_platePrefab, _grid).GetComponent<PlateLogic>();
        plate.LoadPlateImageInRawImage(imageNumber + ".jpg", _url);
        _lastPlate = plate.GetComponent<RectTransform>();
        _createdPlatesCount++; 
    }
}
