using System.Collections.Generic;
using System.Threading.Tasks;
using Website001.API.Models;

namespace Website001.API.Data{
    public interface IReactionRepo{

        public Task addReactionToPost(int userId,int postId,int reactionId);
        public Task addReactionByNameToPost(int userId,int postId,string reactionName);
        public Task<int> numberOfPostReaction(int postId,int reactionId);
        public Task<int> numberOfReactions();
        public Task<bool> isUserReactedToPost(int userId,int postId);
        public Task<Reaction> getUserReactionToPost(int userId,int postId);
        public Task<bool> deleteUserReactionsOnPost(int userId,int postId);  
        public Task<List<Reaction>> getReactions();      
        public Task<bool> saveAll();
    }
}