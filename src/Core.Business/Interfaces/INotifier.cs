﻿using System.Collections.Generic;
using Core.Business.Notifications;

namespace Core.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotification(); 
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}