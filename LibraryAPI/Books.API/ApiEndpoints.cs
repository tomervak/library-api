namespace Books.API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Books
    {
        private const string Base = $"{ApiBase}/books";
        
        public const string Get = $"{Base}/{{id:guid}}";

        public const string GetAll = Base;

        public const string Create = Base;

        public const string Update = $"{Base}/{{id:guid}}";
        
        public const string Delete = $"{Base}/{{id:guid}}";
    }
}