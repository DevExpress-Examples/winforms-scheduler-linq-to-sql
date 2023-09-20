Imports DevExpress.XtraScheduler
Imports System

Namespace XtraScheduler_LINQ

    Public Partial Class Form1
        Inherits DevExpress.XtraBars.Ribbon.RibbonForm

        Private context As LINQDataContext

        Public Sub New()
            InitializeComponent()
            ribbonControl1.SelectedPage = LinqToSqlRibbonPage1
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            schedulerControl1.Start = Date.Today
            SuscribeSchedulerEvents()
            context = New LINQDataContext()
            InitializeMappings()
            schedulerStorage1.Appointments.DataSource = context.DBAppointments
            schedulerStorage1.Resources.DataSource = context.DBResources
        End Sub

        Private Sub InitializeMappings()
            Dim aptStorage As AppointmentDataStorage = schedulerStorage1.Appointments
            Dim resStorage As ResourceDataStorage = schedulerStorage1.Resources
            aptStorage.Mappings.AllDay = "AllDay"
            aptStorage.Mappings.Description = "Description"
            aptStorage.Mappings.End = "EndTime"
            aptStorage.Mappings.Label = "Label"
            aptStorage.Mappings.Location = "Location"
            aptStorage.Mappings.RecurrenceInfo = "RecurrenceInfo"
            aptStorage.Mappings.ReminderInfo = "ReminderInfo"
            aptStorage.Mappings.ResourceId = "CarIdShared"
            aptStorage.Mappings.Start = "StartTime"
            aptStorage.Mappings.Status = "Status"
            aptStorage.Mappings.Subject = "Subject"
            aptStorage.Mappings.Type = "EventType"
            resStorage.Mappings.Id = "ID"
            resStorage.Mappings.Caption = "Model"
            resStorage.Mappings.Image = "Picture"
        End Sub

        Private Sub SuscribeSchedulerEvents()
            AddHandler schedulerStorage1.AppointmentsChanged, AddressOf OnSchedulerStorage1AppointmentsChanged
            AddHandler schedulerStorage1.AppointmentsInserted, AddressOf OnSchedulerStorage1AppointmentsInserted
            AddHandler schedulerStorage1.AppointmentDeleting, AddressOf OnSchedulerStorage1AppointmentDeleting
        End Sub

        Private Sub OnSchedulerStorage1AppointmentsInserted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
            For Each apt As Appointment In e.Objects
                Dim dbApt As DBAppointment = CType(apt.GetSourceObject(schedulerStorage1), DBAppointment)
                context.DBAppointments.InsertOnSubmit(dbApt)
                context.SubmitChanges()
            Next
        End Sub

        Private Sub OnSchedulerStorage1AppointmentsChanged(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
            context.SubmitChanges()
        End Sub

        Private Sub OnSchedulerStorage1AppointmentDeleting(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)
            Dim apt As Appointment = CType(e.Object, Appointment)
            Dim dbApt As DBAppointment = CType(apt.GetSourceObject(schedulerStorage1), DBAppointment)
            If dbApt Is Nothing Then Return ' occurrence
            context.DBAppointments.DeleteOnSubmit(dbApt)
            context.SubmitChanges()
        End Sub

        Private Sub barBtnRefresh_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
            schedulerStorage1.Appointments.DataSource = Nothing
            context = New LINQDataContext()
            schedulerStorage1.Appointments.DataSource = context.DBAppointments
        End Sub
    End Class
End Namespace
