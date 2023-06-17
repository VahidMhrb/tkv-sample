using Application.Common.Interfaces.BusinessRules.Base;
using Infrastructure.Common.Configuration;
using Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.BusinessRules.Base
{
    public class BusinessRuleBase : IBusinessRuleBase
    {
        protected readonly ApplicationDbContext Context;
        protected readonly ApplicationSetting Settings;
       protected readonly IHttpContextAccessor? _accessor;
        
        protected BusinessRuleBase(IOptionsMonitor<ApplicationSetting> options, ApplicationDbContext context)
        {
            Settings = options.CurrentValue;
            Context = context;
        }

        protected BusinessRuleBase(IOptionsMonitor<ApplicationSetting> options, ApplicationDbContext context, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            Settings = options.CurrentValue;
            Context = context; 
        }


        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                /// TO-DO ...
            }

            _disposed = true;
        }
    }
}
