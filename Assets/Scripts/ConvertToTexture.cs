using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ConvertToTexture : MonoBehaviour
{
    public static IEnumerator Convert(string imagePath, Action<Texture2D> callback)
    {
        string imageFilesPathC = Application.persistentDataPath + "/" + imagePath;
        var readingTaskInFileC = File.ReadAllBytes(imageFilesPathC);

        Texture2D imageTexture = new Texture2D(4, 4, TextureFormat.RGB24, false);
      
        imageTexture.LoadImage(readingTaskInFileC);
      
        if (imageTexture.width % 4 != 0 || imageTexture.height % 4 != 0)
        {
            int sizeXShadersInConvertToTexture = imageTexture.width - imageTexture.width % 4;
            int sizeYShaders = imageTexture.height - imageTexture.height % 4;
            var newPixelsInImageTexture = imageTexture.GetPixels(0, 0, sizeXShadersInConvertToTexture, sizeYShaders);
            imageTexture.Reinitialize(sizeXShadersInConvertToTexture, sizeYShaders);
            imageTexture.SetPixels(newPixelsInImageTexture);
            imageTexture.Apply(false, false);
        }
      
        imageTexture.Apply(false, false);

        callback?.Invoke(imageTexture);
        yield return callback;
    }
}
