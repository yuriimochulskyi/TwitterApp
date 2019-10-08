using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTweets.Models.DAL
{
    internal interface ITweetRepository
    {
        
        List<Tweet> GetTweets(int amount, string sort);

        Tweet GetSingleTweet(int tweetId);

        void InsertTweet(Tweet ourTweet);

        void DeleteTweet(int tweetId);

        void UpdateTweet(Tweet ourTweet);
    }
}
