using AutoMapper;
using Core.Abstractions;

namespace TCP.MapperLayer.Mapper
{
    public class ViewMapper<IDto,T> : IViewMapper<IDto,T>
    {
        protected IMapper _mapper;
        public ViewMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public virtual IEnumerable<IDto> Map(IEnumerable<T> entities)
        {
            return _mapper.Map<IEnumerable<IDto>>(entities);
        }

        public virtual IDto Map(T entity)
        {
            return _mapper.Map<IDto>(entity);
        }
    }
}
