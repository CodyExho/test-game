using AutoMapper;
using Game.Common.Models;
using Game.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Common.Profiles
{
    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            CreateMap<OfferEntity, OfferModel>().ReverseMap();
        }
    }
}
