Before running the project, complete the following steps:-

1.  Load the 'RBAC_Schema.sql' file contained in the DatabaseSchema folder into 'Sql Server Management Studio' (SSMS) or equivalent and execute the script to create the database and corresponding tables.

2.  Update the USERS table manually and update the created record's 'Username' field from 'somedomain\admin' with your windows equivalent username.  If you are unsure of the username, run the Visual Studio project and your windows username will be displayed on the Home page with instructions.

3.  Load the project into Visual Studio and run.

NOTE: Make sure you have your project configured for 'Windows Authentication' when integrating RBAC into your own project.  

Enjoy...
