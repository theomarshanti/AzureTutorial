using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PsHelloAzureApp.Models;
using Microsoft.Azure.Documents.Client;

namespace PsHelloAzureApp.Services
{
    public class CourseStore
    {
        private DocumentClient client;
        private Uri coursesLink;

        public CourseStore()
        {
            //URI for our Cosmos DB is in Home > AzureCosmos DB > omarsazurecosmosdb
            var uri = new Uri("https://omarspsdb.documents.azure.com:443/");
            //The key below is typically stored in key-vault or in ApplicationSettings on azureportal
            //It is really sensitive information
            var key = "yStIMaPxFX3owO7bqXlD0PgLXymiqfYmR0IoCoJBYUwE7kryIYvAHYoYrUC7PipUc8DpucMh3Xn4UqP9N7Uy1w==";
            client = new DocumentClient(uri, key);
            coursesLink = UriFactory.CreateDocumentCollectionUri("omarspshelloazure", "courses");
            //Name of Database and then name of the collection
        }

        public async Task InsertCourses(IEnumerable<Course> courses)
        {
            foreach(var course in courses)
            {
                await client.CreateDocumentAsync(coursesLink, course);
                //requires link to where the doc should be stored; here that will be
                // the URI that describes the courses collections in our Database
            }
        }

        public IEnumerable<Course> GetAllCourses()
        {
            var courses = client.CreateDocumentQuery<Course>(coursesLink)
                .OrderBy(c=>c.Title);

            return courses;
        }
    }
}
