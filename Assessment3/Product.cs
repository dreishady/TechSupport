using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Assessment3
{
    public class Product
    {
        /// <summary>
        /// The product code that binds this instance to the database
        /// </summary>
        public string Code { get; protected set; }

        public string Name
        {
            get
            {
                return GetField<string>("Name");
            }

            set
            {
                SetField("Name", value);
            }
        }

        public decimal Version
        {
            get
            {
                return GetField<decimal>("Version");
            }

            set
            {
                SetField("Version", value);
            }
        }

        public DateTime ReleaseDate
        {
            get
            {
                return GetField<DateTime>("ReleaseDate");
            }

            set
            {
                SetField("ReleaseDate", value);
            }
        }

        /// <summary>
        /// Retrieves a list of all products
        /// </summary>
        public static IList<Product> ProductList
        {
            get
            {
                var list = new List<Product>();
                var codes = new List<string>();

                using (var command = new SqlCommand("select ProductCode from [Products]", Database.Connection))
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        codes.Add(reader.GetString(0));

                foreach (var code in codes)
                {
                    Product product;
                    Find(code, out product);
                    list.Add(product);
                }

                return list;
            }
        }

        protected Product(string code)
        {
            Code = code;
        }

        /// <summary>
        /// Retrieves a field from this product
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public T GetField<T>(string fieldName)
        {
            T value = default(T);

            using (var command = new SqlCommand(connection: Database.Connection,
                cmdText: $"select {fieldName} from [Products] where ProductCode = @Code"))
            {
                command.Parameters.Add(new SqlParameter("@Code", Code));

                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        value = reader.IsDBNull(0) ? default(T) : reader.GetFieldValue<T>(0);
            }

            return value;
        }

        /// <summary>
        /// Modifies a field from this product
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public void SetField<T>(string fieldName, T value)
        {
            using (var command = new SqlCommand(connection: Database.Connection,
                cmdText: $"update [Products] set {fieldName} = @Value where ProductCode = @Code"))
            {
                command.Parameters.Add(new SqlParameter("@Code", Code));
                command.Parameters.Add(new SqlParameter("@Value", value));
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Searches for and retrieves a product
        /// </summary>
        /// <param name="code"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public static bool Find(string code, out Product product)
        {
            product = new Product(code);

            using (var command = new SqlCommand(connection: Database.Connection,
                cmdText: $"select * from [Products] where ProductCode = @Code"))
            {
                command.Parameters.Add(new SqlParameter("@Code", code));

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows) return true;
                    product = null;
                    return false;
                }
            }
        }

        /// <summary>
        /// Adds and returns a new product
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Product Add(string code)
        {
            using (var command = new SqlCommand(connection: Database.Connection,
                cmdText: "insert into [Products] (ProductCode, Name, Version, ReleaseDate) values (@ProductCode, @Name, @Version, @ReleaseDate)"))
            {
                command.Parameters.Add(new SqlParameter("@ProductCode", code));
                command.Parameters.Add(new SqlParameter("@Name", ""));
                command.Parameters.Add(new SqlParameter("@Version", 1));
                command.Parameters.Add(new SqlParameter("@ReleaseDate", DateTime.Now) { SqlDbType = SqlDbType.DateTime });
                command.ExecuteNonQuery();
            }

            Product product;
            Find(code, out product);
            return product;
        }

        public override string ToString()
        {
            return $"{Name} | {Code}";
        }
    }
}
