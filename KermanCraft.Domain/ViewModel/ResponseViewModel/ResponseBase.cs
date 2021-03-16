using System;
using System.Collections.Generic;
using System.Text;

namespace KermanCraft.Domain.ViewModel.ResponseViewModel
{
   public class ResponseBase<TEntity>
    {
        public TEntity Entity { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        public List<string> MessageList { get; set; }
    }
}
