using System.Collections.Generic;

namespace DotnetToolset.Adapters
{
  public interface ITypeAdapter<in TSource, out TDestination> where TSource : class where TDestination : class
    {
        /// <summary>
        /// Adapts the source object to the destination object
        /// </summary>
        /// <param name="source">Source object</param>
        /// <returns>An object of the destination type</returns>
        public TDestination Adapt(TSource source);

        /// <summary>
        /// Adapts the source object enumerable collection to the destination object enumerable collection
        /// </summary>
        /// <param name="source">Source object enumerable collection</param>
        /// <returns>An object enumerable collection of the destination type</returns>
        public IEnumerable<TDestination> Adapt(IEnumerable<TSource> source);
    }
}
