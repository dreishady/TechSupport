/*
 * Author     : Ben Moir
 * Date       : 10/11/2017
 * Student ID : 5101965116
 * Known Bugs : None
 * Summary    : Account structures
 */

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Assessment3
{
    /// <summary>
    /// Describes an account's role
    /// </summary>
    public enum AccountRole
    {
        Customer,
        TechnicianLevel1,
        TechnicianLevel2,
        Administrator,
    }

    public class Account
    {
        /// <summary>
        /// The id number of the database entry bound to this instance
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Retrieves a list of all accounts
        /// </summary>
        public static IList<Account> AccountList
        {
            get
            {
                var list = new List<Account>();
                var ids = new List<int>();

                using (var command = new SqlCommand("select CustomerID from [Customers]", Database.Connection))
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        ids.Add(reader.GetInt32(0));

                foreach (var id in ids)
                {
                    Account account;
                    Find(id, out account);
                    list.Add(account);
                }

                return list;
            }
        }

        protected Account(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Returns a field from this account in the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public T GetField<T>(string fieldName)
        {
            T value = default(T);

            using (var command = new SqlCommand(connection: Database.Connection,
                cmdText: $"select {fieldName} from [Customers] where CustomerID = @Id"))
            {
                command.Parameters.Add(new SqlParameter("@Id", Id));

                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        value = reader.IsDBNull(0) ? default(T) : reader.GetFieldValue<T>(0);
            }

            return value;
        }

        /// <summary>
        /// Modifies a field from this account in the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public void SetField<T>(string fieldName, T value)
        {
            using (var command = new SqlCommand(connection: Database.Connection,
                cmdText: $"update [Customers] set {fieldName} = @Value where CustomerID = @Id"))
            {
                command.Parameters.Add(new SqlParameter("@Id", Id));
                command.Parameters.Add(new SqlParameter("@Value", value));
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Searches for and retrieves an account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool Find(int id, out Account account)
        {
            account = new Account(id);

            try
            {
                using (var command = new SqlCommand(connection: Database.Connection,
                    cmdText: $"select * from [Customers] where CustomerID = @Id"))
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) return true;
                        account = null;
                        return false;
                    }
                }
            }
            catch
            {
                return Find(id, out account);
            }
        }

        /// <summary>
        /// Adds and returns a new account
        /// </summary>
        /// <returns></returns>
        public static Account Add()
        {
            int id = -1;

            using (var command = new SqlCommand(connection: Database.Connection,
                cmdText: "insert into [Customers] (Name, Password, Role) values (@Name, @Password, @Role) select cast(scope_identity() as int)"))
            {
                command.Parameters.Add(new SqlParameter("@Name", ""));
                command.Parameters.Add(new SqlParameter("@Password", ""));
                command.Parameters.Add(new SqlParameter("@Role", SqlDbType.Int) { Value = 0 });
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        id = reader.GetInt32(0);
            }

            Account account;
            Find(id, out account);
            return account;
        }

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

        public string Password
        {
            get
            {
                return GetField<string>("Password");
            }

            set
            {
                SetField("Password", value);
            }
        }

        public string Email
        {
            get
            {
                return GetField<string>("Email");
            }

            set
            {
                SetField("Email", value);
            }
        }

        public string Phone
        {
            get
            {
                return GetField<string>("Phone");
            }

            set
            {
                SetField("Phone", value);
            }
        }

        public string Address
        {
            get
            {
                return GetField<string>("Address");
            }

            set
            {
                SetField("Address", value);
            }
        }

        public string City
        {
            get
            {
                return GetField<string>("City");
            }

            set
            {
                SetField("City", value);
            }
        }

        public string State
        {
            get
            {
                return GetField<string>("State");
            }

            set
            {
                SetField("State", value);
            }
        }

        public string ZipCode
        {
            get
            {
                return GetField<string>("ZipCode");
            }

            set
            {
                SetField("ZipCode", value);
            }
        }

        public AccountRole Role
        {
            get
            {
                return (AccountRole)GetField<int>("Role");
            }

            set
            {
                SetField("Role", (int)value);
            }
        }

        public override string ToString()
        {
            switch (Role)
            {
                default:
                case AccountRole.Customer:
                    return $"{Name} | {Id}";
                case AccountRole.Administrator:
                    return $"{Name} | {Id} (Admin)";
                case AccountRole.TechnicianLevel1:
                    return $"{Name} | {Id} (Level 1)";
                case AccountRole.TechnicianLevel2:
                    return $"{Name} | {Id} (Level 2)";
            }
        }
    }
}
