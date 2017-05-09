using System.Collections.Generic;
using SlavStore.Models.ViewModels;

namespace SlavStore.Services
{
    public interface IHomeService
    {
        List<HomeViewModel> GetHomeItems();
    }
}