using AutoMapper;

namespace CustomersList.Application.UseCases.Abstractions;

public abstract class UseCaseBase
{
    private readonly IMapper _mapper;

    protected UseCaseBase( IMapper mapper )
    {
        _mapper = mapper;
    }

    protected IMapper Mapper => _mapper;
}
