using System;

namespace WebAppTweets.Models
{
    public class Tweet
    {
        public decimal StatusID { get; set; }

        public string ScreenName { get; set; }

        public string Text { get; set; }

        public string Date { get { return CreatedAt.ToString("f"); } }

        public string RTCount { get { return CurrentUserRetweet == 0 ? string.Empty : CurrentUserRetweet + " RT"; } }


        public DateTime CreatedAt
        {
            get;
            set;
        }

        public ulong CurrentUserRetweet
        {
            get;
            set;
        }
    }
}