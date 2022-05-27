using System.Linq.Expressions;

namespace LinqOvning
{
    internal static class ExtensionEx
    {
        public static IEnumerable<Person> FindUppercaseBegin(this IEnumerable<Person> persons)
        {
            return persons.Where(a => a.Name is not null && char.IsUpper(a.Name[0])).ToList();
        }

        public static IEnumerable<Person> FindName(this IEnumerable<Person> persons, Expression<Func<Person, bool>> predicate)
        {
            return persons.Where(predicate.Compile()).ToList();
        }

        public static IEnumerable<int> GetAges(this IEnumerable<Person> persons, Expression<Func<Person, bool>> predicate)
        {
            return persons.Where(predicate.Compile()).Select(a => a.Age).ToList();
        }

        public static void SumPersonAges(this IEnumerable<Person> persons, Expression<Func<Person, bool>> predicate)
        {
            var targets = persons.Where(predicate.Compile()).ToList();

            Console.WriteLine(
                $"The sum of all ages together with the name {targets.ElementAt(0).Name} is {targets.Sum(a => a.Age)}");

        }

        public static IEnumerable<Person> EndsWithLetter(this IEnumerable<Person> persons, Expression<Func<Person, bool>> predicate)
        {
            return persons.Where(predicate.Compile()).ToList();
        }

        public static IEnumerable<string> GetAgeLimit(this IEnumerable<Person> persons, Expression<Func<Person, bool>> predicate)
        {
            return persons.Where(predicate.Compile()).Select(n => n.Name);
        }

        public static IEnumerable<Person> GetDistinctNames(this IEnumerable<Person> persons)
        {
            return persons.DistinctBy(n => n.Name).ToList();
        }

        public static void SumAgeAndNames(this IEnumerable<Person> persons)
        {
            var _pers = from a in persons
                        where !string.IsNullOrWhiteSpace(a.Name)
                        select new
                        {
                            Name = a.Name,
                            Age = a.Age,
                            letterLen = a.Name.Length
                        };


            Console.WriteLine($"Sum of all ages together: {_pers.Sum(age => age.Age)}");


            foreach (var per in _pers)
            {
                Console.WriteLine($"Name: {per.Name} Age: {per.Age} Name lenght: {per.letterLen}");
            }

        }

        public static void IsOldest(this IEnumerable<Person> persons)
        {
            var person = persons.Where(p => p.Name is not null).MaxBy(a => a.Age);

            Console.WriteLine($"Oldes person in collection is {person.Name}. Age {person.Age}");

        }

        public static void HasLogestName(this IEnumerable<Person> persons)
        {
            var person = persons.OrderByDescending(x => x.Name.Length).First();

            Console.WriteLine($"Longest name is own by {person.Name}");

        }

        public static void CountLetters(this IEnumerable<Person> persons, int nrOfLetters = 4)
        {
            var _persons = persons.Where(x => x.Name.Length == nrOfLetters).ToList();

            foreach (var person in _persons)
            {
                Console.WriteLine($"{person.Name} contain {nrOfLetters}");
            }
        }


        public static void FindDoubleNames(this IEnumerable<Person> persons, int nrOfLetters = 4)
        {
            var _persons = persons.Where(x => x.Name.Contains('-')).ToList();

            foreach (var person in _persons)
            {
                Console.WriteLine($"{person.Name} is a double name");
            }
        }

        public static void UniqueNameCheck(this IEnumerable<Person> persons)
        {
            var _persons = persons.GroupBy(n => n.Name)
                                     .Select(n => new
                                     {
                                         Name = n.Key,
                                         NoOfNames = n.Count()
                                     })
                                     .OrderBy(n => n.NoOfNames);


            foreach (var person in _persons)
            {
                Console.WriteLine($"{person.Name} Nr of names {person.NoOfNames}");
            }
        }


        public static void CompareCollections(this IEnumerable<Person> persons, IEnumerable<int> _numbers)
        {
            var _persons = from person in persons
                           join number in _numbers on person.Age equals number
                           select person;

            foreach (var person in _persons)
            {
                Console.WriteLine($"Age ({person.Age}) of {person.Name} exist in number array as well");
            }

        }
    }
}
