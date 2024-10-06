using DBServiceDemo.Services;
using DBServiceDemo.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

class Program
{
	static void Main(string[] args)
	{
		try
		{
			var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
			optionsBuilder.UseSqlServer("Data Source=SUIKA;Initial Catalog=DBLWD;Integrated Security=True;Trust Server Certificate=True");
			using var context = new AppDbContext(optionsBuilder.Options);
			var service = new DbService(context);

			Console.WriteLine($"Connection created successfully {context.Database.CanConnect()}");

			while(true)
			{
				Console.WriteLine(

@"
What do you wanna do?
	1. Read
	2. Write
	3. Update
	4. Delete
	5. Execute your own query
	0. Exit
Please, choose a number.
");

				int operationNumber = int.Parse(Console.ReadLine());

				switch (operationNumber)
				{
					case 1:
						service.Read();
						break;

					case 2:
						service.Write();
						break;

					case 3:
						service.Update();
						break;

					case 4:
						service.Delete();
						break;

					case 5:
						service.Execute();
						break;

					case 0:
						goto endpoint;
				}
			}

			endpoint:
			Console.WriteLine("\nAdios!\n");
			
		}
		catch (Exception ex){
			Console.WriteLine(ex.Message);
		}

	}
}
