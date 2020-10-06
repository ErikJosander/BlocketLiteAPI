namespace BlocketLiteAPI
{
    public class DbString
    {
        public static string localDbString = @"Server=(localdb)\mssqllocaldb;Database=BlocketLiteDB;Trusted_Connection=True;";
        public static string azureDbString = @"Server=tcp:blocketliteapidbserver.database.windows.net,1433;Initial Catalog=BlocketLiteAPI_db;Persist Security Info=False;" +
            "User ID=erik;Password=Bas98Pmar;" +
            "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
