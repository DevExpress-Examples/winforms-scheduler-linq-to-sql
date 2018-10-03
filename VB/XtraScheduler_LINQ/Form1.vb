Imports DevExpress.XtraScheduler
Imports System

Namespace XtraScheduler_LINQ
    Partial Public Class Form1
        Inherits DevExpress.XtraBars.Ribbon.RibbonForm

        Private context As LINQDataContext

        Public Sub New()
            InitializeComponent()
            ribbonControl1.SelectedPage = LinqToSqlRibbonPage1
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Me.schedulerControl1.Start = Date.Today
            SuscribeSchedulerEvents()

            Me.context = New LINQDataContext()
            InitializeMappings()
            Me.schedulerStorage1.Appointments.DataSource = context.DBAppointments
            Me.schedulerStorage1.Resources.DataSource = context.DBResources
        End Sub
        Private Sub InitializeMappings()
            Dim aptStorage As AppointmentDataStorage = Me.schedulerStorage1.Appointments
            Dim resStorage As ResourceDataStorage = Me.schedulerStorage1.Resources

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
            AddHandler Me.schedulerStorage1.AppointmentsChanged, AddressOf OnSchedulerStorage1AppointmentsChanged
            AddHandler Me.schedulerStorage1.AppointmentsInserted, AddressOf OnSchedulerStorage1AppointmentsInserted
            AddHandler Me.schedulerStorage1.AppointmentDeleting, AddressOf OnSchedulerStorage1AppointmentDeleting
        End Sub
        Private Sub OnSchedulerStorage1AppointmentsInserted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
            For Each apt As Appointment In e.Objects
                Dim dbApt As DBAppointment = CType(apt.GetSourceObject(Me.schedulerStorage1), DBAppointment)
                Me.context.DBAppointments.InsertOnSubmit(dbApt)
                Me.context.SubmitChanges()
            Next apt
        End Sub
        Private Sub OnSchedulerStorage1AppointmentsChanged(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
            Me.context.SubmitChanges()
        End Sub
        Private Sub OnSchedulerStorage1AppointmentDeleting(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)
            Dim apt As Appointment = CType(e.Object, Appointment)
            Dim dbApt As DBAppointment = CType(apt.GetSourceObject(Me.schedulerStorage1), DBAppointment)
            If dbApt Is Nothing Then ' occurrence
                Return
            End If
            Me.context.DBAppointments.DeleteOnSubmit(dbApt)
            Me.context.SubmitChanges()
        End Sub

        Private Sub barBtnRefresh_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles barBtnRefresh.ItemClick
            Me.schedulerStorage1.Appointments.DataSource = Nothing
            context = New LINQDataContext()
            Me.schedulerStorage1.Appointments.DataSource = context.DBAppointments
        End Sub
    End Class
End Namespace
