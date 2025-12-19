using Heroes.Common.Util.Services;

namespace Heroes.Core.Application.Services.Web.Interfaces;

public interface IBaseServiceResolver
{
    IBaseService For(string apiName);

    string DefaultApiName { get; }
}