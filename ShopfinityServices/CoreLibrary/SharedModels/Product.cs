namespace CoreLibrary.SharedModels
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Category { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public double Price { get; set; }
    }
}
