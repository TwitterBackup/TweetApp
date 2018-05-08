using AutoMapper;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.Models;
using TwitterBackup.Web.Models.TweeterViewModels;
using TwitterBackup.Web.Models.TweetViewModels;

namespace TwitterBackup.Infrastructure.Providers
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
                .ForMember(destination => destination.TweeterName, options => options.MapFrom(source => source.Tweet.Tweeter.Name))
                .ForMember(destination => destination.Tweeter, options => options.MapFrom(source => source.Tweet.Tweeter));

            this.CreateMap<UserTweeter, TweeterDto>()
                .ForMember(destination => destination.CreatedAt, options => options.MapFrom(source => source.Tweeter.CreatedAt))
                .ForMember(destination => destination.Name, options => options.MapFrom(source => source.Tweeter.Name))
                .ForMember(destination => destination.ScreenName, options => options.MapFrom(source => source.Tweeter.ScreenName))
                .ForMember(destination => destination.Description, options => options.MapFrom(source => source.Tweeter.CreatedAt))
                .ForMember(destination => destination.FollowersCount, options => options.MapFrom(source => source.Tweeter.FollowersCount))
                .ForMember(destination => destination.FriendsCount, options => options.MapFrom(source => source.Tweeter.FriendsCount))
                .ForMember(destination => destination.Language, options => options.MapFrom(source => source.Tweeter.Language))
                .ForMember(destination => destination.Location, options => options.MapFrom(source => source.Tweeter.Location))
                .ForMember(destination => destination.TweetsCount, options => options.MapFrom(source => source.Tweeter.TweetsCount))
                .ForMember(destination => destination.Verified, options => options.MapFrom(source => source.Tweeter.Verified))
                .ForMember(destination => destination.ProfileImageUrl, options => options.MapFrom(source => source.Tweeter.ProfileImageUrl))
                .ForMember(destination => destination.ProfileBannerUrl, options => options.MapFrom(source => source.Tweeter.ProfileBannerUrl))
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.User.UserName));

            this.CreateMap<TweetDto, TweetViewModel>()
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.User.UserName))
                .ForMember(destination => destination.Tweeter, options => options.MapFrom(source => source.Tweeter));

            this.CreateMap<TweetDto, EditTweetViewModel>()
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.User.UserName));

            this.CreateMap<TweeterDto, TweeterViewModel>()
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.User.UserName));

            this.CreateMap<TweeterDto, EditTweeterViewModel>()
                .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.User.UserName));

            this.CreateMap<TweeterDto, Tweeter>()
                .ForMember(destination => destination.IsDeleted, options => options.Ignore())
                .ForMember(destination => destination.DeletedOn, options => options.Ignore())
                .ForMember(destination => destination.Tweets, options => options.Ignore())
                .ForMember(destination => destination.UserTweeters, options => options.Ignore());

            this.CreateMap<ApiTweetDto, Tweet>()
                .ForMember(destination => destination.IsDeleted, opt => opt.Ignore())
                .ForMember(destination => destination.DeletedOn, opt => opt.Ignore())
                .ForMember(destination => destination.TweetHashtags, opt => opt.AllowNull())
                .ForMember(destination => destination.UserTweets, opt => opt.Ignore());

        }
    }
}