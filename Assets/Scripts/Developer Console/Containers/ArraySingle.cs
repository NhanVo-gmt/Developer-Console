using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console.Containers
{
    public class ArraySingle<T> : IReadOnlyList<T>
    {
        private readonly T data;

        public ArraySingle(T data)
        {
            this.data = data;
        }
        
        public T this[int index] => data;

        public int Count => 1;

        public IEnumerator<T> GetEnumerator()
        {
            yield return data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return data;
        }
    }

    public static class ArraySingleExtendsions
    {
        public static ArraySingle<T> AsArraySingle<T>(this T data)
        {
            return new ArraySingle<T>(data);
        }
    }
}
