using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadContent : MonoBehaviour
{
    public Coroutine DownloadImageCoroutine;

    public void DownloadFileContentUnity(string urlDownload, string localPath, Action callback)
    {
        DownloadImageCoroutine = StartCoroutine(WaitLoad());

        IEnumerator WaitLoad()
        {
            var task = UnityWebRequestTexture.GetTexture(urlDownload);
            task.SendWebRequest();
            yield return new WaitUntil(() => task.isDone);

            if (task.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning(task.result + " " + task.error);
                Debug.Log("interneterror - " + localPath);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                byte[] dataBytes = task.downloadHandler.data;
                try
                {
                    File.WriteAllBytes(Application.persistentDataPath + "/" + localPath, dataBytes);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            task.Dispose();

            callback?.Invoke();
        }
    }
}
