using DevExpress.XtraScheduler;
using System;

namespace XtraScheduler_LINQ {
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm {
        LINQDataContext context;

        public Form1() {
            InitializeComponent();
            ribbonControl1.SelectedPage = LinqToSqlRibbonPage1;
        }

        void Form1_Load(object sender, EventArgs e) {
            this.schedulerControl1.Start = DateTime.Today;
            SuscribeSchedulerEvents();
            
            this.context = new LINQDataContext();            
            InitializeMappings();
            this.schedulerStorage1.Appointments.DataSource = context.DBAppointments;
            this.schedulerStorage1.Resources.DataSource = context.DBResources;
        }
        void InitializeMappings() {
            AppointmentDataStorage aptStorage = this.schedulerStorage1.Appointments;
            ResourceDataStorage resStorage = this.schedulerStorage1.Resources;

            aptStorage.Mappings.AllDay = "AllDay";
            aptStorage.Mappings.Description = "Description";
            aptStorage.Mappings.End = "EndTime";
            aptStorage.Mappings.Label = "Label";
            aptStorage.Mappings.Location = "Location";
            aptStorage.Mappings.RecurrenceInfo = "RecurrenceInfo";
            aptStorage.Mappings.ReminderInfo = "ReminderInfo";
            aptStorage.Mappings.ResourceId = "CarIdShared";
            aptStorage.Mappings.Start = "StartTime";
            aptStorage.Mappings.Status = "Status";
            aptStorage.Mappings.Subject = "Subject";
            aptStorage.Mappings.Type = "EventType";

            resStorage.Mappings.Id = "ID";
            resStorage.Mappings.Caption = "Model";
            resStorage.Mappings.Image = "Picture";
        }
        void SuscribeSchedulerEvents() {
            this.schedulerStorage1.AppointmentsChanged += OnSchedulerStorage1AppointmentsChanged;
            this.schedulerStorage1.AppointmentsInserted += OnSchedulerStorage1AppointmentsInserted;
            this.schedulerStorage1.AppointmentDeleting += OnSchedulerStorage1AppointmentDeleting;
        }
        void OnSchedulerStorage1AppointmentsInserted(object sender, PersistentObjectsEventArgs e) {
            foreach (Appointment apt in e.Objects) {
                DBAppointment dbApt = (DBAppointment)apt.GetSourceObject(this.schedulerStorage1);
                this.context.DBAppointments.InsertOnSubmit(dbApt);
                this.context.SubmitChanges();
            }
        }
        void OnSchedulerStorage1AppointmentsChanged(object sender, PersistentObjectsEventArgs e) {
            this.context.SubmitChanges();
        }
        void OnSchedulerStorage1AppointmentDeleting(object sender, PersistentObjectCancelEventArgs e) {
            Appointment apt = (Appointment)e.Object;
            DBAppointment dbApt = (DBAppointment)apt.GetSourceObject(this.schedulerStorage1);
            if (dbApt == null) // occurrence
                return;
            this.context.DBAppointments.DeleteOnSubmit(dbApt);
            this.context.SubmitChanges();
        }

        private void barBtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.schedulerStorage1.Appointments.DataSource = null;
            context = new LINQDataContext();
            this.schedulerStorage1.Appointments.DataSource = context.DBAppointments;
        }
    }
}
