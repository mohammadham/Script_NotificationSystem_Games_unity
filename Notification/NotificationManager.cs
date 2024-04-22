using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Resources.Notification
{
    public class NotificationManager
    {
        //custom this script for own use
        private string url = "https://api.example.com/data";
        private List<Notification> ListOfNotifications;

        public List<Notification> GetNotificationFromServer()
        {
            try
            {
                CoroutineWithData coroutine = new CoroutineWithData(FetchDataFromServer());
                while (!coroutine.isDone)
                {
                    // Wait for the coroutine to finish
                }

                return ListOfNotifications;
            }
            catch (Exception e)
            {
                Debug.Log("[Notification] : cant fetch data from server");
                throw;
            }
        }

        IEnumerator FetchDataFromServer()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error: " + webRequest.error);
                }
                else
                {
                    string json = webRequest.downloadHandler.text;
                    ListOfNotifications = JsonUtility.FromJson<List<Notification>>(json);

                    if (ListOfNotifications == null)
                    {
                        Debug.LogError("Failed to deserialize JSON into List<Notification>");
                    }
                    else
                    {
                        yield return true;
                    }
                }
            }
        }
    }

    public class CoroutineWithData
    {
        public bool isDone = false;
        private IEnumerator coroutine;

        public CoroutineWithData(IEnumerator c)
        {
            coroutine = c;
        }

        public IEnumerator Run()
        {
            while (coroutine.MoveNext())
            {
                yield return coroutine.Current;
            }
            isDone = true;
        }
    }
}
