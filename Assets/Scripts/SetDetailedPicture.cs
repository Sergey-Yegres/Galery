using UnityEngine;

public class SetDetailedPicture : MonoBehaviour
{
    private PlateLogic _detailedPlate;
    private RectTransform _image;
    [SerializeField] private RectTransform _canvas;
    void Start()
    {
        _image = GetComponent<RectTransform>();
        _detailedPlate = GetComponent<PlateLogic>();

        _detailedPlate.LoadPlateImageInRawImage(PlayerPrefs.GetString("ImageLink"),"");
    }
    private void FixedUpdate()
    {
        _detailedPlate.AdjustImageSize(_canvas.sizeDelta);
    }

}
