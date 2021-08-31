using Microsoft.Data.SqlClient;
using PoliceHR.Models.AccessModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows;

namespace PoliceHR.ViewModels.AccessViewModels
{
    public class AccVM
    {
        public string MDBConnectString;
        public AccVM(string Mdb_Url)
        {
            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Mdb_Url + ";Jet OLEDB:Database Password=الدقة أساس النجاح;";
            MDBConnectString = connStr;
        }

        public void AddElement(Person PersonMss)
        {

            string sql = "INSERT INTO Persons (FIRST_NAME,FAMILY_NAME,FATHER_NAME,EDUCATION1,EHT_START_DATE,ENLIST_BRANCH,CURR_UNIT,BIRTH_PLACE,BIRTH_DATE,MANDATORY_END_DATE,MANDATORY_START_DATE,MIL_NUMBER,MIL_NUMBER_SLICE,MIL_RANK,MIL_RANK_DATE,MIL_SPEC,MOTHER_NAME,NATIONAL_NUMBER,SERVICE_STATE,SERVICE_TYPE,VOLUNTEER_DATE) " +
                                      "VALUES(@FIRST_NAME,@FAMILY_NAME,@FATHER_NAME,@EDUCATION1,@EHT_START_DATE,@ENLIST_BRANCH,@CURR_UNIT,@BIRTH_PLACE,@BIRTH_DATE,@MANDATORY_END_DATE,@MANDATORY_START_DATE,@MIL_NUMBER,@MIL_NUMBER_SLICE,@MIL_RANK,@MIL_RANK_DATE,@MIL_SPEC,@MOTHER_NAME,@NATIONAL_NUMBER,@SERVICE_STATE,@SERVICE_TYPE,@VOLUNTEER_DATE)";

            using (OleDbConnection con = new OleDbConnection(MDBConnectString))
            {
                con.Open();

                using (OleDbCommand comando = new OleDbCommand(sql, con))
                {
                    comando.Parameters.Add("@FirstName", PersonMss.FirstName);
                    comando.Parameters.Add("@FamilyName", PersonMss.FamilyName);
                    comando.Parameters.Add("@FatherName", PersonMss.FatherName);
                    comando.Parameters.Add("@Education", PersonMss.Education1);
                    comando.Parameters.Add("@EhtStartDate", PersonMss.EhtStartDate);
                    comando.Parameters.Add("@EnlistBranch", PersonMss.EnlistBranch);
                    comando.Parameters.Add("@CurrUnit", PersonMss.CurrUnit);
                    comando.Parameters.Add("@BirthPlace", PersonMss.BirthPlace);
                    comando.Parameters.Add("@BirthDate", PersonMss.BirthDate);
                    comando.Parameters.Add("@MandatoryEndDate", PersonMss.MandatoryEndDate);
                    comando.Parameters.Add("@MandatoryStartDate", PersonMss.MandatoryStartDate);
                    comando.Parameters.Add("@MilNumber", PersonMss.MilNumber);
                    comando.Parameters.Add("@MilNumberSlice", PersonMss.MilNumberSlice);
                    comando.Parameters.Add("@MilRank", PersonMss.MilRank);
                    comando.Parameters.Add("@MilRankDate", PersonMss.MilRankDate);
                    comando.Parameters.Add("@MilSpec", PersonMss.MilSpec);
                    comando.Parameters.Add("@MotherName", PersonMss.MotherName);
                    comando.Parameters.Add("@NationalNumber", PersonMss.NationalNumber);
                    comando.Parameters.Add("@ServiceState", PersonMss.ServiceState);
                    comando.Parameters.Add("@ServiceType", PersonMss.ServiceType);
                    comando.Parameters.Add("@VolunteerDate", PersonMss.VolunteerDate);
                    comando.ExecuteNonQuery();
                }

                con.Close();
            }



        }
        public void UpdateProdact(int Elm_ID, Person PersonMss)
        { 
                                           //FirstName,           FamilyName,            FatherName,               Education,            EhtStartDate,                EnlistBranch,               CurrUnit           ,BirthPlace             ,BirthDate            ,MandatoryEndDate                    ,MandatoryStartDate                      ,MilNumber            ,MilNumberSlice                  ,MilRank          ,MilRankDate               ,MilSpec         ,MotherName            ,NationalNumber                   ,ServiceState               ,ServiceType             ,VolunteerDate
            string sql = "UPDATE Persons SET FIRST_NAME=@FirstName,FAMILY_NAME=@FamilyName,FATHER_NAME=@FatherName,EDUCATION1=@Education,EHT_START_DATE=@EhtStartDate,ENLIST_BRANCH=@EnlistBranch,CURR_UNIT=@CurrUnit,BIRTH_PLACE=@BirthPlace,BIRTH_DATE=@BirthDate,MANDATORY_END_DATE=@MandatoryEndDate,MANDATORY_START_DATE=@MandatoryStartDate,MIL_NUMBER=@MilNumber,MIL_NUMBER_SLICE=@MilNumberSlice,MIL_RANK=@MilRank,MIL_RANK_DATE=@MilRankDate,MIL_SPEC=@MilSpec,MOTHER_NAME=@MotherName,NATIONAL_NUMBER=@NationalNumber,SERVICE_STATE=@ServiceState,SERVICE_TYPE=@ServiceType,VOLUNTEER_DATE=@VolunteerDate WHERE MIL_NUMBER=" + Elm_ID.ToString();

            using (OleDbConnection con = new OleDbConnection(MDBConnectString))
            {
                con.Open();

                using (OleDbCommand comando = new OleDbCommand(sql, con))
                {
                    comando.Parameters.Add("@FirstName", PersonMss.FirstName);
                    comando.Parameters.Add("@FamilyName", PersonMss.FamilyName);
                    comando.Parameters.Add("@FatherName", PersonMss.FatherName);
                    comando.Parameters.Add("@Education", PersonMss.Education1);
                    comando.Parameters.Add("@EhtStartDate", PersonMss.EhtStartDate);
                    comando.Parameters.Add("@EnlistBranch", PersonMss.EnlistBranch);
                    comando.Parameters.Add("@CurrUnit", PersonMss.CurrUnit);
                    comando.Parameters.Add("@BirthPlace", PersonMss.BirthPlace);
                    comando.Parameters.Add("@BirthDate", PersonMss.BirthDate);
                    comando.Parameters.Add("@MandatoryEndDate", PersonMss.MandatoryEndDate);
                    comando.Parameters.Add("@MandatoryStartDate", PersonMss.MandatoryStartDate);
                    comando.Parameters.Add("@MilNumber", PersonMss.MilNumber);
                    comando.Parameters.Add("@MilNumberSlice", PersonMss.MilNumberSlice);
                    comando.Parameters.Add("@MilRank", PersonMss.MilRank);
                    comando.Parameters.Add("@MilRankDate", PersonMss.MilRankDate);
                    comando.Parameters.Add("@MilSpec", PersonMss.MilSpec);
                    comando.Parameters.Add("@MotherName", PersonMss.MotherName);
                    comando.Parameters.Add("@NationalNumber", PersonMss.NationalNumber);
                    comando.Parameters.Add("@ServiceState", PersonMss.ServiceState);
                    comando.Parameters.Add("@ServiceType", PersonMss.ServiceType);
                    comando.Parameters.Add("@VolunteerDate", PersonMss.VolunteerDate);

                    comando.ExecuteNonQuery();
                }

                con.Close();
            }


        }
        public void DeleteElm(int Elm_ID)
        {

            string sql = "DELETE FROM Persons WHERE MilNumber=" + Elm_ID.ToString();

            using (OleDbConnection con = new OleDbConnection(MDBConnectString))
            {
                con.Open();

                using (OleDbCommand comando = new OleDbCommand(sql, con))
                {
                    comando.ExecuteNonQuery();
                }

                con.Close();
            }


        }
        public List<Person> ElementsResource()
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(MDBConnectString))
                {
                using (OleDbCommand cmd = new OleDbCommand("SELECT* FROM Person", con))
                {
                    List<Person> ElmRss = new List<Person>();
                    cmd.CommandType = CommandType.Text;
                    if (con.State != ConnectionState.Open) { con.Open(); }
                    using (OleDbDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ElmRss.Add(new Person
                            {
                                NationalNumber = sdr["NATIONAL_NUMBER"] == DBNull.Value ? string.Empty : sdr["NATIONAL_NUMBER"].ToString(),
                                MilNumber = sdr["MIL_NUMBER"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["MIL_NUMBER"]),
                                MilNumberSlice = sdr["MIL_NUMBER_SLICE"] == DBNull.Value ? string.Empty : sdr["MIL_NUMBER_SLICE"].ToString(),
                                ServiceType = sdr["SERVICE_TYPE"] == DBNull.Value ? 0 : Convert.ToInt16(sdr["SERVICE_TYPE"]),
                                ServiceState = sdr["SERVICE_STATE"] == DBNull.Value ? 0 : (short)sdr["SERVICE_STATE"],
                                MilRank = sdr["MIL_RANK"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["MIL_RANK"]),
                                FirstName = sdr["FIRST_NAME"] == DBNull.Value ? string.Empty : sdr["FIRST_NAME"].ToString(),
                                FatherName = sdr["FATHER_NAME"] == DBNull.Value ? string.Empty : sdr["FATHER_NAME"].ToString(),
                                FamilyName = sdr["FAMILY_NAME"] == DBNull.Value ? string.Empty : sdr["FAMILY_NAME"].ToString(),
                                MotherName = sdr["MOTHER_NAME"] == DBNull.Value ? string.Empty : sdr["MOTHER_NAME"].ToString(),
                                MandatoryEndDate = sdr["MANDATORY_END_DATE"] == DBNull.Value ? DateTime.MinValue : (DateTime)sdr["MANDATORY_END_DATE"],
                                MandatoryStartDate = sdr["MANDATORY_START_DATE"] == DBNull.Value ? DateTime.MinValue : (DateTime)sdr["MANDATORY_START_DATE"],
                                MilRankDate = sdr["MIL_RANK_DATE"] == DBNull.Value ? DateTime.MinValue : (DateTime)sdr["MIL_RANK_DATE"],
                                MilSpec = sdr["MIL_SPEC"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["MIL_SPEC"]),
                                BirthDate = sdr["BIRTH_DATE"] == DBNull.Value ? DateTime.MinValue : (DateTime)sdr["BIRTH_DATE"],
                                BirthPlace = sdr["BIRTH_PLACE"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["BIRTH_PLACE"]),
                                CurrUnit = sdr["CURR_UNIT"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["CURR_UNIT"]),
                                Education1 = sdr["EDUCATION1"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["EDUCATION1"]),
                                EhtStartDate = sdr["EHT_START_DATE"] == DBNull.Value ? DateTime.MinValue : (DateTime)sdr["EHT_START_DATE"],
                                EnlistBranch = sdr["ENLIST_BRANCH"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["ENLIST_BRANCH"]),
                                VolunteerDate = sdr["VOLUNTEER_DATE"] == DBNull.Value ? DateTime.MinValue : (DateTime)sdr["VOLUNTEER_DATE"]
                                //Adress = sdr["ADRESS"] == DBNull.Value ? string.Empty : sdr["ADRESS"].ToString()
                            });
                        }
                    }
                    con.Close();
                    return ElmRss;
                }
            }

            }catch( Exception ex)
            {
                MessageBox.Show(ex.Message, "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
           
        }
        public List<Places> PlacesResource()
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(MDBConnectString))
                {
                    using (OleDbCommand cmd = new OleDbCommand("SELECT* FROM Places", con))
                    {
                        List<Places> ElmRss = new List<Places>();
                        cmd.CommandType = CommandType.Text;
                        if (con.State != ConnectionState.Open) { con.Open(); }
                        using (OleDbDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                ElmRss.Add(new Places
                                {
                                    Code = sdr["CODE"] == DBNull.Value ? 0 : (int)sdr["CODE"],
                                    DetailName = sdr["DETAIL_NAME"] == DBNull.Value ? string.Empty : sdr["DETAIL_NAME"].ToString(),
                                    Flag = (short)(sdr["FLAG"] == DBNull.Value ? 0 : (short) sdr["FLAG"]),
                                    PlaceUpcode = sdr["PLACE_UPCODE"] == DBNull.Value ? 0 : (int)sdr["PLACE_UPCODE"],
                                    Pname = sdr["PNAME"] == DBNull.Value ? string.Empty : (string)sdr["PNAME"],
                                    _22082016 = sdr["22/08/2016"] == DBNull.Value ? DateTime.MinValue : (DateTime)sdr["22/08/2016"],
                                });
                            }
                        }
                        con.Close();
                        return ElmRss;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        }
        public List<EnlistBranch> BranchResource()
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(MDBConnectString))
                {
                    using (OleDbCommand cmd = new OleDbCommand("SELECT* FROM ENLIST_BRANCH", con))
                    {
                        List<EnlistBranch> ElmRss = new List<EnlistBranch>();
                        cmd.CommandType = CommandType.Text;
                        if (con.State != ConnectionState.Open) { con.Open(); }
                        using (OleDbDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                ElmRss.Add(new EnlistBranch
                                {
                                    Encoded = sdr["ENCODED"] == DBNull.Value ? 0 : (double)sdr["ENCODED"],
                                    Decoded = sdr["DECODED"] == DBNull.Value ? string.Empty : sdr["DECODED"].ToString(),
                                    DispOrder = sdr["DISP_ORDER"] == DBNull.Value ? 0 : (double)sdr["DISP_ORDER"],
                                    FatherCode = sdr["FATHER_CODE"] == DBNull.Value ? 0 : (double)sdr["FATHER_CODE"],
                                    RecState = sdr["REC_STATE"] == DBNull.Value ? 0 : (double)sdr["REC_STATE"],
                                    TableName = sdr["TABLE_NAME"] == DBNull.Value ? string.Empty : sdr["TABLE_NAME"].ToString(),
                                });
                            }
                        }
                        con.Close();
                        return ElmRss;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        }
    }
}
