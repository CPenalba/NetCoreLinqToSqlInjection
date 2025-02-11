using Microsoft.AspNetCore.Http.HttpResults;
using NetCoreLinqToSqlInjection.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using static Azure.Core.HttpHeader;

#region PROCEDIMIENTOS ALMACENADOS
//create or replace procedure sp_delete_doctor
//(p_iddoctor DOCTOR.DOCTOR_NO%TYPE)
//as begin
//delete from DOCTOR where DOCTOR_NO=p_iddoctor;
//commit;
//end;

//CREATE OR REPLACE PROCEDURE SP_ACTUALIZAR_DOCTOR(
//    idhospital DOCTOR.HOSPITAL_COD%TYPE,
//    iddoctor DOCTOR.DOCTOR_NO%TYPE,
//    p_apellido DOCTOR.APELLIDO%TYPE,
//    p_especialidad DOCTOR.ESPECIALIDAD%TYPE,
//    p_salario DOCTOR.SALARIO%TYPE
//)
//AS
//BEGIN
//    UPDATE DOCTOR
//    SET HOSPITAL_COD = idhospital,
//        APELLIDO = p_apellido,
//        ESPECIALIDAD = p_especialidad,
//        SALARIO = p_salario
//    WHERE DOCTOR_NO = iddoctor;

//COMMIT;
//END;
#endregion

namespace NetCoreLinqToSqlInjection.Repositories
{

    public class RepositoryDoctoresOracle: IRepositoryDoctores
    {
        private DataTable tablaDoctores;
        private OracleConnection cn;
        private OracleCommand com;

        public RepositoryDoctoresOracle()
        {
            string connectionString = @"Data Source=LOCALHOST:1521/XE; Persist Security Info=True; User Id=SYSTEM; Password=oracle";
            this.tablaDoctores = new DataTable();
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            OracleDataAdapter ad = new OracleDataAdapter("select * from DOCTOR", connectionString);
            ad.Fill(this.tablaDoctores);

        }

        public void DeleteDoctor(int idDoctor)
        {
            string sql = "sp_delete_doctor";
            OracleParameter pamId = new OracleParameter(":p:iddoctor", idDoctor);
            this.com.Parameters.Add(pamId);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public Doctor FindDoctor(int iddoctor)
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           where datos.Field<int>("DOCTOR_NO") == iddoctor
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                var row = consulta.First();
                Doctor d = new Doctor
                {
                    IdHospital = row.Field<int>("HOSPITAL_COD"),
                    IdDoctor = row.Field<int>("DOCTOR_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Especialidad = row.Field<string>("ESPECIALIDAD"),
                    Salario = row.Field<int>("SALARIO")

                };
                return d;
            }
        }

        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doc = new Doctor();
                doc.IdDoctor = row.Field<int>("DOCTOR_NO");
                doc.Apellido = row.Field<string>("APELLIDO");
                doc.Especialidad = row.Field<string>("ESPECIALIDAD");
                doc.Salario = row.Field<int>("SALARIO");
                doc.IdHospital = row.Field<int>("HOSPITAL_COD");
                doctores.Add(doc);
            }
            return doctores;
        }

        public void InsertDoctor(int idDoctor, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "insert into DOCTOR values (:idhospital, :iddoctor "
            + ", :apellido, :especialidad, :salario)";
            //ORACLE TIENE EN CUENTA EL ORDEN DE LOS PARAMETROS
            OracleParameter pamHospital = new OracleParameter(":idhospital", idHospital);
            this.com.Parameters.Add(pamHospital);
            OracleParameter pamIdDoctor =
                new OracleParameter(":iddoctor", idDoctor);
            this.com.Parameters.Add(pamIdDoctor);
            OracleParameter pamApellido =
                new OracleParameter(":apellido", apellido);
            this.com.Parameters.Add(pamApellido);
            OracleParameter pamEspecialidad =
                new OracleParameter(":especialidad", especialidad);
            this.com.Parameters.Add(pamEspecialidad);
            OracleParameter pamSalario = new OracleParameter(":salario", salario);
            this.com.Parameters.Add(pamSalario);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void UpdateDoctor(int idHospital, int idDoctor, string apellido, string especialidad, int salario)
        {
            string sql = "SP_ACTUALIZAR_DOCTOR";
            OracleParameter pamHospital = new OracleParameter(":idhospital", idHospital);
            this.com.Parameters.Add(pamHospital);
            OracleParameter pamIdDoctor =
                new OracleParameter(":iddoctor", idDoctor);
            this.com.Parameters.Add(pamIdDoctor);
            OracleParameter pamApellido =
                new OracleParameter(":p_apellido", apellido);
            this.com.Parameters.Add(pamApellido);
            OracleParameter pamEspecialidad =
                new OracleParameter("p_especialidad", especialidad);
            this.com.Parameters.Add(pamEspecialidad);
            OracleParameter pamSalario = new OracleParameter(":p_salario", salario);
            this.com.Parameters.Add(pamSalario);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

        }

        public List<Doctor> GetDoctoresEspecialidad(string especialidad)
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           where (datos.Field<string>("ESPECIALIDAD")).ToUpper() == especialidad.ToUpper()
                           select datos;

            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                List<Doctor> doctores = new List<Doctor>();
                foreach (var row in consulta)
                {
                    Doctor d = new Doctor
                    {
                        IdHospital = row.Field<int>("HOSPITAL_COD"),
                        IdDoctor = row.Field<int>("DOCTOR_NO"),
                        Apellido = row.Field<string>("APELLIDO"),
                        Especialidad = row.Field<string>("ESPECIALIDAD"),
                        Salario = row.Field<int>("SALARIO")

                    };
                    doctores.Add(d);
                }
                return doctores;
            }
        }


    }
}
