using System;
using System.Collections.Generic;
using System.Linq;
using CoreDemo.Models;

namespace CoreDemo.Services
{
    public class InMomoryRepository : IRepository<Student>
    {
        private static List<Student> _students;

        public InMomoryRepository()
        {
            _students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    FirstName = "Nike",
                    LastName = "Carter",
                    Birthday = new DateTime(1980,1,1)
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Hello",
                    LastName = "Kity",
                    Birthday = new DateTime(1922,1,1)
                },
                new Student
                {
                    Id = 3,
                    FirstName = "Dave",
                    LastName = "Some",
                    Birthday = new DateTime(1933,1,1)
                }
            };
        }

        public IEnumerable<Student> GetAll()
        {
            return _students;
        }

        public Student GetById(int id)
        {
            return _students.FirstOrDefault(x => x.Id == id);
        }

        public Student Add(Student t)
        {
            var maxId = _students.Max(x => x.Id) + 1;
            t.Id = maxId;
            _students.Add(t);
            return t; 
        }
    }
}