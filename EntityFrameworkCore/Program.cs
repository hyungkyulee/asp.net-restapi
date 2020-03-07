using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using EntityFrameworkCore.Entities;

namespace EntityFrameworkCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("### .NET Core - Local SQL DB CRUD ###");

            using var db = new EFDbContext();

            Console.WriteLine("\n=====================================");
            Console.WriteLine("Direct get DB instance");

            var userList = db.Users;
            foreach (var user in userList)
            {
                Console.WriteLine("  [{0}]-th User >> Name: {1}, Email: {2}", user.UserId, user.UserName, user.Email);
            }

            Console.WriteLine("\n=====================================");
            Console.WriteLine("SELECT Query by Linq to Sql (client side resource used)");

            IQueryable<User> selectLinqtoSql = from user2 in db.Users
                                               select user2;
            foreach (var user1 in selectLinqtoSql)
            {
                Console.WriteLine("  [{0}]-th User >> Name: {1}, Email: {2}", user1.UserId, user1.UserName, user1.Email);
            }

            Console.WriteLine("\n=====================================");
            Console.WriteLine("ADD Query");

            var newItem = new User
            {
                UserId = 4,
                UserName = "4th User added",
                Email = "user@test.com",
            };
            db.Users.Add(newItem);
            db.SaveChanges();
            Console.WriteLine("4th-item is added on DB ...\n");

            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine("SELECT Query by List Type");

            List<User> printList = db.Users.ToList();
            foreach (var user2 in printList)
            {
                Console.WriteLine("  [{0}]-th User >> Name: {1}, Email: {2}", user2.UserId, user2.UserName, user2.Email);
            }

            Console.WriteLine("\n=====================================");
            Console.WriteLine("UPDATE Query");

            var updateItem = new User
            {
                UserId = 4,
                UserName = "4th User Updated",
            };
            db.Entry(updateItem).State = EntityState.Modified;
            db.SaveChanges();

            Console.WriteLine("4th-item has been updated on DB ...\n");

            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine("SELECT Query by Enumerable Type (client side resource used)");

            IEnumerable<User> enumList = db.Users.AsEnumerable();
            foreach (var user4 in enumList)
            {
                Console.WriteLine("  [{0}]-th User >> Name: {1}, Email: {2}", user4.UserId, user4.UserName, user4.Email);
            }

            Console.WriteLine("\n=====================================");
            Console.WriteLine("DELETE Query");

            var delItem = new User
            {
                UserId = 4,
            };
            db.Users.Remove(delItem);
            db.SaveChanges();

            Console.WriteLine("4th-item has been deleted from DB ...\n");

            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine("SELECT Query by DbSet Type");

            DbSet<User> dbsetList = db.Users;
            foreach (var user5 in dbsetList)
            {
                Console.WriteLine("  [{0}]-th User >> Name: {1}, Email: {2}", user5.UserId, user5.UserName, user5.Email);
            }
        }
    }
}
