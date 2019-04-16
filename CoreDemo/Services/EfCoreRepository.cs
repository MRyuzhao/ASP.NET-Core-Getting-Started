using System.Collections.Generic;
using System.Linq;
using EfCore.Data;
using EfCore.Domain;

namespace CoreDemo.Services
{
    public class EfCoreRepository : IRepository<Student>
    {
        private readonly MyContext _context;

        public EfCoreRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student GetById(int id)
        {
            return _context.Students.FirstOrDefault(x => x.Id == id);
        }

        public Student Add(Student t)
        {
            _context.Students.Add(t);
            _context.SaveChanges();
            return t;
        }
    }
}