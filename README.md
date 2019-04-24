# PetShop
Database Design Final Project that uses a PostgreSQL database and a Windows Presentation Foundation (WPF) GUI in C# 
to mimic a simple Pet Shop where a user can create an account to either put pets up for sale or purchase a new pet.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

### Prerequisites

* A Windows machine
* Visual Studio 2012 or newer
* Pgadmin 4
* Stack Builder (Choose to install it when installing Pgadmin)

### Installing

A step by step series of examples that tell you how to get a development environment running

#### Step 1: Installing Visual Studio
Install Visual Studio like you would for any other IDE and with your preferred settings [here](https://visualstudio.microsoft.com)

#### Step 2: Installing Pgadmin 4
Download and install the latest version Pgadmin from [here](https://www.pgadmin.org)
When installing, choose port 5432 as your port, and make a password for the application. Remember to change the
password in the application to this password in PostgreSQL.cs 
When asked if you'd like to install Stack Builder select yes.
Pgadmin will then install and a new installer will open for Stack Builder

#### Step 3: Installing Stack Builder
Select the first server option in the list of servers (Possibly: PostgreSQL v# (x64) on port 5432) and then click next.
Open the Databse Drivers tab under Categories and check the Npgsql v# option then click next to finish installing.
If you are asked for a password use the same password used to install Pgadmin

#### Step 4: Adding Npgsql to the Project
Once you have the project open in Visual Studio, navigate to the References tab in the Solution Explorer. 
Right click and select "Add Reference"
Click on the Browse tab to the left and then Browse again at the bottom of the window
Search for Npgsql.dll in your file explorer. It is mostlikely in your C:\ProgramFiles(x86)\PostgreSQL\Npgsql\bin\Npgsql.dll path
Select it and then add it to your references. The NpgsqlConnection classes should now be resolved in PostgreSQL.cs

#### Step 5: Setting up Pgadmin 4
Right click Servers on the left side of the screen and then click new Server

```
Name: Anything you'd like
Host name / Address: localhost
Port: 5432
Username: postgres
Maintenance DB: postgres
Password: The same password used to install Pgadmin and Stack Builder
```
Then create the new server

Under public create 4 new tables: pets, users, shoppers, and sellers. Use the CreateTable.txt file to follow the schema for each of the relations

#### Step 6: Putting it all together

In the command line use the command: psql -U postgres postgres to log into your server
use a 

```
SELECT * FROM pets; 
```

query or something similar to ensure that the tables are saved and being read properly. If it is everything is set up!


#### Step 7: Incrementing Keys
To ensure that all keys are being incremented propely use the following commands in the command line for each key in the tables

```
CREATE sequence <sequence name> OWNED BY <table name>.<column name>;
ALTER TABLE <table name> ALTER COLUMN <column name> SET DEFAULT nextval('<sequence name>');
commit;
```
the sequence name needs to be different for each sequence in the tables.
Once this step is complete you're all set.

## Running the Application

Run the application from Visual Studio using the green play button labled Start at the top of the IDE. Create an account as 
a Seller and add some pets to the database! If you immediately create an account as a Shopper there will be no pets in the database 
to view and add to your cart.

## Authors
* Ashley Benjamin-Shallow
* Jelina Ramos Perez
* Rahul Kant

