# Seed code - Boilerplate for step-0 News App Assignment

## Assignment Step Description

Read the given set of questions and solve them by writing queries using SQL Server.

The estimated effort to complete this assignment is 2 - 3 hours

### Things to do

- Fork this boilerplate.
- Clone the forked boilerplate.
- Create sql queries based on the problem statement and expected solution.
- Write all queries in the given queries.json file Without modifying the Sequence and key names.
- Ensure all test cases pass locally.
- Push the changes into your repository.
- Submit the assignment in Hobbes for evaluation and share score with your mentor.

`Update the connection string in config.json file as per your system.`

### Problem Statement

News-app is used to explore and read the news. It is also used to manage news in your watchlist to read later.
Create the necessary DB schema (SQL Server) including tables, relationships and add sample data into each table.

### Expected solution

- The tables for News, UserProfile, User and Reminders should get created, as per the Database diagram image. 
- The records for these tables should get inserted as per the snapshot shared with the boilerplate.
- Design query statements as per the requirements given below

### Database diagram

![Refer to Database diagram inside images](./images/Database_Diagram.jpg)

**Insert the rows into the created tables as per attached snapshot(SampleData.pdf)**

### Write sql statements to carry out the below listed operations

1. Retreive all the user profiles joined on or after 10-Dec-2019.
2. Retreive the details of user 'Jack' along with all the news created by him.
3. Retreive all details of the user who created the News with newsId=103.
4. Retreive the details of all the users who have not added any news yet.
5. Retreive the newsid and title of all the news having some reminder.
6. Retreive the total number of news added by each user.
7. Update the contact number of userId='John' to '9192939495'.
8. Update the title of the newsId=101 to 'IT industry growth can be seen in 2020'.
9. Remove all the reminders belonging to the news created by Jack.

### Note

1. Although this assignment is based on RDBMS, for automated review it is designed using .NET Core, hence ensure .NET Core SDK 3.1 is installed.
2. Successful execution of queries as well as Successful execution of testcases (both locally and on hobbes) are mandatory to accept the solution of this assignment.