using System;

namespace WebApp.Core.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Boşluk olacak şekilde join işlemini gerçekleştirir.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string JoinSpace(this string[] value)
            => string.Join(" ", value);

        /// <summary>
        /// Dizileriniz için tek satırda foreach ve işlemi gerçekleştirebilirsiniz.
        /// </summary>
        /// <typeparam name="T">Dizi türü.</typeparam>
        /// <param name="values">Dizi nesnenin.</param>
        /// <param name="action">Her bir dizi elemanınızda yapacağınız işlem.</param>
        /// <example>
        /// public void AddFields(params Type1[] objects)
        /// {
        ///     objects.ForEach(o => _objects.Add(o.ToLower()));
        /// }
        /// </example>
        public static void ForEach<T>(this T[] values, Action<T> action)
        {
            foreach (var value in values)
            {
                action(value);
            }
        }
    }
}