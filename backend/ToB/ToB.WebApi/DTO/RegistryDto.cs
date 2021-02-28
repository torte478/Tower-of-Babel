namespace ToB.WebApi.DTO
{
    public class RegistryDto //TODO : to c# 9
    {
        public int Id { get; set; }
        public int? Parent { get; set; }
        public string Label { get; set; }
    }
}