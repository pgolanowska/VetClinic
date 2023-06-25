using System;
using System.Collections.Generic;
using System.Text;
using Windows.Data.Xml.Dom;
using VetClinic.CompanionApp.Models.Appointment;
using Windows.UI.Notifications;
using System.Diagnostics;

namespace VetClinic.CompanionApp.BusinessLogic
{
    public class AppointmentManager
    {
        public void ScheduleAppointmentReminder(AppointmentModel appointment)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            XmlNodeList toastNodeList = toastXml.GetElementsByTagName("text");
            toastNodeList.Item(0).AppendChild(toastXml.CreateTextNode("VetClinic appointment tomorrow"));
            toastNodeList.Item(1).AppendChild(toastXml.CreateTextNode("Don't forget to bring your pet!"));

            DateTime dueTime = appointment.AppointmentDateTime.AddDays(-1);
            if(dueTime < DateTime.Now) { dueTime = DateTime.Now.AddMinutes(2); }

            ScheduledToastNotification scheduledToast = new ScheduledToastNotification(toastXml, dueTime);

            ToastNotificationManager.CreateToastNotifier().AddToSchedule(scheduledToast);

        }

    }
}
