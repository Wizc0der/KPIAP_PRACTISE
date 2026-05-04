using System;
using System.Collections.Generic;

interface IUser
{
    List<string> GetPermissions();
    string GetRole();
}

class AdminUser : IUser
{
    public string GetRole() => "Admin";
    public List<string> GetPermissions() => new List<string> { "Read", "Write", "Delete", "ManageUsers" };
}

class ModeratorUser : IUser
{
    public string GetRole() => "Moderator";
    public List<string> GetPermissions() => new List<string> { "Read", "Write", "Delete" };
}

class RegularUser : IUser
{
    public string GetRole() => "Regular";
    public List<string> GetPermissions() => new List<string> { "Read", "Write" };
}

abstract class UserFactory
{
    public abstract IUser CreateUser();
}

class AdminFactory : UserFactory
{
    public override IUser CreateUser() => new AdminUser();
}

class ModeratorFactory : UserFactory
{
    public override IUser CreateUser() => new ModeratorUser();
}

class RegularFactory : UserFactory
{
    public override IUser CreateUser() => new RegularUser();
}

class Program
{
    static void Main()
    {
        UserFactory[] factories = { new AdminFactory(), new ModeratorFactory(), new RegularFactory() };

        foreach (var factory in factories)
        {
            var user = factory.CreateUser();
            Console.WriteLine($"\n{user.GetRole()}: {string.Join(", ", user.GetPermissions())}");
        }
    }
}