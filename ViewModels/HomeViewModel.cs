using _3sApp.Models;

namespace _3sApp.ViewModels
{
    public class HomeViewModel
    {
            public List<Slider> Sliders { get; set; }
            public About About { get; set; }
            public SiteSetting SiteSettings { get; set; }
            public List<SocialMedia> SocialMedias { get; set; }
            public List<Contactitem> Contactitems { get; set; }
            public List<Service> Services { get; set; }
            public List<Solution> Solutions { get; set; }
            public List<Project> Projects { get; set; }

            public List<Industry> Industries { get; set; }
            public List<Partner> Partners { get; set; }

            public List<Client> Clients { get; set; }

        
    }
}
