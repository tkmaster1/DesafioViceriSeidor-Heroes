using Heroes.Core.Application.Services.Web.Interfaces;

namespace Heroes.Core.Application.Facades.Interfaces;

public interface IApiFacade
{
    ISuperheroesApplication SuperheroesApp { get; }
}