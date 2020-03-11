using Test.Infrasetup;
using Xunit;
namespace Test
{
    [TestCaseOrderer("Test.Infrasetup.PriorityOrderer", "test")]
    public class DBTest : IClassFixture<DataAccess>
    {
        private readonly DataAccess dal;
        public DBTest(DataAccess dataAccess)
        {
            dal = dataAccess;
        }

        #region table create test
        [Fact,TestPriority(1)]
        public void UserTableShouldBeCreated()
        {
            dal.CreateTable(dal.queries.SelectToken("UserTableScript").ToString());
            var actual = dal.IsTableCreated("user");
            Assert.True(actual);
        }

        [Fact, TestPriority(2)]
        public void UserProfileTableShouldBeCreated()
        {
            dal.CreateTable(dal.queries.SelectToken("UserProfileTableScript").ToString());
            var actual= dal.IsTableCreated("UserProfile");

            Assert.True(actual);
        }

        [Fact, TestPriority(3)]
        public void NewsTableShouldBeCreated()
        {
            dal.CreateTable(dal.queries.SelectToken("NewsTableScript").ToString());
            var actual = dal.IsTableCreated("news");
            Assert.True(actual);
        }

        [Fact, TestPriority(4)]
        public void ReminderTableShouldBeCreated()
        {
            dal.CreateTable(dal.queries.SelectToken("ReminderTableScript").ToString());
            var actual = dal.IsTableCreated("reminders");
            Assert.True(actual);
        }

        #endregion

        #region Insert data test

        [Fact, TestPriority(5)]
        public void UserDataShouldBeInserted()
        {
            string query = dal.queries.SelectToken("InsertScripts").SelectToken("User").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.True(actual > 0);
        }

        [Fact, TestPriority(6)]
        public void UserProfileDataShouldBeInserted()
        {
            string query = dal.queries.SelectToken("InsertScripts").SelectToken("UserProfile").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.True(actual > 0);
        }

        [Fact, TestPriority(7)]
        public void NewsDataShouldBeInserted()
        {
            string query = dal.queries.SelectToken("InsertScripts").SelectToken("News").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.True(actual > 0);
        }

        [Fact, TestPriority(8)]
        public void ReminderDataShouldBeInserted()
        {
            string query = dal.queries.SelectToken("InsertScripts").SelectToken("Reminder").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.True(actual > 0);
        }

        #endregion


        #region queries test

        //Q1 Retreive all the user profiles joined on or after 10-Dec-2019
        [Fact, TestPriority(9)]
        public void Query1ShouldReturnUserDetails()
        {
            var query = dal.queries.SelectToken("Query1").ToString();
            var actual = dal.GetTableData(query);

            Assert.True(actual.Count == 1);
            Assert.Equal("Johnson", actual[0][1].ToString());
        }

        //Q2 Retreive the details of user 'Jack' along with all the news created by him
        [Fact, TestPriority(10)]
        public void Query2ShouldReturnUserNewsDetails()
        {
            var query = dal.queries.SelectToken("Query2").ToString();
            var actual = dal.GetTableData(query);

            Assert.True(actual.Count == 2);
            Assert.Equal(102,(int)actual[1][6]);
            Assert.Equal("IT industry was facing disruptive changes in 2019. It is expected to have positive growth in 2020", actual[0][8].ToString());
        }

        //Q3 Retreive all details of the user who created the News with newsId=103
        [Fact, TestPriority(11)]
        public void Query3ShouldReturnUserDetails()
        {
            var query = dal.queries.SelectToken("Query3").ToString();
            var actual = dal.GetTableData(query);

            Assert.True(actual.Count == 1);
            Assert.Equal("John", actual[0][0].ToString());
            Assert.Equal("Johnson", actual[0][1].ToString());
        }

        //Q4 Retreive the details of all the users who have not added any news yet
        [Fact, TestPriority(12)]
        public void Query4ShouldReturnUserWithNoNewsDetails()
        {
            var query = dal.queries.SelectToken("Query4").ToString();
            var actual = dal.GetTableData(query);

            Assert.True(actual.Count == 1);
            Assert.Equal("Kevin", actual[0][0].ToString());
            Assert.Equal("Lloyd", actual[0][2].ToString());
        }

        //Q5 Retreive the newsid and title of all the news having some reminder
        [Fact, TestPriority(13)]
        public void Query5ShouldReturnNewsDetails()
        {
            var query = dal.queries.SelectToken("Query5").ToString();
            var actual = dal.GetTableData(query);

            Assert.True(actual.Count == 3);
            Assert.Equal(101, (int)actual[0][0]);
            Assert.Equal("IT industry in 2020", actual[0][1].ToString());
        }

        //Q6 Retreive the total number of news added by each user
        [Fact, TestPriority(14)]
        public void Query6ShouldReturnNewsCountByUser()
        {
            var query = dal.queries.SelectToken("Query6").ToString();
            var actual = dal.GetTableData(query);

            Assert.True(actual.Count == 3);
            Assert.Equal(2, (int)actual[0][1]);
            Assert.Equal(2, (int)actual[1][1]);
            Assert.Equal(0, (int)actual[2][1]);
        }

        //Q7 Update the contact number of userId='John' to '9192939495'.
        [Fact, TestPriority(15)]
        public void Query7ShouldUpdateContactNumber()
        {
            var query = dal.queries.SelectToken("Query7").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.Equal(1,actual);

            var updated = dal.GetTableData("Select Contact from UserProfile where UserId='John'");
            Assert.Equal("9192939495", updated[0][0].ToString());
        }

        //Q8 Update the title of the newsId=101 to 'IT industry growth can be seen in 2020'
        [Fact, TestPriority(16)]
        public void Query8ShouldUpdateNewsTitle()
        {
            var query = dal.queries.SelectToken("Query8").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.Equal(1, actual);

            var updated = dal.GetTableData("Select title from news where newsId=101");
            Assert.Equal("IT industry growth can be seen in 2020", updated[0][0]);
        }

        //Q9 Remove all the reminders belonging to the news created by Jack
        [Fact, TestPriority(17)]
        public void Query9ShouldDeleteRemindersForNewsByJack()
        {
            var query = dal.queries.SelectToken("Query9").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.Equal(2, actual);
        }
        
        #endregion

    }
}
