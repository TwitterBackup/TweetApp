using AutoMapper;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.Models;
using TwitterBackup.Web.Models.TweetViewModels;

namespace TwitterBackup.Web.Properties
{
    public class MappingSettings : Profile
    {
        public MappingSettings()
        {
            this.CreateMap<UserTweet, TweetDto>()
                .ForMember(destination => destination.RetweetCount, options => options.MapFrom(source => source.Tweet.RetweetCount))
                .ForMember(destination => destination.FavoriteCount, options => options.MapFrom(source => source.Tweet.FavoriteCount))
                .ForMember(destination => destination.QuoteCount, options => options.MapFrom(source => source.Tweet.QuoteCount))
                .ForMember(destination => destination.CreatedAt, options => options.MapFrom(source => source.Tweet.CreatedAt))
                .ForMember(destination => destination.Text, options => options.MapFrom(source => source.Tweet.Text))
                .ForMember(destination => destination.Language, options => options.MapFrom(source => source.Tweet.Language))
                .ForMember(destination => destination.TweeterName, options => options.MapFrom(source => source.Tweet.Tweeter.Name));

            this.CreateMap<TweetDto, TweetViewModel>()
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.User.UserName));

            this.CreateMap<TweetDto, EditTweetViewModel>()
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.User.UserName));

        }
    }
}