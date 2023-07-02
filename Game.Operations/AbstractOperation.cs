using AutoMapper;

namespace Game.Operations
{
    public class AbstractOperations
    {
        protected readonly IMapper Mapper;

        protected AbstractOperations(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}