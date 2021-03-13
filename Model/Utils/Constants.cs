namespace Model.Utils
{
    public class Constants
    {
        #region sql
        public const string InMemoryDatabaseName = "ChatRooms_DB";
        public const string SQLCurrentTimeStamp = "CURRENT_TIMESTAMP";
        #endregion
        #region user
        public const string NotValidUser = "Not valid user";
        public const string NotUserFound = "Not user found";
        public const string EmailAlreadyUsed = "Your e-mail address is already used";
        #endregion
        #region stock
        public const string StockCommandTitle = "/stock";
        public const string StockCommandSeparator = "=";
        public const string StockChatBotUserName = "Stock Chatbot";
        public const string StockNoDataFromCommandText = "N/D";
        public const string UnavailableService = "The service is not working. Try it later";
        #endregion
    }
}
