using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter_Clone.Models
{
    public class UserFollowing
    {
        public Person user  { get;set; }
        public List<Person> FollowingLst { get; set; }
}
}