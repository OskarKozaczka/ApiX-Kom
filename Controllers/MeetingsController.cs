using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiX_Kom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {

        [HttpGet("all")]
        public IEnumerable<Meeting> Get()
        {
            string MeetingsQueryString = string.Format("SELECT * from Meetings");
            
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var MeetingList = new List<Meeting>();
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand MeetingsCommand = new SqlCommand(MeetingsQueryString, connection);
                    MeetingsCommand.Connection.Open();

                    using (SqlDataReader MeetingsReader = MeetingsCommand.ExecuteReader())
                    {
                        while (MeetingsReader.Read()) {

                            var ParticipantsList = new List<Participant>();
                            string ParticipantsQueryString = string.Format("SELECT * from Participants WHERE Meeting = '{0}'", MeetingsReader["MeetingName"].ToString());
                            SqlCommand ParticipantsCommand = new SqlCommand(ParticipantsQueryString, connection);

                            using (SqlDataReader ParticipantsReader = ParticipantsCommand.ExecuteReader())
                            {
                                while (ParticipantsReader.Read()) ParticipantsList.Add(
                                    new Participant { Name = ParticipantsReader["Name"].ToString(), Email = ParticipantsReader["Email"].ToString() });
                            }
                            MeetingList.Add(new Meeting { Name = MeetingsReader["MeetingName"].ToString(), Participants = ParticipantsList });
                        }
                    }

                }
            }
            return MeetingList;
        }


        [HttpGet("{MeetingName}")]
        public Meeting Get(string MeetingName)
        {
            string MeetingsQueryString = string.Format("SELECT * from Meetings WHERE MeetingName = '{0}'",MeetingName);

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var Meeting = new Meeting();
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand MeetingsCommand = new SqlCommand(MeetingsQueryString, connection);
                    MeetingsCommand.Connection.Open();

                    using (SqlDataReader MeetingsReader = MeetingsCommand.ExecuteReader())
                    {
                        while (MeetingsReader.Read())
                        {

                            var ParticipantsList = new List<Participant>();
                            string ParticipantsQueryString = string.Format("SELECT * from Participants WHERE Meeting = '{0}'", MeetingsReader["MeetingName"].ToString());
                            SqlCommand ParticipantsCommand = new SqlCommand(ParticipantsQueryString, connection);

                            using (SqlDataReader ParticipantsReader = ParticipantsCommand.ExecuteReader())
                            {
                                while (ParticipantsReader.Read()) ParticipantsList.Add(
                                    new Participant { Name = ParticipantsReader["Name"].ToString(), Email = ParticipantsReader["Email"].ToString() });
                            }
                            Meeting= new Meeting { Name = MeetingsReader["MeetingName"].ToString(), Participants = ParticipantsList };
                        }
                    }

                }
            }
            return Meeting;
        }


        [HttpPost("create/{MeetingName}")]
        public string Post(string MeetingName)
        {
            string queryString = string.Format("INSERT INTO Meetings VALUES('{0}');", MeetingName);
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return string.Format("Meeting: {0}, created succesfully", MeetingName);
        }


        [HttpPost("register")]
        public string Post([FromQuery] string Meeting, [FromQuery] string name ,[FromQuery] string email)
        {
            string CheckLimitQueryString = string.Format("SELECT * FROM Participants WHERE Meeting = '{0}'", Meeting);
            string queryString = string.Format("INSERT INTO Participants VALUES('{0}','{1}','{2}');", Meeting,name,email);
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(CheckLimitQueryString, connection);
                    command.Connection.Open();
                    var NumberOfParcipants = 0;
                    using (SqlDataReader ParticipantsReader = command.ExecuteReader()) while (ParticipantsReader.Read()) NumberOfParcipants++;

                    if (NumberOfParcipants == 25) return "Limit of Participants has been reached ";
                    else
                    {
                        command = new SqlCommand(queryString, connection);
                        command.ExecuteNonQuery();
                        return "Participant Registered";
                    }
                }
            }
        }

        [HttpPost("delete/{MeetingName}")]
        [HttpDelete("{MeetingName}")]
        public string Delete(string MeetingName)
        {
            string MeetingsQueryString = string.Format("DELETE FROM Meetings WHERE MeetingName = '{0}';", MeetingName);
            string ParticipantsQueryString = string.Format("DELETE FROM Participants WHERE Meeting = '{0}';", MeetingName);
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(MeetingsQueryString, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command = new SqlCommand(ParticipantsQueryString, connection);
                    command.ExecuteNonQuery();
                }
            }
            return string.Format("Meeting: {0}, deleted succesfully", MeetingName);
        }
    }
}
