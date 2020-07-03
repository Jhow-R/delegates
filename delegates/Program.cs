using System;
using System.Linq;
using System.Collections.Generic;

namespace delegates
{
    class Program
    {
        delegate double BinaryNumericOperationDouble(double n1, double n2);
        delegate void BinaryNumericOperationVoid(double n1, double n2);

        static void Main(string[] args)
        {
            double a = 10;
            double b = 12;

            //BinaryNumericOperation op = new BinaryNumericOperation(CalculationService.Sum);
            BinaryNumericOperationDouble op1 = CalculationService.Sum;
            Console.WriteLine(CalculationService.Sum(a, b));

            Console.WriteLine(op1(a, b));
            //Ou
            Console.WriteLine(op1.Invoke(a, b));


            Console.WriteLine("\nMulticast delegates");
            BinaryNumericOperationVoid op2 = CalculationService.ShowSum;
            op2 += CalculationService.ShowMax;
            op2(a, b);

            Console.Write("\n");

            List<Product> list = new List<Product>();
            list.Add(new Product("Tv", 900.00));
            list.Add(new Product("Mouse", 50.00));
            list.Add(new Product("Tablet", 350.50));
            list.Add(new Product("HD Case", 80.90));

            Console.WriteLine("\nRemovendo produtos");
            // Usando delegate Predicate para remover itens da lista

            Predicate<Product> predicate = ProductTest;
            list.RemoveAll(predicate);
            // Ou
            list.RemoveAll(ProductTest);
            // Ou
            list.RemoveAll(p => p.Price >= 100.0);

            foreach (Product p in list)
                Console.WriteLine(p);

            Console.WriteLine("\nAlterando preço dos produtos");
            list.Add(new Product("Tv", 900.00));
            list.Add(new Product("Tablet", 350.50));

            // Usando Action para aumentar preço de cada produto
            Action<Product> act = UpdatePrice;
            // Ou
            act = p => { p.Price *= 1.1; };

            list.ForEach(act);
            // Ou
            list.ForEach(UpdatePrice);
            // Ou
            list.ForEach(p => p.Price *= 1.1);

            foreach (Product p in list)
                Console.WriteLine(p);

            Console.WriteLine("\nAlterando nome dos produtos");

            // Usando Func para gerar uma nova lista com os nomes em caixa alta
            Func<Product, string> func = NameUpper;
            // Ou
            func = p => { return p.Name.ToUpper(); };
            // Ou
            func = p => p.Name.ToUpper();

            IEnumerable<string> result = list.Select(func);
            // Ou
            result = list.Select(NameUpper);
            // Ou
            result = list.Select(p => p.Name.ToUpper());

            foreach (string s in result)
                Console.WriteLine(s);

            Console.ReadLine();
        }

        static string NameUpper(Product product)
        {
            return product.Name.ToUpper();
        }

        static void UpdatePrice(Product product)
        {
            product.Price *= 1.1;
        }

        public static bool ProductTest(Product product)
        {
            return product.Price >= 100.0;
        }
    }
}
