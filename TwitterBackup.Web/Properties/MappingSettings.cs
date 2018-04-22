using AutoMapper;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.Web.Models.TweeterViewModels;

namespace TwitterBackup.Infrastructure.Providers
{
    public class MappingSettings : Profile
    {
        public MappingSettings()
        {
            this.CreateMap<TweetDto, TweeterViewModel>().ReverseMap();

            //this.CreateMap<>()
        }
    }
}