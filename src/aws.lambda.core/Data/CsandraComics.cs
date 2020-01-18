using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace aws.lambda.core.Data{

    [DynamoDBTable("csandra_comic_store")]
    public class ComicData
    {
        private string id;
        [DynamoDBHashKey] //Partition key
        public string ID {get=> (string.IsNullOrWhiteSpace(id)?"040488110614_"+Title: id); set=> id = value;}
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string PictureUri { get; set; }
        public string Heroes { get; set; }
        public string Publisher { get; set; }
        public string Authors { get; set; }
        public string Artists { get; set; }
        public string Categories { get; set; }
        public string Medium { get; set; }
        public string LentTo { get; set; }
        public DateTime? LentSince { get; set; }
        public bool IsLent { get; set; }
        public bool IsKindle { get; set; }
        public int? Rating { get; set; } = 0;
        public int EditableRating { get=> Rating.GetValueOrDefault(0); set=> Rating = value; } 
        public string Genres { get; set; }
        public string Tags { get; set; }

        static AmazonDynamoDBClient  SetupDynamoDbClient()=>
            new AmazonDynamoDBClient(
                "AccessKey",
                "Secret",
                new AmazonDynamoDBConfig(){
                    RegionEndpoint = Amazon.RegionEndpoint.EUWest1
                });
        
        internal static async Task<List<ComicData>> GetComics (string client=""){
           try{
                using(var context = new DynamoDBContext (SetupDynamoDbClient())){
                    var comics = await context.ScanAsync<ComicData>(
                        new List<ScanCondition>(){
                            new ScanCondition("ID",ScanOperator.BeginsWith, client )
                            }).GetRemainingAsync();
                    Console.WriteLine("After Scan");
                    foreach(var comic in comics)
                    {
                        if(string.IsNullOrWhiteSpace(client) ||
                           !comic.ID.Contains(client)){
                               comic.LentTo = "";
                               comic.LentSince = null;
                           }

                    }
                    return comics.ToList();
                }
                
           }
           catch(Exception ex){
               Console.WriteLine(ex.Message);
               return new List<ComicData>();
           }
        }

        internal static async Task<string> SaveComic(ComicData data, string client = "")  {
            try{
               if(string.IsNullOrWhiteSpace(client))
                    throw new UnauthorizedAccessException("You didn't pass a valid client");
                if(data.ID.Contains(client)){
                    using(var context = new DynamoDBContext (SetupDynamoDbClient())){
                        await context.SaveAsync(data);
                        Console.WriteLine("After Save");
                    }
                    return "";
                }
                else
                    throw new UnauthorizedAccessException("Client is not authorized for changes");
           }
           catch(Exception ex){
               Console.WriteLine(ex.Message);
               return ex.Message;
           }
        }
    }

}