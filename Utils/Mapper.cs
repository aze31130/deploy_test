using AutoMapper;
using deploy_test.DTO;
using deploy_test.Models;

namespace deploy_test.Utils
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
        }
    }
}
