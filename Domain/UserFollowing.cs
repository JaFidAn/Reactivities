using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class UserFollowing
    {
        public string  ObserverId { get; set; }   //FollowerId
        public AppUser Observer { get; set; }    //Follower
        public string TargetId { get; set; }    // Following Id (following by Follower)
        public AppUser Target { get; set; }    // Following User
    }
}