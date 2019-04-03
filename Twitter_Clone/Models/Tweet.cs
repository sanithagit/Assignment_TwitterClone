using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Twitter_Clone.Models
{
    public class Tweet
    {
        [Key]
        public int Tweet_id { get; set; }

        [DisplayName("User")]
        public string User_Id { get; set; }
        public string Message { get; set; }

        [DisplayName("Date")]
        public DateTime Created { get; set; }

        public Person User { get; set; }

        public ICollection<Tweet> TweetList { get; set; }

    }
}