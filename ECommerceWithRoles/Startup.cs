using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ECommerceWithRoles.Models;
using System.Linq;
using System.Collections.Generic;

[assembly: OwinStartupAttribute(typeof(ECommerceWithRoles.Startup))]
namespace ECommerceWithRoles
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //CreateDbIfNotExists();
        }
        // In this method we will create default User roles and Admin user for login    
        //private void CreateDbIfNotExists()
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();

        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


        //    // In Startup I am creating first Admin Role and creating a default Admin User     
        //    if (!roleManager.RoleExists("Admin"))
        //    {

        //        // first we create Admin role    
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "Admin";
        //        roleManager.Create(role);

        //        //Here we create a Admin super user who will maintain the website                   

        //        var user = new ApplicationUser();
        //        user.UserName = "Admin";
        //        user.Email = "admin@gmail.com";
        //        user.ShoppingBasket = new ShoppingBasket();

        //        string userPWD = "password";

        //        var chkUser = UserManager.Create(user, userPWD);

        //        //Add default User to Role Admin    
        //        if (chkUser.Succeeded)
        //        {
        //            var result1 = UserManager.AddToRole(user.Id, "Admin");

        //        }
        //    }

        //    // creating Creating User role     
        //    if (!roleManager.RoleExists("User"))
        //    {
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "User";
        //        roleManager.Create(role);

        //        var user = new ApplicationUser();
        //        user.UserName = "User";
        //        user.Email = "user@gmail.com";
        //        user.ShoppingBasket = new ShoppingBasket();

        //        string userPWD = "password";

        //        var chkUser = UserManager.Create(user, userPWD);

        //        //Add default User to Role User    
        //        if (chkUser.Succeeded)
        //        {
        //            var result1 = UserManager.AddToRole(user.Id, "User");

        //        }
        //    }



        //    // Seed db with tags, departments, products
        //    if (!context.Tags.Any())
        //    {
        //        var tags = new Tag[]
        //        {
        //            new Tag
        //            {
        //                Description = "GPU"
        //            },
        //            new Tag
        //            {
        //                Description = "Nvidia"
        //            },
        //            new Tag
        //            {
        //                Description = "Game"
        //            },
        //            new Tag
        //            {
        //                Description = "Sci-Fi"
        //            },
        //            new Tag
        //            {
        //                Description = "Strategy"
        //            },
        //            new Tag
        //            {
        //                Description = "Peripheral"
        //            }
        //        };
        //        foreach (Tag t in tags)
        //        {
        //            context.Tags.Add(t);
        //        }
        //        context.SaveChanges();
        //    }

        //    if (!context.Departments.Any())
        //    {
        //        var departments = new Department[]
        //        {
        //            new Department
        //            {
        //                Name = "Hardware",
        //                Description = "It's tangible, baby."
        //            },
        //            new Department
        //            {
        //                Name = "Software",
        //                Description = "Can't touch this."
        //            }
        //        };
        //        foreach (Department d in departments)
        //        {
        //            context.Departments.Add(d);
        //        }
        //        context.SaveChanges();
        //    }

        //    if (!context.Products.Any())
        //    {
        //        var products = new Product[]
        //        {
        //            new Product
        //            {
        //                Name = "GeForce RTX 3090 Graphics Card",
        //                Description = "Lol, out of stock obv.",
        //                Price = 1399.99m,
        //                Image = "Geforce3090.jpg",
        //                Department = context.Departments.Where(d => d.Name == "Hardware").FirstOrDefault()
        //            },
        //            new Product
        //            {
        //                Name = "Stellaris",
        //                Description = "Get ready to explore, discover and interact with a multitude of species as you journey among the stars. Forge a galactic empire by sending out science ships to survey and explore, while construction ships build stations around newly discovered planets. Discover buried treasures and galactic wonders as you spin a direction for your society, creating limitations and evolutions for your explorers. Alliances will form and wars will be declared.",
        //                Price = 34.99m,
        //                Image = "stellaris.jpg",
        //                Department = context.Departments.Where(d => d.Name == "Software").FirstOrDefault()
        //            },
        //            new Product
        //            {
        //                Name = "Logitech MX Anywhere 2 Wireless Mouse",
        //                Description = "If you put your hand on it the cursor on the screen magically moves.",
        //                Price = 48.50m,
        //                Image = "logitech2.jpg",
        //                Department = context.Departments.Where(d => d.Name == "Hardware").FirstOrDefault()
        //            }
        //        };

        //        products[0].Tags.Add(context.Tags.Where(t => t.Description == "GPU").FirstOrDefault());
        //        products[0].Tags.Add(context.Tags.Where(t => t.Description == "Nvidia").FirstOrDefault());
        //        products[1].Tags.Add(context.Tags.Where(t => t.Description == "Game").FirstOrDefault());
        //        products[1].Tags.Add(context.Tags.Where(t => t.Description == "Sci-Fi").FirstOrDefault());
        //        products[1].Tags.Add(context.Tags.Where(t => t.Description == "Strategy").FirstOrDefault());
        //        products[2].Tags.Add(context.Tags.Where(t => t.Description == "Peripheral").FirstOrDefault());


        //        foreach (Product p in products)
        //        {
        //            context.Products.Add(p);
        //        }
        //        context.SaveChanges();
        //    }
        //}
    }
}
