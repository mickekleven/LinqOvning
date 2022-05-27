namespace LinqOvning
{
    internal static class LinqExample
    {
        public static IEnumerable<Person> FindUppercaseBegin(this IEnumerable<Person> persons, string letter)
        {
            return persons.Where(a => a.Name is not null && char.IsUpper(a.Name[0])).ToList();
        }
    }
}
