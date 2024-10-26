/* Willy Betancur
 Ingeniería de Sistemas
 Grupo 76
*/

using System;
using System.Collections.Generic;

namespace StoreProject
{
    public class Product
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double UnitPrice { get; private set; }
        public int StockQuantity { get; private set; }

        public Provider Provider { get; set; }

        public Product(string code, string name, string description, double unitPrice, int stockQuantity, Provider provider)
        {
            Code = code;
            Name = name;
            Description = description;
            UnitPrice = unitPrice;
            StockQuantity = stockQuantity;
            Provider = provider;
        }
    }

    public class Provider
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<Product> ProductList { get; private set; } = new List<Product>();

        public Provider(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public void AddProduct(Product product)
        {
            ProductList.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            ProductList.Remove(product);
        }
    }

    public class Order
    {
        public int ID { get; set; }
        public Provider Provider { get; set; }
        public Employee Employee { get; set; }
        public Dictionary<Product, int> OrderedProducts { get; private set; } = new Dictionary<Product, int>();

        public Order(int id, Provider provider, Employee employee)
        {
            ID = id;
            Provider = provider;
            Employee = employee;
        }

        public void AddProduct(Product product, int quantity)
        {
            if (OrderedProducts.ContainsKey(product))
                OrderedProducts[product] += quantity;
            else
                OrderedProducts[product] = quantity;
        }

        public void RemoveProduct(Product product)
        {
            OrderedProducts.Remove(product);
        }
    }

    public abstract class Employee
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Role { get; private set; }

        protected Employee(int id, string name, string role)
        {
            ID = id;
            Name = name;
            Role = role;
        }
    }

    public class Administrator : Employee
    {
        public Administrator(int id, string name) : base(id, name, "Administrador") { }
    }

    public class WarehouseManager : Employee
    {
        public WarehouseManager(int id, string name) : base(id, name, "Gerente Almacen") { }
    }

    public class Program
    {
        private static List<Product> products = new List<Product>();
        private static List<Provider> providers = new List<Provider>();
        private static List<Employee> employees = new List<Employee>();
        private static List<Order> orders = new List<Order>();

        public static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
{
    Console.Clear();
    Console.WriteLine("\n|---------------------------- Sistema de Gestión de Bodega ----------------------------|");
    Console.WriteLine("|--------------------------------------------------------------------------------------|");
    Console.WriteLine("| Opción |                                Descripción                                 |");
    Console.WriteLine("|--------|---------------------------------------------------------------------------|");
    Console.WriteLine("|   1    | Agregar Proveedor                                                          |");
    Console.WriteLine("|   2    | Agregar Producto                                                           |");
    Console.WriteLine("|   3    | Agregar Empleado                                                           |");
    Console.WriteLine("|   4    | Crear Pedido                                                               |");
    Console.WriteLine("|   5    | Mostrar Proveedores                                                        |");
    Console.WriteLine("|   6    | Mostrar Productos                                                          |");
    Console.WriteLine("|   7    | Mostrar Empleados                                                          |");
    Console.WriteLine("|   8    | Mostrar Pedidos                                                            |");
    Console.WriteLine("|   9    | Salir                                                                      |");
    Console.WriteLine("|--------------------------------------------------------------------------------------|");
    
    Console.Write("Elige una opción: ");
    string option = Console.ReadLine() ?? "9"; // Predeterminado "9" en caso de valor nulo para salir

    switch (option)
                {
                    case "1":
                        AddProvider();
                        break;
                    case "2":
                        AddProduct();
                        break;
                    case "3":
                        AddEmployee();
                        break;
                    case "4":
                        CreateOrder();
                        break;
                    case "5":
                        DisplayProviders();
                        break;
                    case "6":
                        DisplayProducts();
                        break;
                    case "7":
                        DisplayEmployees();
                        break;
                    case "8":
                        DisplayOrders();
                        break;
                    case "9":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

    if (!exit)
    {
        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
        Console.ReadKey();
    }
}
        }

        private static void AddProvider()
        {
            Console.Write("Ingrese ID del Proveedor: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Ingrese el Nombre del Proveedor: ");
            string name = Console.ReadLine();

            Provider provider = new Provider(id, name);
            providers.Add(provider);

            Console.WriteLine("Proveedor Agregado Exitosamente.");
        }

        private static void AddProduct()
        {
            Console.Write("Ingrese el Código del Producto: ");
            string code = Console.ReadLine();
            Console.Write("Ingrese Nombre del Producto: ");
            string name = Console.ReadLine();
            Console.Write("Ingrese Descripción: ");
            string description = Console.ReadLine();
            Console.Write("Ingrese Precio de Unidad: ");
            double unitPrice = double.Parse(Console.ReadLine());
            Console.Write("Ingrese Cantidad en Stock: ");
            int stockQuantity = int.Parse(Console.ReadLine());

            Console.WriteLine("Seleccionar proveedor por ID:");
            foreach (var provider in providers)
            {   
                Console.WriteLine($"ID: {provider.ID}, Name: {provider.Name}");
            }

            int providerId = int.Parse(Console.ReadLine());
            Provider selectedProvider = providers.Find(p => p.ID == providerId);

            if (selectedProvider != null)
            {
                Product product = new Product(code, name, description, unitPrice, stockQuantity, selectedProvider);
                products.Add(product);
                selectedProvider.AddProduct(product);
                Console.WriteLine("Producto agregado exitosamente.");
            }
            else
            {
                Console.WriteLine("Producto no funciona.");
            }
        }

        private static void AddEmployee()
        {
            Console.Write("Ingrese el ID del Empleado: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Ingrese el nombre del Empleado: ");
            string name = Console.ReadLine();
            Console.WriteLine("Seleccionar Rol del empleado (1 para Administrator, 2 Gerente Almacen): ");
            string roleOption = Console.ReadLine();

            Employee employee = roleOption == "1" ? new Administrator(id, name) : new WarehouseManager(id, name);
            employees.Add(employee);

            Console.WriteLine("Empleado agregado exitosamente!");
        }

        private static void CreateOrder()
        {
            Console.Write("Ingrese ID del Pedido: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Seleccionar ID del Proveedor:");
            foreach (var provider in providers)
            {
                Console.WriteLine($"ID: {provider.ID}, Name: {provider.Name}");
            }

            int providerId = int.Parse(Console.ReadLine());
            Provider selectedProvider = providers.Find(p => p.ID == providerId);

            Console.WriteLine("Seleccionar ID del Empelado: ");
            foreach (var employee in employees)
            {
                Console.WriteLine($"ID: {employee.ID}, Name: {employee.Name}");
            }

            int employeeId = int.Parse(Console.ReadLine());
            Employee selectedEmployee = employees.Find(e => e.ID == employeeId);

            if (selectedProvider != null && selectedEmployee != null)
            {
                Order order = new Order(id, selectedProvider, selectedEmployee);
                
                Console.WriteLine("Agregar productos a la orden (Escriba 'Listo' cuando haya finalizado):");
                while (true)
                {
                    Console.WriteLine("Seleccionar Código del Producto:");
                    foreach (var product in products)
                    {
                        Console.WriteLine($"Code: {product.Code}, Name: {product.Name}");
                    }

                    string productCode = Console.ReadLine();
                    if (productCode.ToLower() == "Listo") break;

                    Product selectedProduct = products.Find(p => p.Code == productCode);

                    if (selectedProduct != null)
                    {
                        Console.Write("Ingrese Cantidad: ");
                        int quantity = int.Parse(Console.ReadLine());
                        order.AddProduct(selectedProduct, quantity);
                    }
                    else
                    {
                        Console.WriteLine("Producto no Funciona.");
                    }
                }
                
                orders.Add(order);
                Console.WriteLine("Pedido creado Exitosamente.");
            }
            else
            {
                Console.WriteLine("Proveedor o empleado no funciona.");
            }
        }

        private static void DisplayProviders()
        {
            Console.WriteLine("Proveedores:");
            foreach (var provider in providers)
            {
                Console.WriteLine($"ID: {provider.ID}, Name: {provider.Name}");
            }
        }

        private static void DisplayProducts()
{
    Console.WriteLine("| Código | Nombre             | Proveedor         | Precio Unitario | Cantidad en Stock |");
    Console.WriteLine("|--------|---------------------|-------------------|-----------------|-------------------|");

    double totalInventoryValue = 0;

    foreach (var product in products)
    {
        double productTotalValue = product.UnitPrice * product.StockQuantity;
        totalInventoryValue += productTotalValue;

        Console.WriteLine($"| {product.Code,-6} | {product.Name,-19} | {product.Provider.Name,-17} | {product.UnitPrice,14:C} | {product.StockQuantity,17} |");
    }

    Console.WriteLine("\nValor total del inventario: " + totalInventoryValue.ToString("C"));
}

        private static void DisplayEmployees()
        {
            Console.WriteLine("Empleados:");
            foreach (var employee in employees)
            {
                Console.WriteLine($"ID: {employee.ID}, Name: {employee.Name}, Role: {employee.Role}");
            }
        }

        private static void DisplayOrders()
        {
            Console.WriteLine("Pedidos:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.ID}, Provider: {order.Provider.Name}, Employee: {order.Employee.Name}");
                foreach (var item in order.OrderedProducts)
                {
                    Console.WriteLine($"Product: {item.Key.Name}, Quantity: {item.Value}");
                }
            }
        }
    }
}