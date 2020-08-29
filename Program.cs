using System;
using System.Security;
using System.Threading.Tasks;

namespace Example_CSOM_NET_Standar
{
    class Program
    {
        public static async Task  Main(string[] args)
        {
            Console.WriteLine("Introduce el tenant");
            var tenant = Console.ReadLine();
            Console.WriteLine("Introduce el usuario");
            var user = Console.ReadLine();
            Console.WriteLine("Introduce el passworkd");
            var rawPassword = Console.ReadLine();
            Uri site = new Uri($"https://{tenant}.sharepoint.com/");
            
            SecureString password = new SecureString();
            foreach (char c in rawPassword) password.AppendChar(c);

            // Note: The PnP Sites Core AuthenticationManager class also supports this  
            using (var authenticationManager = new AuthenticationManager())
            using (var context = authenticationManager.GetContext(site, user, password))
            {
                context.Load(context.Web, p => p.Title);
                await context.ExecuteQueryAsync();
                Console.WriteLine($"Title: {context.Web.Title}");
            }
        }
    }
}
