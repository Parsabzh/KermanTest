using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using KermanCraft.Domain.Interfaces;
using KermanCraft.Domain.Models.Sms;
using KermanCraft.Domain.ViewModel.ResponseViewModel;
using KermanCraft.Infrastructure.Data.Context;
using KermanCraft.Infrastructure.Localize.Messaging;
using KermanCraft.Infrastructure.Localize.SmsResource;
using Microsoft.EntityFrameworkCore;

namespace KermanCraft.Infrastructure.Data.Repository
{
    public class SmsRepository : ISmsRepository
    {
        private readonly KermanCraftDb _db;

        public SmsRepository(KermanCraftDb db)
        {
            _db = db;
        }

        public async Task<ResponseBase<Sms>> AddSms(string userPhone)
        {

            try
            {
                var code = new Random().Next(1000, 9999);
                var sms = new Sms()
                {
                    PhoneNumber = userPhone,
                    Code = code
                };
                await _db.Sms.AddAsync(sms);
                await _db.SaveChangesAsync();
                return new ResponseBase<Sms>()
                {
                    Entity = sms,
                    Message = SmsResource.SendSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Sms>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Sms>> GetCode(string userPhone)
        {
            try
            {
                var code = await _db.Sms.SingleOrDefaultAsync(i => i.PhoneNumber == userPhone);
                if (code == null)
                {
                    return new ResponseBase<Sms>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound)
                    };
                }

                return new ResponseBase<Sms>()
                {
                    Entity = code,
                    Message = MessageResource.SearchSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Sms>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }

        public async Task<ResponseBase<Sms>> DeleteCode(string userPhone)
        {
            try
            {
                var code = await _db.Sms.SingleOrDefaultAsync(i => i.PhoneNumber == userPhone);
                if (code == null)
                {
                    return new ResponseBase<Sms>()
                    {
                        IsSuccessful = false,
                        Message = string.Format(MessageResource.NotFound)
                    };
                }

                _db.Sms.Remove(code);
                await _db.SaveChangesAsync();
                return new ResponseBase<Sms>()
                {
                    
                    Message = MessageResource.DeletSuccess,
                    IsSuccessful = true
                };
            }
            catch (Exception e)
            {
                return new ResponseBase<Sms>()
                {
                    Message = e.Message,
                    IsSuccessful = false
                };
            }
        }
    }
}
