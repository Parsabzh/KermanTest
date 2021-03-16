using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Models.Window;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Domain.Interfaces
{
    public interface IWindowRepository
    {
        Task<ResponseBase<Window>> AddWindow(Window win);
        Task<ResponseBase<Window>> UpdateWindow(Window win);
        Task<ResponseBase<Window>> DeleteWindow(int winId);
        Task<ResponseBase<Window>> GetWindow(int winId);
        Task<ResponseBase<IEnumerable<Window>>> GetAllWindows();


    }
}
