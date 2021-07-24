using System.Linq;
using HotChocolate;
using MSAYearbook.Data;
using MSAYearbook.Models;

namespace MSAYearbook.GraphQL.Students
{
    [HotChocolate.Types.ExtendObjectType(name: "Query")]
    public class StudentQueries
    {
        public IQueryable<Student> GetStudents([ScopedService] AppDbContext context)
        {
            return context.Students;
        }
    }
}