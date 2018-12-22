using System;
using System.Collections.Generic;
using System.Linq;

namespace CXY.CJS.Core.Extension
{
    public static class IEnumerableExtension
    {

        /// <summary>
        /// 分割成指定大小
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="chunksize">指定大小</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
        {
            while (source.Any())
            {
                yield return source.Take(chunksize);
                source = source.Skip(chunksize);
            }
        }

    }
}