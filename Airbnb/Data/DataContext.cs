using MySqlConnector;
using System.Collections.Generic;
using Airbnb.Models;
using System.Data;
using Airbnb.Models.Airbnb.Models;
using System.Reflection.PortableExecutable;

namespace Airbnb.Data
{
    public class DataContext
    {
        private readonly string _connectionString;

        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public IEnumerable<User> GetUsers()
        {
            var users = new List<User>();

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM user", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserId = reader.GetInt32("user_id"),
                            UserName = reader.GetString("user_name"),
                            FirstName = reader.GetString("first_name"),
                            LastName = reader.GetString("last_name"),
                            Email = reader.GetString("email"),
                            Password = reader.GetString("password"),
                            Type = reader.GetString("type"),
                            PhoneNumber = reader.GetString("phone_number"),
                           

                        });
                    }
                }
            }

            return users;
        }

        public User GetUserById(int id)
        {
            User user = null;

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM user WHERE user_id = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserId = reader.GetInt32("user_id"),
                            UserName = reader.GetString("user_name"),
                            FirstName = reader.GetString("first_name"),
                            LastName = reader.GetString("last_name"),
                            Email = reader.GetString("email"),
                            Password = reader.GetString("password"),
                            Type = reader.GetString("type"),
                            PhoneNumber = reader.GetString("phone_number"),
                            
                        };
                    }
                }
            }

            return user;
        }

        public void AddUser(User user)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("INSERT INTO user (user_id,user_name, first_name, last_name, email, password, type,phone_number,dob) VALUES (@UserId, @Username, @FirstName, @LastName, @Email, @Password, @Type, @PhoneNumber, @Dob)", connection);
                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@Username", user.UserName);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Type", user.Type);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@Dob", user.Dob);


                cmd.ExecuteNonQuery();
            }
        }


        public void UpdateUser(User user)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("UPDATE user SET user_name = @Username, first_name = @FirstName, last_name = @LastName, email = @Email, password = @Password,phone_number = @PhoneNumber, dob =@Dob, type = @Type WHERE user_id = @UserId", connection);
                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@Username", user.UserName);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Type", user.Type);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@Dob", user.Dob);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // First, delete related reviews
                var deleteReviewsCmd = new MySqlCommand("DELETE FROM reviews WHERE user_id = @id", connection);
                deleteReviewsCmd.Parameters.AddWithValue("@id", id);
                deleteReviewsCmd.ExecuteNonQuery();

                // Then, delete related bookings
                var deleteBookingsCmd = new MySqlCommand("DELETE FROM booking WHERE user_id = @id", connection);
                deleteBookingsCmd.Parameters.AddWithValue("@id", id);
                deleteBookingsCmd.ExecuteNonQuery();

                // Finally, delete the user
                var deleteUserCmd = new MySqlCommand("DELETE FROM user WHERE user_id = @id", connection);
                deleteUserCmd.Parameters.AddWithValue("@id", id);
                deleteUserCmd.ExecuteNonQuery();
            }
        }



        // Location methods
        // Method to fetch all locations
        public IEnumerable<Location> GetLocations()
        {
            var locations = new List<Location>();

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM location", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        locations.Add(new Location
                        {
                            LocationId = reader.GetInt32("location_id"),
                            Country = reader.GetString("country"),
                            City = reader.GetString("city"),
                            Postcode = reader.GetString("postcode")
                        });
                    }
                }
            }

            return locations;
        }

        // Method to fetch a location by ID
        public Location GetLocationById(int id)
        {
            Location location = null;

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM location WHERE location_id = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        location = new Location
                        {
                            LocationId = reader.GetInt32("location_id"),
                            Country = reader.GetString("country"),
                            City = reader.GetString("city"),
                            Postcode = reader.GetString("postcode")
                        };
                    }
                }
            }

            return location;
        }

        // Method to add a new location
        public void AddLocation(Location location)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("INSERT INTO location (country, city, postcode) VALUES (@Country, @City, @Postcode)", connection);
                cmd.Parameters.AddWithValue("@Country", location.Country);
                cmd.Parameters.AddWithValue("@City", location.City);
                cmd.Parameters.AddWithValue("@Postcode", location.Postcode);

                cmd.ExecuteNonQuery();
            }
        }


        // Booking methods
        public IEnumerable<Booking> GetBookings()
        {
            var bookings = new List<Booking>();

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM booking", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bookings.Add(new Booking
                        {
                            BookingId = reader.GetInt32("booking_id"),
                            UserId = reader.GetInt32("user_id"),
                            BookingDate = reader.GetDateTime("booking_date"),
                            ArrivalDate = reader.GetDateTime("arrival_date"),
                            DepartureDate = reader.GetDateTime("departure_date"),
                           
                            NoOfGuests = reader.GetInt32("no_of_guests"),
                            CampingSpaceId = reader.GetInt32("campingspace_id")
                        });
                    }
                }
            }

            return bookings;
        }
        public IEnumerable<Booking> GetBookingsByCampingSpaceId(int campingSpaceId)
        {
            var bookings = new List<Booking>();

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand(
                    "SELECT b.booking_id, b.user_id, b.booking_date, b.arrival_date, b.departure_date, b.no_of_guests, b.campingspace_id " +
                    "FROM booking b " +
                    "JOIN campingspace cs ON b.campingspace_id = cs.campingspace_id " +
                    "WHERE b.campingspace_id = @CampingSpaceId", connection);
                cmd.Parameters.AddWithValue("@CampingSpaceId", campingSpaceId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bookings.Add(new Booking
                        {
                            BookingId = reader.GetInt32("booking_id"),
                            UserId = reader.GetInt32("user_id"),
                            BookingDate = reader.GetDateTime("booking_date"),
                            ArrivalDate = reader.GetDateTime("arrival_date"),
                            DepartureDate = reader.GetDateTime("departure_date"),
                            NoOfGuests = reader.GetInt32("no_of_guests"),
                            CampingSpaceId = reader.GetInt32("campingspace_id"),
                           
                        });
                    }
                }
            }

            return bookings;
        }

        public Booking GetBookingById(string id)
        {
            Booking booking = null;

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM booking WHERE booking_id = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        booking = new Booking
                        {
                            BookingId = reader.GetInt32("booking_id"),
                            UserId = reader.GetInt32("user_id"),
                            BookingDate = reader.GetDateTime("booking_date"),
                            ArrivalDate = reader.GetDateTime("arrival_date"),
                            DepartureDate = reader.GetDateTime("departure_date"),
                           
                            NoOfGuests = reader.GetInt32("no_of_guests"),
                            CampingSpaceId = reader.GetInt32("campingspace_id")
                        };
                    }
                }
            }

            return booking;
        }
        
        public IEnumerable<Booking> GetBookingsByUserId(int userId)
        {
            var bookings = new List<Booking>();

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand(
                    "SELECT b.booking_id, b.user_id, b.booking_date, b.arrival_date, b.departure_date, b.no_of_guests, b.campingspace_id, cs.name as name " +
                    "FROM booking b " +
                    "JOIN campingspace cs ON b.campingspace_id = cs.campingspace_id " +
                    "WHERE b.user_id = @UserId", connection);
                cmd.Parameters.AddWithValue("@UserId", userId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bookings.Add(new Booking
                        {
                            BookingId = reader.GetInt32("booking_id"),
                            UserId = reader.GetInt32("user_id"),
                            BookingDate = reader.GetDateTime("booking_date"),
                            ArrivalDate = reader.GetDateTime("arrival_date"),
                            DepartureDate = reader.GetDateTime("departure_date"),
                            
                            NoOfGuests = reader.GetInt32("no_of_guests"),
                            CampingSpaceId = reader.GetInt32("campingspace_id"),
                            CampingSpaceName = reader.GetString("name") // Retrieve campingspace name
                        });
                    }
                }
            }

            return bookings;
        }


        public void AddBooking(Booking booking)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Check if the user exists
                var userCheckCmd = new MySqlCommand("SELECT COUNT(*) FROM user WHERE user_id = @UserId", connection);
                userCheckCmd.Parameters.AddWithValue("@UserId", booking.UserId);
                var userExists = Convert.ToInt32(userCheckCmd.ExecuteScalar()) > 0;

                if (!userExists)
                {
                    throw new Exception("The provided user_id does not exist.");
                }
                

                // Insert the booking data
                var cmd = new MySqlCommand("INSERT INTO booking (user_id, booking_date, arrival_date, departure_date,  no_of_guests, campingspace_id) VALUES (@UserId, @BookingDate, @ArrivalDate, @DepartureDate, @NoOfGuests, @CampingSpaceId)", connection);
                cmd.Parameters.AddWithValue("@UserId", booking.UserId);
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ArrivalDate", booking.ArrivalDate);
                cmd.Parameters.AddWithValue("@DepartureDate", booking.DepartureDate);
                
                cmd.Parameters.AddWithValue("@NoOfGuests", booking.NoOfGuests);
                cmd.Parameters.AddWithValue("@CampingSpaceId", booking.CampingSpaceId);


                cmd.ExecuteNonQuery();
            }
        }

        // CampingSpaceAmenities methods
        public IEnumerable<CampingSpaceAmenities> GetCampingSpaceAmenities()
        {
            var campingSpaceAmenities = new List<CampingSpaceAmenities>();

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM campingspace_amenities", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        campingSpaceAmenities.Add(new CampingSpaceAmenities
                        {
                            Id = reader.GetInt32("id"),
                            CampingSpaceId = reader.GetInt32("campingspace_id"),
                            AmenitiesId = reader.GetInt32("amenities_id")
                        });
                    }
                }
            }

            return campingSpaceAmenities;
        }
        public CampingSpaceAmenities GetCampingSpaceAmenityById(int id)
        {
            CampingSpaceAmenities campingSpaceAmenity = null;

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM campingspace_amenities WHERE id = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        campingSpaceAmenity = new CampingSpaceAmenities
                        {
                            Id = reader.GetInt32("id"),
                            CampingSpaceId = reader.GetInt32("campingspace_id"),
                            AmenitiesId = reader.GetInt32("amenities_id")
                        };
                    }
                }
            }

            return campingSpaceAmenity;
        }

        public void AddCampingSpaceAmenity(CampingSpaceAmenities campingSpaceAmenity)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("INSERT INTO campingspace_amenities (campingspace_id, amenities_id) VALUES (@CampingSpaceId, @AmenitiesId)", connection);
                cmd.Parameters.AddWithValue("@CampingSpaceId", campingSpaceAmenity.CampingSpaceId);
                cmd.Parameters.AddWithValue("@AmenitiesId", campingSpaceAmenity.AmenitiesId);

                cmd.ExecuteNonQuery();
            }
        }

        // Image methods
        public IEnumerable<Image> GetImages()
        {
            var images = new List<Image>();

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM image", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        images.Add(new Image
                        {
                            ImageId = reader.GetInt32("image_id"),
                            ImageUrl = reader.GetString("imageUrl"),
                            CampingSpaceId = reader.IsDBNull("campingspace_id") ? (int?)null : reader.GetInt32("campingspace_id"),
                        });
                    }
                }
            }

            return images;
        }

        public Image GetImageById(int id)
        {
            Image image = null;

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM image WHERE image_id = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        image = new Image
                        {
                            ImageId = reader.GetInt32("image_id"),
                            ImageUrl = reader.GetString("imageUrl"),
                            CampingSpaceId = reader.IsDBNull("campingspace_id") ? (int?)null : reader.GetInt32("campingspace_id"),
                        };
                    }
                }
            }

            return image;
        }

        public void AddImage(Image image)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("INSERT INTO image (imageUrl, campingspace_id) VALUES (@ImageUrl, @CampingSpaceId)", connection);
                cmd.Parameters.AddWithValue("@ImageUrl", image.ImageUrl);
                cmd.Parameters.AddWithValue("@CampingSpaceId", image.CampingSpaceId);

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Image> GetImagesByCampingSpace(int campingSpaceId)
        {
            var images = new List<Image>();

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM image WHERE campingspace_id = @campingSpaceId", connection);
                cmd.Parameters.AddWithValue("@campingSpaceId", campingSpaceId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var relativePath = reader.GetString("imageUrl");
                        var imageUrl = $"http://localhost:8080/{relativePath}"; // Construct the correct URL

                        images.Add(new Image
                        {
                            ImageId = reader.GetInt32("image_id"),
                            ImageUrl = imageUrl,
                            CampingSpaceId = reader.GetInt32("campingspace_id")
                        });
                    }
                }
            }

            return images;
        }






        // Amenities methods
        public IEnumerable<Amenities> GetAmenities()
        {
            var amenities = new List<Amenities>();

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM amenities", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        amenities.Add(new Amenities
                        {
                            AmenitiesId = reader.GetInt32("amenities_id"),
                            TypeOfAmenities = reader.GetString("type_of_amenities"),
                            Description = reader.GetString("description")
                        });
                    }
                }
            }

            return amenities;
        }

        public Amenities GetAmenityById(int id)
        {
            Amenities amenity = null;

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM amenities WHERE amenities_id = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        amenity = new Amenities
                        {
                            AmenitiesId = reader.GetInt32("amenities_id"),
                            TypeOfAmenities = reader.GetString("type_of_amenities"),
                            Description = reader.GetString("description")
                        };
                    }
                }
            }

            return amenity;
        }

        public void AddAmenity(Amenities amenity)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("INSERT INTO amenities (type_of_amenities, description) VALUES (@TypeOfAmenities, @Description)", connection);
                cmd.Parameters.AddWithValue("@TypeOfAmenities", amenity.TypeOfAmenities);
                cmd.Parameters.AddWithValue("@Description", amenity.Description);

                cmd.ExecuteNonQuery();
            }
        }

        // CampingSpace methods
        // Method to fetch all camping spaces
        public IEnumerable<CampingSpace> GetCampingSpaces()
        {
            var campingSpaces = new Dictionary<int, CampingSpace>();

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand(@"SELECT cs.*, ca.amenities_id 
                                     FROM campingspace cs
                                     LEFT JOIN campingspace_amenities ca ON cs.campingspace_id = ca.campingspace_id", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var campingSpaceId = reader.GetInt32("campingspace_id");
                        if (!campingSpaces.ContainsKey(campingSpaceId))
                        {
                            campingSpaces[campingSpaceId] = new CampingSpace
                            {
                                CampingSpaceId = reader.GetInt32("campingspace_id"),
                                Name = reader.GetString("name"),
                                PricePerNight = reader.GetInt32("price_per_night"),
                                LocationId = reader.GetInt32("location_id"),
                                Longitude = reader.GetDecimal("longitude"),
                                Latitude = reader.GetDecimal("latitude"),
                                Address = reader.GetString("address"),
                                AmenitiesIds = new List<int>()
                            };
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("amenities_id")))
                        {
                            campingSpaces[campingSpaceId].AmenitiesIds.Add(reader.GetInt32("amenities_id"));
                        }
                    }
                }
            }

            return campingSpaces.Values;
        }
        public IEnumerable<CampingSpace> GetCampingSpacesByOwnerId(int ownerId)
        {
            var campingSpaces = new List<CampingSpace>();

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM campingspace WHERE owner_id = @OwnerId", connection);
                cmd.Parameters.AddWithValue("@OwnerId", ownerId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        campingSpaces.Add(new CampingSpace
                        {
                            CampingSpaceId = reader.GetInt32("campingspace_id"),
                            Name = reader.GetString("name"),
                            PricePerNight = reader.GetInt32("price_per_night"),
                            LocationId = reader.GetInt32("location_id"),
                            Longitude = reader.GetDecimal("longitude"),
                            Latitude = reader.GetDecimal("latitude"),
                            Address = reader.GetString("address"),
                            OwnerId = reader.GetInt32("owner_id"),
                            IsAvailable = reader.GetBoolean("is_available")
                        });
                    }
                }
            }

            return campingSpaces;
        }
        public void UpdateCampingSpaceAvailability(int campingSpaceId, bool availability)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("UPDATE campingspace SET is_available = @IsAvailable WHERE campingspace_id = @CampingSpaceId", connection);
                cmd.Parameters.AddWithValue("@IsAvailable", availability);
                cmd.Parameters.AddWithValue("@CampingSpaceId", campingSpaceId);
                cmd.ExecuteNonQuery();
            }
        }


        public CampingSpace GetCampingSpaceById(int id)
        {
            CampingSpace campingSpace = null;

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand(@"SELECT cs.*, ca.amenities_id 
                                     FROM campingspace cs
                                     LEFT JOIN campingspace_amenities ca ON cs.campingspace_id = ca.campingspace_id
                                     WHERE cs.campingspace_id = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (campingSpace == null)
                        {
                            campingSpace = new CampingSpace
                            {
                                CampingSpaceId = reader.GetInt32("campingspace_id"),
                                Name = reader.GetString("name"),
                                PricePerNight = reader.GetInt32("price_per_night"),
                                LocationId = reader.GetInt32("location_id"),
                                Longitude = reader.GetDecimal("longitude"),
                                Latitude = reader.GetDecimal("latitude"),
                                Address = reader.GetString("address"),
                                AmenitiesIds = new List<int>()
                            };
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("amenities_id")))
                        {
                            campingSpace.AmenitiesIds.Add(reader.GetInt32("amenities_id"));
                        }
                    }
                }
            }

            return campingSpace;
        }


        public void AddCampingSpace(CampingSpace campingSpace)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Check if the location exists
                var locationCheckCmd = new MySqlCommand("SELECT COUNT(*) FROM location WHERE location_id = @LocationId", connection);
                locationCheckCmd.Parameters.AddWithValue("@LocationId", campingSpace.LocationId);
                var locationExists = Convert.ToInt32(locationCheckCmd.ExecuteScalar()) > 0;

                if (!locationExists)
                {
                    throw new Exception("The provided location_id does not exist.");
                }

                // Check if the owner exists and is a host
                var ownerCheckCmd = new MySqlCommand("SELECT COUNT(*) FROM user WHERE user_id = @OwnerId AND type = 'host'", connection);
                ownerCheckCmd.Parameters.AddWithValue("@OwnerId", campingSpace.OwnerId);
                var ownerExists = Convert.ToInt32(ownerCheckCmd.ExecuteScalar()) > 0;

                if (!ownerExists)
                {
                    throw new Exception("The provided owner_id does not exist or is not a host.");
                }

                // Begin a transaction
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert the camping space data
                        var cmd = new MySqlCommand("INSERT INTO campingspace (name, price_per_night, location_id, longitude, latitude, address, owner_id) VALUES (@Name, @PricePerNight, @LocationId, @Longitude, @Latitude, @Address, @OwnerId)", connection, transaction);
                        cmd.Parameters.AddWithValue("@Name", campingSpace.Name);
                        cmd.Parameters.AddWithValue("@PricePerNight", campingSpace.PricePerNight);
                        cmd.Parameters.AddWithValue("@LocationId", campingSpace.LocationId);
                        cmd.Parameters.AddWithValue("@Longitude", campingSpace.Longitude);
                        cmd.Parameters.AddWithValue("@Latitude", campingSpace.Latitude);
                        cmd.Parameters.AddWithValue("@Address", campingSpace.Address);
                        cmd.Parameters.AddWithValue("@OwnerId", campingSpace.OwnerId);
                        cmd.ExecuteNonQuery();

                        // Get the last inserted id
                        var campingSpaceId = Convert.ToInt32(cmd.LastInsertedId);

                        // Insert into campingspace_amenities table for each amenities_id
                        foreach (var amenitiesId in campingSpace.AmenitiesIds)
                        {
                            var cmdAmenities = new MySqlCommand("INSERT INTO campingspace_amenities (campingspace_id, amenities_id) VALUES (@CampingSpaceId, @AmenitiesId)", connection, transaction);
                            cmdAmenities.Parameters.AddWithValue("@CampingSpaceId", campingSpaceId);
                            cmdAmenities.Parameters.AddWithValue("@AmenitiesId", amenitiesId);
                            cmdAmenities.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction in case of an error
                        transaction.Rollback();
                        throw new Exception("Error adding camping space", ex);
                    }
                }
            }
        }

        public bool GetCampingSpaceAvailability(int id)
        {
            bool isAvailable = false;

            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT is_available FROM campingspace WHERE campingspace_id = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        isAvailable = reader.GetBoolean("is_available");
                    }
                }
            }

            return isAvailable;
        }

        public bool CampingSpaceExists(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT COUNT(*) FROM campingspace WHERE campingspace_id = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }











        // Reviews methods
        public IEnumerable<Reviews> GetReviews()
        {
            var reviews = new List<Reviews>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM reviews", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new Reviews
                        {
                            ReviewsId = reader.GetInt32("reviews_id"),
                            ReviewText = reader.GetString("reviewText"),
                            Rating = reader.GetInt32("rating"),
                            UserId = reader.GetInt32("user_id"),
                            CampingSpaceId = reader.GetInt32("campingspace_id")
                        });
                    }
                }
            }

            return reviews;
        }

        public Reviews GetReviewById(int id)
        {
            Reviews review = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM reviews WHERE reviews_id = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        review = new Reviews
                        {
                            ReviewsId = reader.GetInt32("reviews_id"),
                            ReviewText = reader.GetString("reviewText"),
                            Rating = reader.GetInt32("rating"),
                            UserId = reader.GetInt32("user_id"),
                            CampingSpaceId = reader.GetInt32("campingspace_id")
                        };
                    }
                }
            }

            return review;
        }
        // New method to fetch reviews by camping space ID
        public IEnumerable<Reviews> GetReviewsByCampingSpaceId(int campingSpaceId)
        {
            var reviews = new List<Reviews>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new MySqlCommand("SELECT * FROM reviews WHERE campingspace_id = @campingSpaceId", connection);
                cmd.Parameters.AddWithValue("@campingSpaceId", campingSpaceId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new Reviews
                        {
                            ReviewsId = reader.GetInt32("reviews_id"),
                            ReviewText = reader.GetString("reviewText"),
                            Rating = reader.GetInt32("rating"),
                            UserId = reader.GetInt32("user_id"),
                            CampingSpaceId = reader.GetInt32("campingspace_id")
                        });
                    }
                }
            }

            return reviews;
        }

        public void AddReview(Reviews review)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new MySqlCommand("INSERT INTO reviews (reviewText, rating, user_id, campingspace_id) VALUES (@ReviewText, @Rating, @UserId, @CampingSpaceId)", connection);

                cmd.Parameters.AddWithValue("@ReviewText", review.ReviewText);
                cmd.Parameters.AddWithValue("@Rating", review.Rating);
                cmd.Parameters.AddWithValue("@UserId", review.UserId);
                cmd.Parameters.AddWithValue("@CampingSpaceId", review.CampingSpaceId);

                // Log the values to ensure they are correct
                Console.WriteLine($"Adding review with UserId: {review.UserId}, CampingSpaceId: {review.CampingSpaceId}");

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    // Log the error message
                    Console.WriteLine($"MySqlException: {ex.Message}");
                    throw;
                }
            }
        }

    
}
}