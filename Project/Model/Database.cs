using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;
using System.Data;

namespace Labo06_1.Model
{
    class Database
    {

        //vooraf: instellingen ophalen uit config-bestand
        private static ConnectionStringSettings ConnectionString
        {
            get
            {
                //met onderstaande lijn haalt hij alle informatie uit het configuratiebestand op
                //dat te maken heeft met de cnnectionstring met naarm"connectionString"
                return ConfigurationManager.ConnectionStrings["ConnectionString"];
            }

        }
        //stap1: connectie opvragen
        public static DbConnection GetConnection()
        {
            DbConnection con = DbProviderFactories.GetFactory(ConnectionString.ProviderName).CreateConnection();
            con.ConnectionString = ConnectionString.ConnectionString;

            //de connectie openen om te gebruiken
            con.Open();
            return con;
        }
        // stap2: connectie vrijgeven
        public static void ReleaseConnection(DbConnection con)
        {
            if (con != null)
            {
                con.Close();
                con = null;
            }
        }

        // stap3: command gaan opstellen: sql-string & parameters doorgeven
        // operking: keyword params laat toe om deze methode op te roepen met slechts 1 parameter, nl. de sql-string
        private static DbCommand BuildCommand(String sql, params DbParameter[] parameters)
        {
            //intern in deze methode gaan we connectie leggen met de database
            DbCommand command = GetConnection().CreateCommand();
            //command -> boodschappenlijstje
            command.CommandType = System.Data.CommandType.Text;

            //sql-string!
            command.CommandText = sql;

            //parameters doorgeven
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }


            return command;
        }
        // stap 3bis: hulpmethode om parameters te maken
        //deze methode maakt een parameter aan ( die dan later kan doorgegeven worden via de methode buildcommand)
        public static DbParameter AddParameter(String naam, object value)
        {
            DbParameter par = DbProviderFactories.GetFactory(ConnectionString.ProviderName).CreateParameter();
            par.ParameterName = naam;
            par.Value = value;

            return par;

        }
        //stap 4-a: Data ophalen(select-statements)
        public static DbDataReader GetData(string sql, params DbParameter[] parameters)
        {
            //zie vorige methode(s)
            DbCommand command = null;
            DbDataReader reader = null;


            try
            {
                command = BuildCommand(sql, parameters);

                /*op onderstaande lijn word naar de database gegaan,
                 * en wordt met een datareader teruggekeerd.*/
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch(Exception ex)
            {
                //afprinten wat er verkeerd is
                Console.WriteLine(ex.Message);
                if (reader != null) reader.Close();
                if (command != null) ReleaseConnection(command.Connection);

                //fout doorgeven aan de aanroeper
                throw ex;
                
            }           
        }
        //stap 4-b: Database gaan wijzigen(insert-delete-update)
        public static int ModifyData(String sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            try
            {
                command = BuildCommand(sql, parameters);
                int aantalRijenGewijzigd = command.ExecuteNonQuery();

                ReleaseConnection(command.Connection);
                /*aantal verwijderde toegevoegde of aangepaste rijen worden teruggegeven
                 * zo heeft de gebruiker controle of het gelukt is of niet*/
                return aantalRijenGewijzigd;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (command != null) ReleaseConnection(command.Connection);
                throw ex;
            }
            
        }

        //extra: werken met transacties
        //vooraf: Transactie aanmaken(waarin alle commando's ofwel lukken,ofwel nietlukken)
        public static DbTransaction BeginTransaction()
        {
            DbConnection con = null;

            try
            {
                con = GetConnection();

                //transactie aanmaken en teruggeven
                return con.BeginTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (con != null) ReleaseConnection(con);
                throw (ex);
            }
        }
        //stap3 extra: command ifv transactie
        private static DbCommand BuildCommand(DbTransaction trans, String sql, params DbParameter[] parameters)
        {
            //intern in deze methode gaan we connectie leggen met de database
            DbCommand command = trans.Connection.CreateCommand();
            //command -> boodschappenlijstje
            command.CommandType = System.Data.CommandType.Text;

            //sql-string!
            command.CommandText = sql;

            //parameters doorgeven
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }


            return command;
        }
        //stap 4 extra A: data ophalen binnen in een transactie
        public static DbDataReader GetData(DbTransaction trans,string sql, params DbParameter[] parameters)
        {
            //zie vorige methode(s)
            DbCommand command = null;
            DbDataReader reader = null;


            try
            {
                command = BuildCommand(trans,sql, parameters);

                /*op onderstaande lijn word naar de database gegaan,
                 * en wordt met een datareader teruggekeerd.*/
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                //afprinten wat er verkeerd is
                Console.WriteLine(ex.Message);
                if (reader != null) reader.Close();
                if (command != null) ReleaseConnection(command.Connection);

                //fout doorgeven aan de aanroeper
                throw ex;

            }
        }
        //stap 4 extra B: data wijzigen binnen een transactie
        public static int ModifyData(DbTransaction trans,String sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            try
            {
                command = BuildCommand(trans,sql, parameters);
                int aantalRijenGewijzigd = command.ExecuteNonQuery();

                ReleaseConnection(command.Connection);
                /*aantal verwijderde toegevoegde of aangepaste rijen worden teruggegeven
                 * zo heeft de gebruiker controle of het gelukt is of niet*/
                return aantalRijenGewijzigd;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (command != null) ReleaseConnection(command.Connection);
                throw ex;
            }

        }
    }
}
