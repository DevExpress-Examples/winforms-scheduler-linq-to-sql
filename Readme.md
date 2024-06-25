<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128633875/12.2.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E823)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/XtraScheduler_linq/Form1.cs) (VB: [Form1.vb](./VB/XtraScheduler_linq/Form1.vb))
* [Program.cs](./CS/XtraScheduler_linq/Program.cs) (VB: [Program.vb](./VB/XtraScheduler_linq/Program.vb))
<!-- default file list end -->
# How to bind XtraScheduler to data using LINQ to SQL


<p>This example demonstrates how to bind the Scheduler to SQL server using LINQ to SQL data model.<br>Note that the Scheduler control does not automatically respond to data modifications in the LINQ data source. The reason for this behavior lies in the fact that the scheduler is bound to a copy of data provided by the LINQDataContext. The LINQDataContext does not provide a list change notification mechanism that can be used by a scheduler. So after the initial data binding the scheduler can only trace changes originated from the actions performed by the scheduler itself. Click the <strong>Refresh</strong> button to reload data and display changes of the underlying data source.</p>
<p>To run the project, you need a database at the local SQL server. The script used to create the XtraCars database is included in the XtraCars.sql file. Create the database and change the connection string in the app.config file, if necessary.</p>

<br/>


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-scheduler-linq-to-sql&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-scheduler-linq-to-sql&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
