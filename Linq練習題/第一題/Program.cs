using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace 第一題
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            List<ProductClass> product = new List<ProductClass>();
            using (var reader = new StreamReader(@"C:\Users\dan\Downloads/product.csv"))
            {
                bool one_col = false;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    //Console.WriteLine(values[0]);
                    if(one_col) 
                    {
                        var prod = new ProductClass
                        {
                            productNumber = values[0],
                            productName = values[1],
                            productCount = 0,
                            productPrice = 0,
                            productClassify  = values[4],
                        };
                        int count = 0;
                        decimal price = 0;
                        if(int.TryParse(values[2],out count))
                        {
                            prod.productCount= count;
                        }
                        if (decimal.TryParse(values[3], out price))
                        {
                            prod.productPrice= price;
                        }
                        product.Add(prod);
                    }
                    one_col=true;
                    
                }


                
                
               
                /*
                foreach (var p in product)
                {
                    Console.WriteLine($"{p.productNumber}, {p.productName}, {p.productCount}, {p.productPrice}, {p.productClassify}");
                }
                */
                decimal sum_price = product.Sum(x=>x.productPrice);
                Console.WriteLine($"計算所有商品的總價格: {sum_price}");
                decimal average_price = product.Average(x => x.productPrice);
                Console.WriteLine($"計算所有商品的平均價格: {average_price:N2}");
                int sum_count = product.Sum(x => x.productCount);
                Console.WriteLine($"計算所有商品的總數量: {sum_count}");
                double average_count = product.Average(x => x.productCount);
                Console.WriteLine($"計算所有商品的平均數量: {average_count:N2}");
                //decimal max_expensive = product.Max(x => x.productPrice);

                Console.WriteLine($"找出哪一項商品最貴: ");
                var maxPrice = product.Max(x => x.productPrice);
                var maxExpensiveProduct = product.Where(x => x.productPrice == maxPrice);
                foreach (var item in maxExpensiveProduct)
                {
                    Console.Write($"{item.productName} ");
                }
                Console.WriteLine();

                Console.WriteLine($"找出哪一項商品最便宜: ");
                var minPrice = product.Min(x => x.productPrice);
                var minCheapProduct = product.Where(x => x.productPrice == minPrice);
                foreach (var item in minCheapProduct)
                {
                    Console.Write($"{item.productName} ");
                }
                Console.WriteLine();
                //var minExpensiveProduct = product.OrderBy(x => x.productPrice).First();
              
               
                var threeC_product=product.Where(x=>x.productClassify=="3C");
                decimal sumprice = 0m;
                foreach(var item in threeC_product)
                {
                    sumprice += (item.productPrice * item.productCount);
                }
                Console.WriteLine($"計算產品類別為 3C 的商品總價: {sumprice}");

                sumprice = 0;
                var DrinksAndFood_product= product.Where(x => (x.productClassify == "飲料"|| x.productClassify == "食品"));
                foreach(var item in DrinksAndFood_product)
                {
                    sumprice+= (item.productPrice * item.productCount);
                }
                Console.WriteLine($"計算產品類別為飲料及食品的商品總價: {sumprice}");

                Console.WriteLine($"找出所有商品類別為食品，而且商品數量大於 100 的商品: ");
                var Product_100_Food = product.Where(x => x.productClassify == "食品").Where(x => x.productCount >= 100);
                foreach(var item in Product_100_Food)
                {
                    Console.Write(item.productName+" ");
                }
                Console.Write("\n");

                var More100Product = product.Where(x => x.productPrice > 1000);
                var distinctclassify = More100Product.Select(x=>x.productClassify).Distinct();
                Console.WriteLine($"找出各個商品類別底下有哪些商品的價格是大於 1000 的商品: ");
                foreach (var item in distinctclassify)
                {
                    Console.Write(item+" "); 
                }
                Console.Write("\n");

                var Average_Price  =More100Product.Average(x => x.productPrice);
                Console.WriteLine($"呈上題，請計算該類別底下所有商品的平均價格: {Average_Price}");

                Console.WriteLine($"依照商品價格由高到低排序: ");
                var HightToLowPrice = product.OrderByDescending(x => x.productPrice);
                foreach(var item in HightToLowPrice) 
                {
                    Console.Write(item.productName+",");
                }
                Console.Write("\n");
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine($"依照商品數量由低到高排序: ");
                var LowToHightCount = product.OrderBy(x => x.productCount);
                foreach(var item in LowToHightCount) 
                {
                    Console.Write(item.productName + ",");
                }
                Console.Write("\n");

                

                Console.WriteLine($"找出各商品類別底下，最貴的商品: ");
                var maxPriceByCategory = product
                .GroupBy(p => p.productClassify)
                .Select(g => new {
                    Category = g.Key,
                    MaxPrice = g.Max(p => p.productPrice),
                    ProductName = g.First(p => p.productPrice == g.Max(p2 => p2.productPrice)).productName
                });
                foreach(var item in maxPriceByCategory) 
                {
                    Console.WriteLine(item);
                }
                Console.Write("\n");

                Console.WriteLine($"找出各商品類別底下，最便宜的商品: ");
                var minPriceByCategory = product
                .GroupBy(p => p.productClassify)
                .Select(g => new {
                    Category = g.Key,
                    MinPrice = g.Min(p => p.productPrice),
                    ProductName = g.First(p => p.productPrice == g.Min(p2 => p2.productPrice)).productName
                });
                foreach (var item in minPriceByCategory)
                {
                    Console.WriteLine(item);
                }
                Console.Write("\n");

                Console.WriteLine($"找出價格小於等於 10000 的商品: ");
                var Min100Price = product.Where(p => p.productPrice <= 1000);
                foreach(var item in Min100Price)
                {
                    Console.WriteLine(item.productName);
                }
                Console.WriteLine();

                Console.WriteLine($"製作一頁 4 筆總共 5 頁的分頁選擇器");
                int pageNumber = 1;
                int pageSize = 4;
                var pageProducts = product.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                while (true)
                {
                    Console.WriteLine($"第{pageNumber}頁---");
                    pageProducts = product.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    foreach (var prod in pageProducts)
                    {
                        Console.WriteLine($"商品編號{prod.productNumber},商品名稱{prod.productName},商品數量{prod.productCount}," +
                            $"價格{prod.productPrice},商品類別{prod.productClassify}");
                    }
                    Console.WriteLine();
                    Console.Write("輸入(n:下一頁,p上一頁,q:退出)");
                    string command = Console.ReadLine();
                    if (command.ToLower() == "n")
                    {
                        if (pageNumber < 5)
                        {
                            pageNumber++;
                        }
                    }
                    else if (command.ToLower() == "p")
                    {
                        if (pageNumber > 1)
                        {
                            pageNumber--;
                        }
                    }
                    else if (command.ToLower() == "q")
                    {
                        break;
                    }

                }

                /*
                int pageNumber = 1;
                int pageSize = 4;
                var pageProducts = product.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                foreach (var prod in pageProducts)
                {
                    Console.WriteLine($"商品編號{prod.productNumber},商品名稱{prod.productName},商品數量{prod.productCount}," +
                        $"價格{prod.productPrice},商品類別{prod.productClassify}");
                }
                */
                /*
                int TotalPage = 5;
                int currentPage = 1;
                int pageCount = (int)Math.Ceiling((double)product.Count / TotalPage);
                Console.WriteLine(product.Count);
                

                Console.WriteLine(product.ToArray());
                
                while (true)
                {
                    Console.WriteLine($"分頁{currentPage}");
                    for (int i = (currentPage - 1) * TotalPage; i < currentPage * TotalPage && i < product.Count; i++)
                    {
                        foreach (var p in product)
                        {
                            Console.WriteLine($"{p.productNumber}, {p.productName}, {p.productCount}, {p.productPrice}, {p.productClassify}");
                        }
                    }

                    Console.WriteLine();
                    Console.Write("Enter command (n: next, p: previous, q: quit): ");

                    string command = Console.ReadLine();

                    if (command.ToLower() == "n")
                    {
                        if (currentPage < pageCount)
                        {
                            currentPage++;
                        }
                    }
                    else if (command.ToLower() == "p")
                    {
                        if (currentPage > 1)
                        {
                            currentPage--;
                        }
                    }
                    else if (command.ToLower() == "q")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid command.");
                    }
                }
                */
                /*
                foreach (var item in result)
                {
                    Console.WriteLine(item.Key);
                    
                    foreach (var value in item)
                    {
                        if(value.productName==item.Key)
                        Console.WriteLine(value.productName);
                       
                    }
                }
                */
                /*
                
                
                
                Console.WriteLine($"製作一頁 4 筆總共 5 頁的分頁選擇器");
                */
            }

        

        }
       
    }
}
