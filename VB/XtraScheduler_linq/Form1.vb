Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler

Namespace XtraScheduler_linq
	Partial Public Class Form1
		Inherits Form
		Private context As LINQDataContext

		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			SuscribeSchedulerEvents()

			Me.context = New LINQDataContext()
			Dim appointmentList As New DBAppointmentList()
			appointmentList.AddRange(Me.context.DBAppointments.ToArray())

			Dim resourceList As New List(Of DBResource)()
			resourceList.AddRange(Me.context.DBResources.ToArray())

			InitializeMappings()

			Me.schedulerStorage1.Appointments.DataSource = appointmentList
			Me.schedulerStorage1.Resources.DataSource = resourceList
		End Sub
		Private Sub InitializeMappings()
			Dim aptStorage As AppointmentStorage = Me.schedulerStorage1.Appointments
			Dim resStorage As ResourceStorage = Me.schedulerStorage1.Resources

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
	End Class

	Friend Class DBAppointmentList
		Inherits List(Of DBAppointment)
		Implements IBindingList
		#Region "IBindingList Members"
		Public Event ListChanged As ListChangedEventHandler Implements IBindingList.ListChanged

		Public Sub AddIndex(ByVal [property] As PropertyDescriptor) Implements IBindingList.AddIndex
		End Sub
		Public Function AddNew() As Object Implements IBindingList.AddNew
			Dim apt As New DBAppointment()
			Me.Add(apt)
			Return apt
		End Function
		Public ReadOnly Property AllowEdit() As Boolean Implements IBindingList.AllowEdit
			Get
				Return True
			End Get
		End Property
		Public ReadOnly Property AllowNew() As Boolean Implements IBindingList.AllowNew
			Get
				Return True
			End Get
		End Property
		Public ReadOnly Property AllowRemove() As Boolean Implements IBindingList.AllowRemove
			Get
				Return True
			End Get
		End Property
		Public Sub ApplySort(ByVal [property] As PropertyDescriptor, ByVal direction As ListSortDirection) Implements IBindingList.ApplySort
		End Sub
		Public Overloads Function Find(ByVal [property] As PropertyDescriptor, ByVal key As Object) As Integer Implements IBindingList.Find
			Return -1
		End Function
		Public ReadOnly Property IsSorted() As Boolean Implements IBindingList.IsSorted
			Get
				Return False
			End Get
		End Property
		Public Sub RemoveIndex(ByVal [property] As PropertyDescriptor) Implements IBindingList.RemoveIndex
		End Sub
		Public Sub RemoveSort() Implements IBindingList.RemoveSort
		End Sub
		Public ReadOnly Property SortDirection() As ListSortDirection Implements IBindingList.SortDirection
			Get
				Return ListSortDirection.Ascending
			End Get
		End Property
		Public ReadOnly Property SortProperty() As PropertyDescriptor Implements IBindingList.SortProperty
			Get
				Return Nothing
			End Get
		End Property
		Public ReadOnly Property SupportsChangeNotification() As Boolean Implements IBindingList.SupportsChangeNotification
			Get
				Return False
			End Get
		End Property
		Public ReadOnly Property SupportsSearching() As Boolean Implements IBindingList.SupportsSearching
			Get
				Return False
			End Get
		End Property
		Public ReadOnly Property SupportsSorting() As Boolean Implements IBindingList.SupportsSorting
			Get
				Return False
			End Get
		End Property
		#End Region
	End Class
End Namespace
