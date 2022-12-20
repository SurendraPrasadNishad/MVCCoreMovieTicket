using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OnlineMovieTicket.Data.Enums;
using OnlineMovieTicket.Data.Static;
using OnlineMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Data
{
    public class AppInitializer
    {
        public static void seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDBContext>();
                context.Database.EnsureCreated();
                //Cinema
                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            Name="Kalika",
                            CinemaLogo="https://www.pngwing.com/en/free-png-zhggc",
                            Description="This cinema is kalika Cinema Established in 1963AD."
                        },
                         new Cinema()
                        {
                            Name="Padma shree",
                            CinemaLogo="https://www.pngegg.com/en/png-bpoom",
                            Description="This cinema is Padma shree Cinema Established in 2000AD."
                        },
                          new Cinema()
                        {
                            Name="Siddhartha",
                            CinemaLogo="https://www.pngegg.com/en/png-pyuxq",
                            Description="This cinema is Siddhartha Cinema Established in 1953AD."
                        }
                    });
                    context.SaveChanges();
                }
                //Producer
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            FullName="Karan Johar",
                            Bio="Johar was born on 25 May 1972 in Mumbai, India to film producer Yash Johar, founder of Dharma Productions and Hiroo Johar. He has Punjabi Hindu ancestry from his father's side, and Sindhi Hindu ancestry from his mother's side. He studied at the Greenlawns High School. After Greenlawns, he attended H.R.",
                            ProfilePictureUrl="https://www.cleanpng.com/png-varun-dhawan-bollywood-film-badlapur-actor-7166433/"
                        },
                        new Producer()
                        {
                            FullName="Maniratnam",
                            Bio="Mani Ratnam, originally Gopala Ratnam Subramaniam, (born June 2, 1956, Madura [now Madurai], Tamil Nadu, India), Indian filmmaker noted for his popular films in both Tamil and Hindi cinema.",
                            ProfilePictureUrl="https://www.cleanpng.com/png-varun-dhawan-student-of-the-year-bollywood-actor-f-7162998/"
                        },
                        new Producer()
                        {
                            FullName="Ekta kapoor",
                            Bio="Ekta Kapoor (born 7 June 1975) is an Indian television producer, film producer and director who works in Hindi cinema and soap opera",
                            ProfilePictureUrl="https://www.cleanpng.com/png-alia-bhatt-kalank-film-bollywood-actor-7180189/"
                        }
                    });
                    context.SaveChanges();
                }
                //Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName="YamiGautam",
                            Bio=" after doing some commercials for Glow & Lovely and began her acting career in televally progressed to Bollywood",
                            ProfilePictureUrl="https://www.cleanpng.com/png-yami-gautam-female-actor-social-media-bollywood-ya-76020/"
                        },
                         new Actor()
                        {
                            FullName="Hrithik Roshan",
                            Bio=" Starting from 2012, he has appeared in Forbes India's Celebrity 100 several times based on his income and popularity.",
                            ProfilePictureUrl="https://www.cleanpng.com/png-hrithik-roshan-actor-bollywood-film-male-hrithik-r-75822/"
                        },
                          new Actor()
                        {
                            FullName="Sahid Kapoor",
                            Bio="Hrithik Roshan (pronounced [rɪθɪk roʊʃən];[1] born 10 January 1974) is an Indian actor who works in Hindi films.  including six Filmfare Awards, of which four were for Best Actor. Starting from 2012, he has appeared in Forbes India's Celebrity 100 several times based on his income and popularity.",
                            ProfilePictureUrl="https://www.cleanpng.com/png-shahid-kapoor-actor-ishq-vishk-bollywood-shahid-ka-89269/"
                        }
                    });
                    context.SaveChanges();
                }
                //Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            MovieName="Kantara",
                            Description="In 1800s, a King trades his forest land to a demigod for peace of mind. The demigod gives the land to the tribals and warns the King that should he or his descendants seek to get the lands back there would be disastrous consequences.",
                            Imageurl="https://images.app.goo.gl/NBGrkX34Ub3vDMqN9",
                            StartDate=DateTime.Now.AddDays(-4),
                            EndDate=DateTime.Now.AddDays(-2),
                            Price=13.25,
                            MovieCategory=MovieCategory.Drama,
                            CinemaId=1,
                            ProducerId=2


                        },
                         new Movie()
                        {
                            MovieName="Bhulbhulaiya",
                            Description="Plot. Badrinarayan Badri Chaturvedi heads former royal family of Varanasi whose ancestral palace is believed to be haunted by the ghost of Manjulika, a classical dancer from Bengal. Siddharth, son of Badri's elder brother, and his archeologist wife Avni, return to the palace from the United States.",
                            Imageurl="https://images.app.goo.gl/AvK7y8YVtXqpvEuX6",
                            StartDate=DateTime.Now.AddDays(-5),
                            EndDate=DateTime.Now.AddDays(-1),
                            Price=13.25,
                            MovieCategory=MovieCategory.Thriller,
                            CinemaId=2,
                            ProducerId=3

                        },
                         new Movie()
                         {
                          MovieName="AANKHE",
                            Description="After a temperamental man is unceremoniously stripped of his duties as a bank manager, he decides to seek revenge by robbing the bank. He trains three blind men to do this risky task for him. Vijay Singh Rajput (Amitabh Bachchan) is a quirky manager of Vilasrao Jefferson Bank.",
                            Imageurl="https://images.app.goo.gl/a1K83LXUGzzU5Uf36",
                            StartDate=DateTime.Now.AddDays(-10),
                            EndDate=DateTime.Now.AddDays(-2),
                            Price=23.25,
                            MovieCategory=MovieCategory.Mystery,
                            CinemaId=3,
                            ProducerId=3
                         }

                    });
                    context.SaveChanges();
                }
                //Actors and Movies
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie()
                        {
                            ActorId=1,
                            MovieId=2,
                        },
                         new Actor_Movie()
                        {
                            ActorId=2,
                            MovieId=2
                        },
                          new Actor_Movie()
                        {
                           ActorId=1,
                            MovieId=3
                        },
                          new Actor_Movie()
                          {
                               ActorId=2,
                            MovieId=3
                          }
                          ,
                          new Actor_Movie()
                          {
                             ActorId=3,
                            MovieId=1
                          }
                    });
                    context.SaveChanges();
                }
            }
        }


        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            //Create scope of application service
            using (var serviceScope= applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles From RoleManager By Default create table so add data to it
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin)) 
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));


                //Users From UserManager adding data through model ApplicationUser.cs to db 
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail= "OnlineMovieTicket.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName="admin-user",
                        Email="admin@UserEmail",
                        EmailConfirmed=true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@123?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@OnlineMovieTicket.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = "app@UserEmail",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@123?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
