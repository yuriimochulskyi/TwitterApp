using System;
using LinqToTwitter;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppTweets.Models;
using System.Threading.Tasks;
using WebAppTweets.DAL;
using System.Data.SqlClient;
using System.Configuration;

namespace WebAppTweets.Controllers
{
    public class TweetController : ApiController
    {
        private TweetRepository _ourTweetRepository;

        public TweetController()
        {
            _ourTweetRepository = new TweetRepository();
        }

        [HttpGet]
        [Route("Tweets")]
        public async Task<List<Tweet>> Get()
        {
            var auth = new ApplicationOnlyAuthorizer()
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"],
                },
            };
            await auth.AuthorizeAsync();

            var twitterContext = new TwitterContext(auth);

            
            var tweets =
                    await (from tweet in twitterContext.Status
                           where tweet.Type == StatusType.User &&
                             tweet.ScreenName == "Yurii79584584" &&
                             tweet.Count == 100 &&
                             tweet.IncludeRetweets == true &&
                             tweet.ExcludeReplies == true
                           select new Tweet
                           {
                               StatusID = tweet.StatusID,
                               ScreenName = tweet.User.ScreenNameResponse,
                               Text = tweet.Text,
                               CurrentUserRetweet = tweet.CurrentUserRetweet,
                               CreatedAt = tweet.CreatedAt
                           }).ToListAsync();

            foreach (var tweet in tweets)
            {
                try
                {
                    _ourTweetRepository.InsertTweet(tweet);
                }
                catch (SqlException ex)
                {

                }
                
            }
            
            return tweets;
        }
        
        [HttpGet]
        [Route("TweetsDB")]
        public List<Tweet> GetFromDB()
        {
            return _ourTweetRepository.GetTweets(100, "ASC");
        }

        
    }
}
