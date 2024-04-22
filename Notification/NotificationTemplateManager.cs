using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Resources.Notification
{
    public class NotificationTemplateManager:MonoBehaviour
    {
        [SerializeField] [Header("TemplateForNotification")]
        public NotificationTemplate NotificationTemplateObject;
        //[HideInInspector]
        [SerializeField] [Header("list of notification")]
        public List<Resources.Notification.Notification> Notifications;


        private List<GameObject> CreatedNotifications=new List<GameObject>();

        private void Awake()
        {
            if (this.NotificationTemplateObject != null)
            {
                this.NotificationTemplateObject.Hide();
            }
///
///
/// first need to get notification from server
///
///
/// custom this part and NotificationManager Calss script for own Use
///
/// 

            this.GetListFromServer();
            if (this.Notifications.Count>0)
            {
                //show list
                this.CreatorNotifications();
            }
        }

        private void Update()
        {
            //update notification list from server in 5 min
            new WaitForSeconds(500);
            //get new list from server ?!
            if (this.GetListFromServer())
            {
                //show list
                this.CreatorNotifications();
            }
        }

        private bool GetListFromServer()
        {
            NotificationManager handlerManager = new NotificationManager();
            this.Notifications = handlerManager.GetNotificationFromServer();
            if (this.Notifications.Count > 0)
            {
                return true;
            }
            return false;
        }
    /// <summary>
    /// show and create notification list to view
    /// </summary>
        private void CreatorNotifications()
        {
            if (this.Notifications.Count>0)
            {
                List<Notification> showNotif = Notifications;
                //check notification is not exported or remove it from list for show
                if (this.CreatedNotifications.Count > 0)
                {
                    foreach ( var notifItem in showNotif)
                    {
                        foreach (var createdNotification in CreatedNotifications)
                        {
                            if (createdNotification.name == notifItem.ID)
                            {
                                showNotif.Remove(notifItem);
                                break;
                            }
                        }
                    }   
                }
                // show list
                foreach (Resources.Notification.Notification notification in showNotif)
                {

                    this.CreatedNotifications.Add(Instantiate(this.NotificationTemplateObject.gameObject,
                        gameObject.transform));
                    GameObject Template = this.CreatedNotifications.Last();
                    if (Template.TryGetComponent(out NotificationTemplate T))
                    {
                        T.SetNotificationTemplate(notification.ID,notification.Type,notification.Title,notification.Description,notification.IconID);
                        Template.gameObject.name = notification.ID;
                    }
                }
                
            }
        }
    /// <summary>
    /// remove item from server and show list
    /// </summary>
    /// <returns></returns>
        public bool RemoverItemFromShowList(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                //check notification is not exported or remove it from list for show
                if (this.CreatedNotifications.Count > 0)
                {
                    foreach (var createdNotification in CreatedNotifications)
                        {
                            if (createdNotification.name == id)
                            {
                                CreatedNotifications.Remove(createdNotification);
                                if (createdNotification)
                                {
                                    Destroy(createdNotification);
                                }

                                return true;
                                break;
                            }
                        }
                    
                }
            }

            return false;
        }
    }
}