using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using VetClinic.CompanionApp.Models.Appointment;
using VetClinic.CompanionApp.Models.Pet;
using VetClinic.CompanionApp.Services;
using Xamarin.Forms;

namespace VetClinic.CompanionApp.ViewModels.Appointment
{
    public class DoctorDetailsViewModel : BaseViewModel
    {
        public AppointmentDataStore AppointmentDataStore => DependencyService.Get<AppointmentDataStore>();
        private ScheduleModel _schedule;
        public ScheduleModel Schedule
        {
            get { return _schedule; }
            set { SetProperty(ref _schedule, value); }
        }
        private EmployeeModel _employee;
        public EmployeeModel Employee
        {
            get { return _employee; }
            set { SetProperty(ref _employee, value); }
        }
        public DoctorDetailsViewModel()
        {
            Schedule = DependencyService.Get<ScheduleModel>();
            Initialize();
        }

        public async Task Initialize()
        {
            Employee = await AppointmentDataStore.GetEmployeeDetails(Schedule.EmployeeId);
        }
    }
}
