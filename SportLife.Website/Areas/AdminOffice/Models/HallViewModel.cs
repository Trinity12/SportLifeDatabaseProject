namespace SportLife.Website.Areas.AdminOffice.Models {
    public class HallViewModel {
        public int ID { get; set; }
        public int AdressId { get; set; }
        public AdressViewModel Adress { get; set; }
        public int ImageId { get; set; }
    }

    public class AdressViewModel {
        public int ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Building { get; set; }
        public string Apartament { get; set; }

        public string FullAdress
            => $"{State}, {City}, {Street} street, {Building}/{Apartament}";
    }

    public class HallDropDownViewModel
    {
        public int ID { get; set; }
        public string FullAdress { get; set; }          
    }
}