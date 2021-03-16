using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Application.ViewModels.Window;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Interfaces
{
    public interface IWindowService
    {
        Task<ResponseBase<WindowViewModel>> AddWindow(WindowViewModel win);
        Task<ResponseBase<UpdateWindowViewModel>> UpdateWindow(UpdateWindowViewModel win);
        Task<ResponseBase<WindowViewModel>> DeleteWindow(int winId);
        Task<ResponseBase<WindowViewModel>> GetWindow(int winId);
        Task<ResponseBase<IEnumerable<WindowViewModel>>> GetAllWindow();
        Task<ResponseBase<IEnumerable<WindowViewModel>>> GetAllWindowByCategory(string category);
    }
}
