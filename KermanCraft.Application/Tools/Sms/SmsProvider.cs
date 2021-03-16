#nullable enable
using System.Threading.Tasks;
using Kavenegar;
using Kavenegar.Core.Models;
using Kavenegar.Core.Models.Enums;

namespace KermanCraft.Application.Tools.Sms
{
   public static class SmsProvider
    {
        public static Task<SendResult> SendSms(string serviceName,
            string phoneNumber,
            string token10,
            string token20,
            string token)
        {
            var api = new KavenegarApi("71574B44696F5177394F7650322F6E454A4C306551423842562F4E587A56752F4C33637A6D76614C7965593D");
            var send = api.VerifyLookup(phoneNumber, token, "", "", token10, token20, serviceName, VerifyLookupType.Sms);
            return send;
        }

        public static Task<SendResult> SendSms(string serviceName,
            string phoneNumber,
            string token)
        {
            var api = new KavenegarApi("71574B44696F5177394F7650322F6E454A4C306551423842562F4E587A56752F4C33637A6D76614C7965593D");
            var send = api.VerifyLookup(phoneNumber, token, serviceName, VerifyLookupType.Sms);
            return send;
        }

        public static Task<SendResult> SendSms(string serviceName,
            string phoneNumber,
            string token10,
            string token20,
            string token,
            string toke2)
        {
            var api = new KavenegarApi("71574B44696F5177394F7650322F6E454A4C306551423842562F4E587A56752F4C33637A6D76614C7965593D");
            var send = api.VerifyLookup(phoneNumber, token, toke2, "", token10, token20, serviceName, VerifyLookupType.Sms);
            return send;
        }

    }
}
