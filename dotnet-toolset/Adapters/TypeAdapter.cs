using AutoMapper;

namespace DotnetToolset.Adapters
{
  public class TypeAdapter<TSource, TDestination> : AdapterBase<TSource, TDestination>, ITypeAdapter<TSource, TDestination> where TSource : class
        where TDestination : class
    {
        public TypeAdapter(IMapper mapper) : base(mapper)
        {
        }
    }
}
