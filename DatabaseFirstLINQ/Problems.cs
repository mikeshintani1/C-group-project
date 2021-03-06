using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void Runlinqqueries()
        {
            //problemone();
            //problemtwo();
            //problemthree();
            //problemfour();
            //problemfive();
            //problemsix();
            //problemseven();
            //problemeight();
            //problemnine();
            //problemten();
            //problemeleven();
            //problemtwelve();
            //problemthirteen();
            //problemfourteen();
            //problemfifteen();
            //problemsixteen();
            //problemseventeen();
            //problemeighteen();
            //problemnineteen();
            //problemtwenty();
            //bonusone();
            bonustwo();
        }

        //<><><><><><><><> r actions(read) <><><><><><><><><>
        private void problemone()
        {
            //write a linq query that returns the number of users in the users table.
            //hint: .tolist().count
            var users = _context.Users;
            int userCount = users.Count();
            Console.WriteLine(userCount);

        }

        private void problemtwo()
        {
            //write a linq query that retrieves the users from the user tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void problemthree()
        {
            //write a linq query that gets each product where the products price is greater than $150.
            // then print the name and price of each product from the above query to the console.
            var products = _context.Products;
            foreach (Product product in products)
            if (product.Price > 150)
            {
                    Console.WriteLine($"Product: {product.Name}  Price: ${product.Price}");
            }
        }

        private void problemfour()
        {
            //write a linq query that gets each product that contains an "s" in the products name.
            // then print the name of each product from the above query to the console.
            var products = _context.Products;
            foreach(Product product in products)
            if (product.Name.Contains("s"))
            {
                    Console.WriteLine(product.Name);
            }

        }

        private void problemfive()
        {
            //write a linq query that gets all of the users who registered before 2016
            // then print each user's email and registration date to the console.
            var users = _context.Users;
            DateTime d1 = new DateTime(2016, 1, 1);
            
            foreach (User user in users) 
               
                if (user.RegistrationDate < d1 )
            {
                    Console.WriteLine(user.Email);
            }
        }

        private void problemsix()
        {
            //write a linq query that gets all of the users who registered after 2016 and before 2018
            // then print each user's email and registration date to the console.
            var users = _context.Users;
            DateTime date1 = new DateTime(2018, 1, 1);
            DateTime date2 = new DateTime(2016, 1, 1);
            foreach (User user in users)
                if  (user.RegistrationDate < date1 & user.RegistrationDate > date2)
            {
                    Console.WriteLine($"Email: {user.Email}\nRegistration: {user.RegistrationDate}");
            }

        }

         //<><><><><><><><> r actions(read) with foreign keys<><><><><><><><><>

        private void problemseven()
        {
            //write a linq query that retreives all of the users who are assigned to the role of customer.
            // then print the users email and role name to the console.
            var customerusers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "customer");
            foreach (UserRole userrole in customerusers)
            {
                Console.WriteLine($"email: {userrole.User.Email} role: {userrole.Role.RoleName}");
            }
        }

        private void problemeight()
        {
            //write a linq query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // then print the product's name, price, and quantity to the console.
            var shoppingcart = _context.ShoppingCarts.Include(ur => ur.User).Include(ur => ur.Product).Where(ur => ur.User.Email == "afton@gmail.com");
            foreach (ShoppingCart shoppingCart in shoppingcart)
            {
                Console.WriteLine($"{shoppingCart.Product.Name} price {shoppingCart.Product.Price} quantity {shoppingCart.Quantity}");
            }
        }

        private void problemnine()
        {
            //write a linq query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // hint: end of query will be: .select(sc => sc.product.price).sum();
            //then print the total of the shopping cart to the console.
            var shoppingcarts = _context.ShoppingCarts.Include(ur => ur.User).Include(ur => ur.Product).Where(ur => ur.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum(); 
           
            {
                Console.WriteLine(shoppingcarts);

            }
        }

        private void problemten()
        {
            //write a linq query that retreives all of the products in the shopping cart of users who have the role of "employee".
            // then print the user's email as well as the product's name, price, and quantity to the console.
          
            var roles = _context.UserRoles.Include(r => r.Role).Include(u => u.User).Where(r => r.Role.RoleName == "employee").Select(d=> d.UserId);
            var scs = _context.ShoppingCarts.Include(ur => ur.Product).Include(ur => ur.User).Where(cart => roles.Contains(cart.UserId));
            foreach (ShoppingCart sc in scs)   
            {
                Console.WriteLine($"User Email: {sc.User.Email}, Product {sc.Product.Name} Price: {sc.Product.Price} Quantity: {sc.Quantity} ");
            }
   
                {

                }
       
        }
         //<><><><><><><><> cud(create, update, delete) actions<><><><><><><><><>

         //<><> c actions(create) <><>

        private void problemeleven()
        {
            // create a new user object and add that user to the users table using linq.
            User newuser = new User()
            {
               Email = "david@gmail.com",
               Password = "davidspass123"
           };
            _context.Users.Add(newuser);
            _context.SaveChanges();
        }

        private void problemtwelve()
        {
            //create a new product object and add that product to the products table using linq.
            Product newproduct = new Product()
            {
                Name = "Fruit Roll Up",
                Description = "Delicious Candy",
                Price = 500,

            };
            _context.Products.Add(newproduct);
            _context.SaveChanges();

        }

        private void problemthirteen()
        {
            //add the role of "customer" to the user we just created in the userroles junction table using linq.
           var roleid = _context.Roles.Where(r => r.RoleName == "customer").Select(r => r.Id).SingleOrDefault();
            var userid = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newuserrole = new UserRole()
            {
                UserId = userid,
                RoleId = roleid
            };
            _context.UserRoles.Add(newuserrole);
            _context.SaveChanges();
        }

        private void problemfourteen()
        {
            //add the product you create to the user we created in the shoppingcart junction table using linq.
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").Select(r => r.Id).SingleOrDefault();
            var product = _context.Products.Where(p => p.Name == "Fruit Roll Up").Select(u => u.Id).SingleOrDefault();
            ShoppingCart newItem = new ShoppingCart()
            {
                UserId = user,
                ProductId = product,
                Quantity = 1
               

            };
            _context.ShoppingCarts.Add(newItem);
            _context.SaveChanges();

        }

         //<><> u actions(update) <><>

        private void problemfifteen()
        {
            //update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void problemsixteen()
        {
            //update the price of the product you created to something different using linq.
            var product = _context.Products.Where(p => p.Name == "Fruit Roll Up").SingleOrDefault();
            product.Price = 400;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void problemseventeen()
        {
            //change the role of the user we created to "employee"
            // hint: you need to delete the existing role relationship and then create a new userrole object and add it to the userroles table
            // see problem eighteen as an example of removing a role relationship

            var userrole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userrole);
            UserRole newuserrole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newuserrole);
            _context.SaveChanges();
        }

         //<><> d actions(delete) <><>

        private void problemeighteen()
        {
            //delete the role relationship from the user who has the email "oda@gmail.com" using linq.
            var user = _context.UserRoles.Where(u => u.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(user);
            
            _context.SaveChanges();
            
            
  
           
        }

        private void problemnineteen()
        {
            //delete all of the product relationships to the user with the email "oda@gmail.com" in the shoppingcart table using linq.
            //hint: loop
           var shoppingcartproducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userproductrelationship in shoppingcartproducts)
            {
                _context.ShoppingCarts.Remove(userproductrelationship);
            }
            _context.SaveChanges();
        }

        private void problemtwenty()
        {
            //delete the user with the email "oda@gmail.com" from the users table using linq.
            var user = _context.Users.Where(u => u.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();

        }

         //<><><><><><><><> bonus problems<><><><><><><><><>

        private void bonusone()
        {
            //prompt the user to enter in an email and password through the console.
            // take the email and password and check if the there is a person that matches that combination.
            // print "signed in!" to the console if they exists and the values match otherwise print "invalid email or password.".
            Console.WriteLine("Enter your email: ");
            var email = Console.ReadLine();
            Console.WriteLine("Enter your password: ");
            var password = Console.ReadLine();
            var user = _context.Users.Where(p => p.Email == email).Where(p => p.Password == password).SingleOrDefault();
            if (user == null)
            {
                Console.WriteLine("Invalid email or password");
            }
            else
            {
                Console.WriteLine("Signed in");
            }

        }

        private void bonustwo()
        {
            //write a query that finds the total of every users shopping cart products using linq.
            var cartTotal = _context.ShoppingCarts.Include(p => p.Product).Include(u => u.User).Select(p => p.Product.Price).Sum();
            Console.WriteLine(cartTotal.ToString());
            //display the total of each users shopping cart as well as the total of the toals to the console.
            foreach (ShoppingCart cart in _context.ShoppingCarts)
            {
                int user = cart.UserId;
                if (cart.UserId == user)
                {
                    var total = _context.ShoppingCarts.Where(u => u.UserId == user).ToList().Select(p => p.Product.Price).Sum();
                    Console.WriteLine($"User: {user} Total Cart: {total} ");
                   
                }
                else
                {
                    { user = cart.UserId; }
                }
            }

        }

        
        private void bonusthree()
        {
            //1.create functionality for a user to sign in via the console

            //2. if the user succesfully signs in
            // a.give them a menu where they perform the following actions within the console
            // view the products in their shopping cart
            // view all products in the products table
            // add a product to the shopping cart(incrementing quantity if that product is already in their shopping cart)
            // remove a product from their shopping cart
            // 3. if the user does not succesfully sing in
            // a.display "invalid email or password"
            // b.re - prompt the user for credentials

        }

    }
}
