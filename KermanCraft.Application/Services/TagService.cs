using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KermanCraft.Application.Interfaces;
using KermanCraft.Application.ViewModels.Tag;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Tag;
using KermanCraft.Domain.ViewModel.ResponseViewModel;

namespace KermanCraft.Application.Services
{
    public class TagService:ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(IMapper mapper, ITagRepository tagRepository)
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
        }

        public async Task<ResponseBase<TagViewModel>> AddTag(TagViewModel tag)
        {
          
            var result = await _tagRepository.AddTag(_mapper.Map<Tag>(tag));
            return new ResponseBase<TagViewModel>()
            {
                Entity = _mapper.Map<TagViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };

        }

        public async Task<ResponseBase<TagViewModel>> UpdateTag(TagViewModel tag)
        {
          
            var result = await _tagRepository.UpdateTag(_mapper.Map<Tag>(tag));
            return new ResponseBase<TagViewModel>()
            {
                Entity = _mapper.Map<TagViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<TagViewModel>> DeleteTag(int tagId)
        {
            var result = await _tagRepository.DeleteTag(tagId);
            return new ResponseBase<TagViewModel>()
            {

                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<TagViewModel>> GetTag(int tagId)
        {
            var result = await _tagRepository.GetTag(tagId);
            return new ResponseBase<TagViewModel>()
            {
                Entity = _mapper.Map<TagViewModel>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<TagViewModel>>> GetProductTag(int productId)
        {
            var result = await _tagRepository.GetProductTag(productId);
            return new ResponseBase<IEnumerable<TagViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<TagViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<TagViewModel>>> GetNewsTag(int newsId)
        {
            var result = await _tagRepository.GetNewsTag(newsId);
            return new ResponseBase<IEnumerable<TagViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<TagViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<TagViewModel>>> GetArticleTag(int articleId)
        {
            var result = await _tagRepository.GetArticleTag(articleId);
            return new ResponseBase<IEnumerable<TagViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<TagViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }

        public async Task<ResponseBase<IEnumerable<TagViewModel>>> GetAllTag()
        {

            var result = await _tagRepository.GetAllTag();
            return new ResponseBase<IEnumerable<TagViewModel>>()
            {
                Entity = _mapper.Map<IEnumerable<TagViewModel>>(result.Entity),
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
        }
    }
}
