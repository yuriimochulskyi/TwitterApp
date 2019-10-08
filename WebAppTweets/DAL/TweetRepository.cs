using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebAppTweets.Models;
using WebAppTweets.Models.DAL;

namespace WebAppTweets.DAL
{
    public class TweetRepository : ITweetRepository
    {

        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public void DeleteTweet(int tweetId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute(@"DELETE FROM Tweet WHERE StatusID = @StatusID",
                 new { StatusID = tweetId });
            }
        }

        public Tweet GetSingleTweet(int tweetId)
        {
            Tweet tweet = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                tweet = db.Query<Tweet>("SELECT StatusID, ScreenName, Text, CreatedAt, RTCount  FROM Tweet" +
                " WHERE StatusID =@StatusID", new { StatusID = tweetId }).SingleOrDefault();
            }
            return tweet;
        }

        public List<Tweet> GetTweets(int amount, string sort)
        {
            List<Tweet> tweets = new List<Tweet>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                tweets = db.Query<Tweet>("SELECT TOP " + amount + " StatusID, ScreenName, Text, CreatedAt, RTCount FROM Tweet ORDER BY CreatedAt " + sort).ToList();
            }
            return tweets;
        }

        public void InsertTweet(Tweet ourTweet)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute(@"INSERT Tweet(StatusID, ScreenName, Text, CreatedAt, RTCount) values (@StatusID, @ScreenName, @Text, @CreatedAt, @RTCount)",
                new { ourTweet.StatusID, ourTweet.ScreenName, ourTweet.Text, ourTweet.CreatedAt, ourTweet.RTCount});
            }
        }

        public void UpdateTweet(Tweet ourTweet)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("UPDATE Tweet SET StatusID = @StatusID, ScreenName = @ScreenName, Text = @Text," +
                    " CreatedAt = @CreatedAt, RTCount = @RTCount WHERE StatusID =" + ourTweet.StatusID, ourTweet);
            }

        }
    }
}