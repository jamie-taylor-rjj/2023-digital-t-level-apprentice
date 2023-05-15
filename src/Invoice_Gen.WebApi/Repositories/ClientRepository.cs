namespace Invoice_Gen.WebApi.Repositories;

public class ClientRepository : IClientRepository
{
    public List<Client> GetAll() =>
        new()
        {
            new Client
            {
                ClientId = 1,
                ClientName = "Muller Inc",
                ContactName = "Bradley S Crooks",
                ClientAddress = "4509 Clement Street, Atlanta, GA 30331",
                ContactEmail = "AaronDBaker@dayrep.com"
            },
            new Client
            {
                ClientId = 2,
                ClientName = "Gutkowski Inc",
                ContactName = "Anna L. Witt",
                ClientAddress = "2545 James Street, Fairport, NY 14450",
                ContactEmail = "AnnaLWitt@teleworm.us"
            },
            new Client
            {
                ClientId = 3,
                ClientName = "Hoeger - Gislason",
                ContactName = "Susan Railey",
                ClientAddress = "923 Euclid Avenue, San Luis Obispo, CA 93401",
                ContactEmail = "SusanMRailey@armyspy.com"
            },
            new Client
            {
                ClientId = 4,
                ClientName = "Toy Group",
                ContactName = "Stanley D. Rogers",
                ClientAddress = "2607 Goldcliff Circle, Washington, DC 20005",
                ContactEmail = "StanleyDRogers@dayrep.com"
            },
            new Client
            {
                ClientId = 5,
                ClientName = "Upton, Gleason and Cronin",
                ContactName = "Tammy W. Finley",
                ClientAddress = "1381 Monroe Street, Houston, TX 77030",
                ContactEmail = "TammyWFinley@dayrep.com"
            },
        };
}
