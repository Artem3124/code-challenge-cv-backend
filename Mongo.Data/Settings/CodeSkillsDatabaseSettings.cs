namespace Mongo.Data.Settings
{
    public class CodeSkillsDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string CodeRunMessageQueueName { get; set; }

        public string TestCaseSetCollectionName { get; set; }
    }
}
