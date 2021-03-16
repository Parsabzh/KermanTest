using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.ViewModels.Window;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Window;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
   public  class WindowService:IWindowService
   {
       private readonly IWindowRepository _windowRepository;
       private readonly IMapper _mapper;
       private readonly IImageService _imageService;

       public WindowService(IWindowRepository windowRepository, IMapper mapper, IImageService imageService)
       {
           _windowRepository = windowRepository;
           _mapper = mapper;
           _imageService = imageService;
       }

       public async Task<ResponseBase<WindowViewModel>> AddWindow(WindowViewModel win)
        {
            var result = await _windowRepository.AddWindow(_mapper.Map<Window>(win));
            return new ResponseBase<WindowViewModel>()
            {
                Entity = _mapper.Map<WindowViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message=result.Message
            };

        }

        public async Task<ResponseBase<UpdateWindowViewModel>> UpdateWindow(UpdateWindowViewModel win)
        {
            var result = await _windowRepository.UpdateWindow(_mapper.Map<Window>(win));
            return new ResponseBase<UpdateWindowViewModel>()
            {
                Entity = _mapper.Map<UpdateWindowViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<WindowViewModel>> DeleteWindow(int winId)
        {
            var result = await _windowRepository.DeleteWindow(winId);
            return new ResponseBase<WindowViewModel>()
            {
                
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<WindowViewModel>> GetWindow(int winId)
        {
            var result = await _windowRepository.GetWindow(winId);
            return new ResponseBase<WindowViewModel>()
            {
                Entity = _mapper.Map<WindowViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

       

        public async Task<ResponseBase<IEnumerable<WindowViewModel>>> GetAllWindowByCategory(string category)
        {
            var result = await _windowRepository.GetAllWindows();
            var model = result.Entity.Where(i => i.Category == category);
            var entity = _mapper.Map<IEnumerable<WindowViewModel>>(model).ToList();
            foreach (var item in entity)
            {
                var image = await _imageService.GetWindowsImage(item.WindowId);
                item.ImageViewModels = image.Entity.ToList();
            }
            return new ResponseBase<IEnumerable<WindowViewModel>>()
            {
                Entity = entity,
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<WindowViewModel>>> GetAllWindow()
        {

            var result = await _windowRepository.GetAllWindows();
            var entity = _mapper.Map<IEnumerable<WindowViewModel>>(result.Entity).ToList();
            foreach (var item in entity)
            {
                var image = await _imageService.GetWindowsImage(item.WindowId);
                item.ImageViewModels = image.Entity.ToList();
            }
            return new ResponseBase<IEnumerable<WindowViewModel>>()
            {
                Entity = entity,
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
