using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TableOfRecords;

/// <summary>
/// Presents method that write in table form to the text stream a set of elements of type T.
/// </summary>
public static class TableOfRecordsCreator
{
    /// <summary>
    /// Write in table form to the text stream a set of elements of type T (<see cref="ICollection{T}"/>),
    /// where the state of each object of type T is described by public properties that have only build-in
    /// type (int, char, string etc.)
    /// </summary>
    /// <typeparam name="T">Type selector.</typeparam>
    /// <param name="collection">Collection of elements of type T.</param>
    /// <param name="writer">Text stream.</param>
    /// <exception cref="ArgumentNullException">Throw if <paramref name="collection"/> is null.</exception>
    /// <exception cref="ArgumentNullException">Throw if <paramref name="writer"/> is null.</exception>
    /// <exception cref="ArgumentException">Throw if <paramref name="collection"/> is empty.</exception>
    public static void WriteTable<T>(ICollection<T>? collection, TextWriter? writer)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection), "The collection is null.");
        }

        if (writer == null)
        {
            throw new ArgumentNullException(nameof(writer), "The writer is null.");
        }

        if (collection.Count == 0)
        {
            throw new ArgumentException("The collection is empty.", nameof(collection));
        }

        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var headers = props.Select(p => p.Name).ToArray();
        var widths = props.Select(p => p.Name.Length).ToArray();

        foreach (var item in collection)
        {
            for (int i = 0; i < props.Length; i++)
            {
                var value = props[i].GetValue(item)?.ToString() ?? string.Empty;
                if (value.Length > widths[i])
                {
                    widths[i] = value.Length;
                }
            }
        }

        string divider = "+" + string.Join("+", widths.Select(w => new string('-', w + 2))) + "+";

        throw new NotImplementedException();
    }
}
