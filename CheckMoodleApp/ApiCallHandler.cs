using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using CheckMoodleApp.Models;

namespace CheckMoodleApp
{
    public static class ApiCallHandler
    {
        public static string baseUrl = "https://acs.curs.pub.ro/2019/";
        public static string loginEndpoint = "login/token.php";
        public static string dataEndpoint = "webservice/rest/server.php";

        public static string service = "moodle_mobile_app";
        public static string token;
        public static string userId;

        // Getting a token based on credentials
        public static void SetToken(string username, string password)
        {

            RestClient restClient = new RestClient(baseUrl);

            RestRequest restRequest = new RestRequest(loginEndpoint, Method.GET);
            restRequest.AddParameter("moodlewsrestformat", "json");
            restRequest.AddParameter("username", username);
            restRequest.AddParameter("password", password);
            restRequest.AddParameter("service", service);

            string requestResponse = restClient.Execute(restRequest).Content;

            TokenResponseModel tokenResponse = JsonConvert.DeserializeObject<TokenResponseModel>(requestResponse);

            token = tokenResponse.Token;
        }

        // Getting the user id for the user the token was generated for
        public static void SetUserId()
        {
            RestClient restClient = new RestClient(baseUrl);

            RestRequest restRequest = new RestRequest(dataEndpoint, Method.GET);
            restRequest.AddParameter("wstoken", token);
            restRequest.AddParameter("moodlewsrestformat", "json");
            restRequest.AddParameter("wsfunction", "core_webservice_get_site_info");

            string requestResponse = restClient.Execute(restRequest).Content;

            WebInfoResponseModel getSiteInfoResponse = JsonConvert.DeserializeObject<WebInfoResponseModel>(requestResponse);

            userId = getSiteInfoResponse.UserId;
        }

        // Getting the courses the user is enrolled in
        public static List<CourseResponseModel> GetCourses() 
        {
            RestClient restClient = new RestClient(baseUrl);

            RestRequest restRequest = new RestRequest(dataEndpoint, Method.GET);
            restRequest.AddParameter("wstoken", token);
            restRequest.AddParameter("moodlewsrestformat", "json");
            restRequest.AddParameter("wsfunction", "core_enrol_get_users_courses");
            restRequest.AddParameter("userid", userId);
            
            string requestResponse = restClient.Execute(restRequest).Content;

            List<CourseResponseModel> courses = JsonConvert.DeserializeObject<List<CourseResponseModel>>(requestResponse);

            return courses;
        }

        public static string GetCourseContents(string courseId)
        {
            RestClient restClient = new RestClient(baseUrl);
            RestRequest restRequest = new RestRequest(dataEndpoint, Method.GET);
            restRequest.AddParameter("wstoken", token);
            restRequest.AddParameter("moodlewsrestformat", "json");
            restRequest.AddParameter("wsfunction", "core_course_get_contents");
            restRequest.AddParameter("courseid", courseId);

            string requestResponse = restClient.Execute(restRequest).Content;
            
            // Since we are interested in any changes to the Moodle course page, we don't have to check (deserialize) the data we get
            return requestResponse;
        }

    }
}
