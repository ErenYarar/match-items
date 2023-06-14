using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Notifications.Android;

public class BasicNotifications : MonoBehaviour
{
    /// <summary>
    /// Notification
    /// </summary>
    // [SerializeField] Button notificationOnButton;
    // [SerializeField] Button notificationOffButton;

    [SerializeField] Image notificationOff_img;
    [SerializeField] Image notificationOn_img;

    bool isNotifRunnig = false;

    void Start()
    {
        if (!PlayerPrefs.HasKey("notificationArea"))
        {
            PlayerPrefs.SetInt("notificationArea", 0);
            Load();
        }
        else
        {
            Load();
        }

        if (isNotifRunnig == true && HealthManager.instance.currentHealth == HealthManager.instance.maxHealth)
        {
            StartNotifications();
        }
        UpdateNotifButton();
    }

    void StartNotifications()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default, // Importance.High
            Description = "Generic notification",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification();
        notification.Title = "Match 3D"; //Oyun ismi eklenecek
        notification.Text = "Life is Full!"; 
        notification.SmallIcon = "icon";
        notification.LargeIcon = "logo";
        notification.ShowTimestamp = true;
        //Time Notification
        notification.FireTime = System.DateTime.Now.AddMinutes(30); //Sonradan degisilebilir zaman

        //Send Notification
        var identifier = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }

    public void OnNotificationButtonPress()
    {
        isNotifRunnig = true;
        Save();
        UpdateNotifButton();
    }

    public void OffNotificationButtonPress()
    {
        isNotifRunnig = false;
        Save();
        UpdateNotifButton();
    }

    private void UpdateNotifButton()
    {
        if (isNotifRunnig == true)
        {
            notificationOn_img.gameObject.SetActive(true);
            notificationOff_img.gameObject.SetActive(false);
        }
        else
        {
            notificationOn_img.gameObject.SetActive(false);
            notificationOff_img.gameObject.SetActive(true);
        }
    }

    void Load()
    {
        isNotifRunnig = PlayerPrefs.GetInt("notificationArea") == 1;
    }

    void Save()
    {
        PlayerPrefs.SetInt("notificationArea", isNotifRunnig ? 1 : 0);
    }

}
