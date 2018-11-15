<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/XtraScheduler_linq/Form1.cs) (VB: [Form1.vb](./VB/XtraScheduler_linq/Form1.vb))
* [Program.cs](./CS/XtraScheduler_linq/Program.cs) (VB: [Program.vb](./VB/XtraScheduler_linq/Program.vb))
<!-- default file list end -->
# How to bind XtraScheduler to data using LINQ to SQL


<p>This example demonstrates how to bind the Scheduler to SQL server using LINQ to SQL data model.<br>Note that the Scheduler control does not automatically respond to data modifications in the LINQ data source. The reason for this behavior lies in the fact that the scheduler is bound to a copy of data provided by the LINQDataContext. The LINQDataContext does not provide a list change notification mechanism that can be used by a scheduler. So after the initial data binding the scheduler can only trace changes originated from the actions performed by the scheduler itself. Click the <strong>Refresh</strong> button to reload data and display changes of the underlying data source.</p>
<p>To run the project, you need a database at the local SQL server. The script used to create the XtraCars database is included in the XtraCars.sql file. Create the database and change the connection string in the app.config file, if necessary.</p>

<br/>


