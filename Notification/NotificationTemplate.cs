using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Notification
{
    public class NotificationTemplate:MonoBehaviour
    {
        public Image BackgroundImage { set; get; }
        public Text TitleText { set; get; }
        public TextMeshPro TitleTextMeshPro { set; get; }
        public Text InfoText { set; get; }
        public TextMeshPro InfoTextMeshPro { set; get; }
        public Image IconImage { set; get; }
        public Text IDText { set; get; }
        public NotificationType Type { set; get; }
        public string ID { set; get; }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public NotificationTemplate()
        {
            
        }
        public NotificationTemplate(string id , NotificationType type,string title,string description, string icon="")
        {
            if (!string.IsNullOrEmpty(id))
            {
                //set id
                this.SetID(id);
                //change color with type
                this.ColorTypeMatch(type);
                //change title
                this.SetTitle(title);
                //change Description
                this.SetDescription(description);
                //change icon
                

            }
            else
            {
                //show warning in log
                Debug.Log("[Notification] : This system can not show  notification without ID ");
            }
            
        }
        public void SetNotificationTemplate(string id , NotificationType type,string title,string description, string icon="")
        {
            if (!string.IsNullOrEmpty(id))
            {
                //set id
                this.SetID(id);
                //change color with type
                this.ColorTypeMatch(type);
                //change title
                this.SetTitle(title);
                //change Description
                this.SetDescription(description);
                //change icon
                

            }
            else
            {
                //show warning in log
                Debug.Log("[Notification] : This system can not show  notification without ID ");
            }
            
        }

        public void SetID(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                this.ID = id;
                if (this.IDText)
                {
                    this.IDText.text = id;
                    this.IDText.gameObject.SetActive(false);
                }
            }
        }
        public void SetIcon(string iconCode)
        {
            
        }
        public void SetTitle(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                if (this.TitleText)
                {
                    this.TitleText.text = title;
                }

                if (this.TitleTextMeshPro)
                {
                    this.TitleTextMeshPro.text = title;
                }
            }
        }
        public void SetDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                if (this.InfoText)
                {
                    this.InfoText.text = description;
                }

                if (this.InfoTextMeshPro)
                {
                    this.InfoTextMeshPro.text = description;
                }
            }
        }
        public void ColorTypeMatch(NotificationType type)
        {
            if (this.BackgroundImage)
            {
                this.Type = type;
                float alphaColorTemplate = this.BackgroundImage.color.a;
                switch (type)
                {
                    case NotificationType.Normal : 
                        this.BackgroundImage.color = new Color(Color.black.r,Color.black.g,Color.black.b,alphaColorTemplate);
                        break;
                    case NotificationType.Danger:
                        this.BackgroundImage.color = new Color(Color.red.r,Color.red.g,Color.red.b,alphaColorTemplate);
                        break;
                    case NotificationType.Important:
                        this.BackgroundImage.color = new Color(Color.yellow.r,Color.yellow.g,Color.yellow.b,alphaColorTemplate);
                        break;
                    case NotificationType.VeryImportant:
                        this.BackgroundImage.color = new Color(Color.yellow.r,Color.yellow.g,Color.red.b,alphaColorTemplate);
                        break;
                    default:
                        this.BackgroundImage.color = new Color(Color.green.r,Color.green.g,Color.green.b,alphaColorTemplate);
                        break;

                }

                // return true;
            }

            // return false;
        }
    }
}