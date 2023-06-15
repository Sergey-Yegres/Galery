using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlateLogic : MonoBehaviour
{
    [SerializeField]
    private LoadContent _loadContent;

    private string _imageLink;

    private Action<Texture2D> _onSpawned;
    private Action _canCheckImageFile;

    [SerializeField]
    private RawImage _plateImg;
    [SerializeField]
    private RectTransform _changeImgSizeToThis;
    [SerializeField]
    private GameObject _loadingObj;

    private void OnEnable()
    {
      
        _onSpawned += GetTexture;
          
        _canCheckImageFile += CheckImageFileExists;

        if (_plateImg.gameObject.GetComponent<Button>())
        {
            _plateImg.gameObject.GetComponent<Button>().onClick.AddListener
                (() => {
                    SceneController.Instance.GoToNextScene();
                    PlayerPrefs.SetString("ImageLink", _imageLink);
                });
        }
    }
    private void OnDisable()
    {
      
        _onSpawned -= GetTexture;
     
        _canCheckImageFile -= CheckImageFileExists;
      
    }
    private void OnDestroy()
    {
        Destroy(_plateImg.texture);
    }

    public void LoadPlateImageInRawImage(string imageLink, string url)
    {
        _loadingObj.SetActive(true);
        _imageLink = imageLink;
        if (!File.Exists(Application.persistentDataPath + "/" + imageLink))
            _loadContent.DownloadFileContentUnity(url + imageLink, imageLink, _canCheckImageFile);
        else
            CheckImageFileExists();
    }

    public void GetTexture(Texture2D texture)
    {
        _plateImg.texture = texture;
        _plateImg.GetComponent<RectTransform>().sizeDelta = new Vector2(texture.width, texture.height);

        AdjustImageSize(_changeImgSizeToThis.sizeDelta);

        _loadingObj.SetActive(false);

        _onSpawned -= GetTexture;
        _canCheckImageFile -= CheckImageFileExists;
    }
    public void AdjustImageSize(Vector2 adjustSizeToThis)
    {

        var RT = _plateImg.GetComponent<RectTransform>();
        float width = RT.sizeDelta.x;
        float height = RT.sizeDelta.y;

        if(adjustSizeToThis.x > adjustSizeToThis.y)
        {
            var difference = adjustSizeToThis.y / height;
            RT.sizeDelta *= difference;
        }
        else
        {
            var difference = adjustSizeToThis.x / width;
            RT.sizeDelta *= difference;
        }
    }
    public void CheckImageFileExists()
    {
            StartCoroutine(ConvertToTexture.Convert(_imageLink, _onSpawned));
            OnDisable();
    }
}