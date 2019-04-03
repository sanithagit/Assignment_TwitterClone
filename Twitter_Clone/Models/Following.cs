using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Twitter_Clone.Models
{
    public class Following
    {
        [ForeignKey("User")]
        public string User_Id { get; set; }

        [ForeignKey("Follower")]
        public string Following_Id { get; set; }

        public ICollection<Person> UserList { get; set; }

        public virtual Person User { get; set; }

        public virtual Person Follower { get; set; }

    }
}