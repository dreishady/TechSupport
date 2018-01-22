/*
 * Author     : Ben Moir
 * Date       : 10/11/2017
 * Student ID : 5101965116
 * Known Bugs : None
 * Summary    : Registration structures
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Assessment3
{
    public class Registration
    {
        /// <summary>
        /// The id number of the database entry bound to this instance
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Retrieves a list of all registrations
        /// </summary>
        public static IList<Registration> RegistrationList
        {
            get
            {
                var list = new List<Registration>();
                var ids = new List<int>();

                using (var command = new SqlCommand("select RegistrationID from [Registrations]", Database.Connection))
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        ids.Add(reader.GetInt32(0));

                foreach (var id in ids)
                {
                    Registration registration;
                    Find(id, out registration);
                    list.Add(registration);
                }

                return list;
            }
        }

        protected Registration(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Returns a field from this registration in the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public T GetField<T>(string fieldName)
        {
            T value = default(T);

            using (var command = new SqlCommand(connection: Database.Connection,
                cmdText: $"select {fieldName} from [Registrations] where RegistrationID = @Id"))
            {
                command.Parameters.Add(new SqlParameter("@Id", Id));

                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        value = reader.IsDBNull(0) ? default(T) : reader.GetFieldValue<T>(0);
            }

            return value;
        }

        /// <summary>
        /// Modifies a field from this registration in the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public void SetField<T>(string fieldName, T value)
        {
            using (var command = new SqlCommand(connection: Database.Connection,
                cmdText: $"update [Registrations] set {fieldName} = @Value where RegistrationID = @Id"))
            {
                command.Parameters.Add(new SqlParameter("@Id", Id));
                command.Parameters.Add(new SqlParameter("@Value", value));
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Searches for and retrieves a registration
        /// </summary>
        /// <param name="id"></param>
        /// <param name="registration"></param>
        /// <returns></returns>
        public static bool Find(int id, out Registration registration)
        {
            registration = new Registration(id);

            try
            {
                using (var command = new SqlCommand(connection: Database.Connection,
                    cmdText: $"select * from [Registrations] where RegistrationID = @Id"))
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) return true;
                        registration = null;
                        return false;
                    }
                }
            }
            catch
            {
                return Find(id, out registration);
            }
        }

        /// <summary>
        /// Adds and returns a new registration
        /// </summary>
        /// <returns></returns>
        public static Registration Add(int customerId, string productCode, DateTime registrationDate)
        {
            int id = -1;

            using (var command = new SqlCommand(connection: Database.Connection,
                cmdText: "insert into [Registrations] (CustomerID, ProductCode, RegistrationDate) values (@CustomerID, @ProductCode, @RegistrationDate) select cast(scope_identity() as int)"))
            {
                command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int) { Value = customerId });
                command.Parameters.Add(new SqlParameter("@ProductCode", productCode));
                command.Parameters.Add(new SqlParameter("@RegistrationDate", SqlDbType.DateTime) { Value = registrationDate });
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        id = reader.GetInt32(0);
            }

            Registration registration;
            Find(id, out registration);
            return registration;
        }

        public int RegistrationID
        {
            get
            {
                return GetField<int>("RegistrationID");
            }

            set
            {
                SetField("RegistrationID", value);
            }
        }

        public int CustomerId
        {
            get
            {
                return GetField<int>("CustomerID");
            }

            set
            {
                SetField("CustomerID", value);
            }
        }

        public string ProductCode
        {
            get
            {
                return GetField<string>("ProductCode");
            }

            set
            {
                SetField("ProductCode", value);
            }
        }

        public DateTime RegistrationDate
        {
            get
            {
                return GetField<DateTime>("RegistrationDate");
            }

            set
            {
                SetField("RegistrationDate", value);
            }
        }
    }
}
