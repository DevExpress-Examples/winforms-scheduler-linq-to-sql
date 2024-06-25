<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128633875/18.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E823)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# WinForms Scheduler - Bind to data using LINQ to SQL

This example demonstrates how to bind the WinForms Scheduler to a SQL server using the LINQ to SQL data model.

The Scheduler control does not automatically respond to data modifications in the LINQ data source because the Scheduler control is bound to a copy of data supplied by `LINQDataContext`. The `LINQDataContext` does not implement a list change notification mechanism. The Scheduler control can only trace changes originated from the actions performed by the Scheduler itself. Click the **Refresh** button to reload data.

To run the example, you need a database on the local SQL server. The script used to create the XtraCars database is included in the *XtraCars.sql* file. Create the database and change the connection string in the *app.config* file if needed.


## Documentation

* [Binding to LINQ to SQL Classes](https://docs.devexpress.com/WindowsForms/4057/common-features/data-binding/binding-to-linq-to-sql-classes)
* [Data Sources - WinForms Scheduler](https://docs.devexpress.com/WindowsForms/3289/controls-and-libraries/scheduler/data-binding/data-sources)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-scheduler-linq-to-sql&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-scheduler-linq-to-sql&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
