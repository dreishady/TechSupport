using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Assessment3
{
    public class Incident
    {
        /// <summary>
        /// The id number of the database entry bound to this instance
        /// </summary>
        public int Id { get; protected set; }

        public int CustomerID
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

        public int TechID
        {
            get
            {
                return GetField<int>("TechID");
            }

            set
            {
                SetField("TechID", value);
            }
        }

        public DateTime DateOpened
        {
            get
            {
                return GetField<DateTime>("DateOpened");
            }

            set
            {
                SetField("DateOpened", value);
            }
        }

        public DateTime DateClosed
        {
            get
            {
                return GetField<DateTime>("DateClosed");
            }

            set
            {
                SetField("DateClosed", value);
            }
        }

        public string Title
        {
            get
            {
                return GetField<string>("Title");
            }

            set
            {
                SetField("Title", value);
            }
        }

        public string Description
        {
            get
            {
                return GetField<string>("Description");
            }

            set
            {
                SetField("Description", value);
            }
        }

        /// <summary>
        /// Retrieves a list of all incidents
        /// </summary>
        public static IList<Incident> IncidentList
        {
            get
            {
                var list = new List<Incident>();
                var ids = new List<int>();

                using (var command = new SqlCommand("select IncidentID from [Incidents]", Database.Connection))
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        ids.Add(reader.GetInt32(0));

                foreach (var id in ids)
                {
                    Incident incident;
                    Find(id, out incident);
                    list.Add(incident);
                }

                return list;
            }
        }

        protected Incident(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Retrieves a field from this incident
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public T GetField<T>(string fieldName)
        {
            T value = default(T);

            using (var command = new SqlCommand(connection: Database.Connection,
                cmdText: $"select {fieldName} from [Incidents] where IncidentID = @Id"))
            {
                command.Parameters.Add(new SqlParameter("@Id", Id));

                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        value = reader.IsDBNull(0) ? default(T) : reader.GetFieldValue<T>(0);
            }

            return value;
        }

        /// <summary>
        /// Modifies a field from this incident
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public void SetField<T>(string fieldName, T value)
        {
            using (var command = new SqlCommand(connection: Database.Connection,
                cmdText: $"update [Incidents] set {fieldName} = @Value where IncidentID = @Id"))
            {
                command.Parameters.Add(new SqlParameter("@Id", Id));
                command.Parameters.Add(new SqlParameter("@Value", value));
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Searches for and retrieves an incident
        /// </summary>
        /// <param name="id"></param>
        /// <param name="incident"></param>
        /// <returns></returns>
        public static bool Find(int id, out Incident incident)
        {
            incident = new Incident(id);

            using (var command = new SqlCommand(connection: Database.Connection,
                cmdText: $"select * from [Incidents] where IncidentID = @Id"))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows) return true;
                    incident = null;
                    return false;
                }
            }
        }

        /// <summary>
        /// Adds and returns a new incident
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public static Incident Add(int customerId, string productCode)
        {
            int id = -1;

            using (var command = new SqlCommand(connection: Database.Connection,
                cmdText: "insert into [Incidents] (CustomerID, ProductCode, DateOpened, Title, Description) values (@CustomerID, @ProductCode, @DateOpened, @Title, @Description) select cast(scope_identity() as int)"))
            {
                command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Int) { Value = customerId });
                command.Parameters.Add(new SqlParameter("@ProductCode", productCode));
                command.Parameters.Add(new SqlParameter("@DateOpened", DateTime.Now) { SqlDbType = SqlDbType.DateTime });
                command.Parameters.Add(new SqlParameter("@Title", ""));
                command.Parameters.Add(new SqlParameter("@Description", ""));
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        id = reader.GetInt32(0);
            }

            Incident incident;
            Find(id, out incident);
            return incident;
        }
    }
}
