using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SlavStore.Models;
using SlavStore.Models.BindingModels;
using SlavStore.Models.ViewModels;

namespace SlavStore.Services.Mapper
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            this.CreateMap<CreateItemBindingModel, Item>().ForMember(m => m.Category, opt => opt.Ignore());
            this.CreateMap<EditItemBindingModel, Item>().ForMember(m => m.Category, opt => opt.Ignore());
            this.CreateMap<Item, EditItemViewModel>();
            this.CreateMap<EditItemBindingModel, EditItemViewModel>();
            this.CreateMap<CreateItemBindingModel, CreateItemViewModel>();

            this.CreateMap<Item, HomeViewModel>();
        }
    }
}
