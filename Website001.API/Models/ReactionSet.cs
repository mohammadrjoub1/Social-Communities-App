using System;
using System.Text.Json.Serialization;

namespace Website001.API.Models{
    public class ReactionSet{
    public int id{set;get;}
    public int postId{set;get;}
    public int userId{set;get;}

    public int reactionId{set;get;}
    public DateTime date{set;get;}
     [JsonIgnore]
    public Reaction reaction{set;get;}
     [JsonIgnore]
    public User user{set;get;}

    }
}