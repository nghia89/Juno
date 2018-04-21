namespace Juno.Data.Migrations
{
    using Juno.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Juno.Data.JunoDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }  

        protected override void Seed(Juno.Data.JunoDBContext context)
        {
            CreateProductCatrgory(context);
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new JunoDBContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new JunoDBContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "tedu",
            //    Email = "hangnghia11089@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Technology Education"

            //};

            //manager.Create(user, "123654$");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("hangnghia11089@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }
        private void CreateProductCatrgory(Juno.Data.JunoDBContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory() { Name="Điện lạnh",Alias="dien-lanh",Status=true },
                 new ProductCategory() { Name="Viễn thông",Alias="vien-thong",Status=true },
                  new ProductCategory() { Name="Đồ gia dụng",Alias="do-gia-dung",Status=true },
                   new ProductCategory() { Name="Mỹ phẩm",Alias="my-pham",Status=true }
            };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }
    }
}
