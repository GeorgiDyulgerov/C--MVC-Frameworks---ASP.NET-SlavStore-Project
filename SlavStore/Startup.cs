using Microsoft.Owin;
using Owin;
using AutoMapper;
using SlavStore.Models;
using SlavStore.Models.BindingModels;
using SlavStore.Models.ViewModels;

[assembly: OwinStartupAttribute(typeof(SlavStore.Startup))]
namespace SlavStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMapper();
            ConfigureAuth(app);
        }

        private void ConfigureMapper()
        {
            Mapper.Initialize(ex =>
            {
                ex.CreateMap<CreateItemBindingModel, Item>().ForMember(m=>m.Category,opt=>opt.Ignore());
                ex.CreateMap<EditItemBindingModel, Item>().ForMember(m => m.Category, opt => opt.Ignore());
                ex.CreateMap<Item, EditItemViewModel>();
                ex.CreateMap<EditItemBindingModel, EditItemViewModel>();
                ex.CreateMap<CreateItemBindingModel, CreateItemViewModel>();

            });
        }
    }
}
