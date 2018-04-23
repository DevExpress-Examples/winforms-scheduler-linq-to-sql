using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;

namespace XtraScheduler_linq {
    public partial class Form1 : Form {
        LINQDataContext context;

        public Form1() {
            InitializeComponent();
        }
        void Form1_Load(object sender, EventArgs e) {
            SuscribeSchedulerEvents();
            
            this.context = new LINQDataContext();
            DBAppointmentList appointmentList = new DBAppointmentList();
            appointmentList.AddRange(this.context.DBAppointments.ToArray());

            List<DBResource> resourceList = new List<DBResource>();
            resourceList.AddRange(this.context.DBResources.ToArray());
            
            InitializeMappings();

            this.schedulerStorage1.Appointments.DataSource = appointmentList;
            this.schedulerStorage1.Resources.DataSource = resourceList;
        }
        void InitializeMappings() {
            AppointmentStorage aptStorage = this.schedulerStorage1.Appointments;
            ResourceStorage resStorage = this.schedulerStorage1.Resources;

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
    }

    class DBAppointmentList : List<DBAppointment>, IBindingList {
        #region IBindingList Members
        public event ListChangedEventHandler ListChanged;

        public void AddIndex(PropertyDescriptor property) {
        }
        public object AddNew() {
            DBAppointment apt = new DBAppointment();
            this.Add(apt);
            return apt;
        }
        public bool AllowEdit {
            get { return true; }
        }
        public bool AllowNew {
            get { return true; }
        }
        public bool AllowRemove {
            get { return true; }
        }
        public void ApplySort(PropertyDescriptor property, ListSortDirection direction) {
        }
        public int Find(PropertyDescriptor property, object key) {
            return -1;
        }
        public bool IsSorted {
            get { return false; }
        }
        public void RemoveIndex(PropertyDescriptor property) {
        }
        public void RemoveSort() {
        }
        public ListSortDirection SortDirection {
            get { return ListSortDirection.Ascending; }
        }
        public PropertyDescriptor SortProperty {
            get { return null; }
        }
        public bool SupportsChangeNotification {
            get { return false; }
        }
        public bool SupportsSearching {
            get { return false; }
        }
        public bool SupportsSorting {
            get { return false; }
        }
        #endregion
    }
}
