using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiniAPP.Data;
using MiniAPP.DTOs;
using MiniAPP.Entities;
using MiniAPP.Entitiesp;
using MiniAPP.Validators;

namespace MiniApp;

class Program
{
    private static MiniAPPDbContext? _context;
    static void Main(string[] args)
    {
        _context = new MiniAPPDbContext();
        try
        {
            _context.Database.EnsureCreated();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Database initialized successfully.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing database: {ex.Message}");
            Console.WriteLine("Please ensure SQL Server is running and the connection string is correct.");
            return;
        }

        bool exit = false;
        while (!exit)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=========================================");
            Console.WriteLine("       RESTAURANT RESERVATION SYSTEM     ");
            Console.WriteLine($"      UTC Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine("=========================================\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  1. Add restaurant");
            Console.WriteLine("  2. List restaurants");
            Console.WriteLine("  3. Delete restaurant");
            Console.WriteLine("  4. Add dining table to restaurant");
            Console.WriteLine("  5. List dining tables by restaurant");
            Console.WriteLine("  6. Create reservation");
            Console.WriteLine("  7. List reservations by restaurant");
            Console.WriteLine("  8. Update reservation");
            Console.WriteLine("  9. Cancel reservation");
            Console.WriteLine("  0. Exit");
            Console.ResetColor();

            Console.Write("\nSelect an option: ");

            var choice = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        AddRestaurant();
                        break;
                    case "2":
                        ListRestaurants();
                        break;
                    case "3":
                        DeleteRestaurant();
                        break;
                    case "4":
                        AddDiningTable();
                        break;
                    case "5":
                        ListDiningTables();
                        break;
                    case "6":
                        CreateReservation();
                        break;
                    case "7":
                        ListReservations();
                        break;
                    case "8":
                        UpdateReservation();
                        break;
                    case "9":
                        CancelReservation();
                        break;
                    case "0":
                        exit = true;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Goodbye!");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Details: {ex.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            if (!exit)
            {
                Console.WriteLine("\nPress ENTER to return to menu...");
                Console.ReadLine();
            }
        }

        _context?.Dispose();
    }

    static void AddRestaurant()
    {
        var request = new CreateRestaurantRequest();

        Console.Write("Enter restaurant name: ");
        request.Name = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter city: ");
        request.City = Console.ReadLine() ?? string.Empty;

        var validator = new CreateRestaurantRequestValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            Console.WriteLine("Validation errors:");
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine($"- {error.ErrorMessage}");
            }
            return;
        }

        var restaurant = new Restaurant
        {
            Name = request.Name,
            City = request.City
        };

        _context!.Restaurants.Add(restaurant);
        _context.SaveChanges();
        Console.WriteLine($"Restaurant '{restaurant.Name}' added successfully with ID: {restaurant.Id}");
    }

    static void ListRestaurants()
    {
        var restaurants = _context!.Restaurants
            .OrderBy(r => r.Name)
            .ToList();

        if (!restaurants.Any())
        {
            Console.WriteLine("No restaurants found.");
            return;
        }

        Console.WriteLine("Restaurants:");
        foreach (var restaurant in restaurants)
        {
            Console.WriteLine($"ID: {restaurant.Id}, Name: {restaurant.Name}, City: {restaurant.City}, Created: {restaurant.CreatedAt:yyyy-MM-dd HH:mm:ss} UTC");
        }
    }

    static void DeleteRestaurant()
    {
        ListRestaurants();
        Console.Write("\nEnter restaurant ID to delete: ");

        if (!int.TryParse(Console.ReadLine(), out int restaurantId))
        {
            Console.WriteLine("Invalid restaurant ID.");
            return;
        }

        var restaurant = _context!.Restaurants
            .Include(r => r.Reservations)
            .FirstOrDefault(r => r.Id == restaurantId);

        if (restaurant == null)
        {
            Console.WriteLine("Restaurant not found.");
            return;
        }

        if (restaurant.Reservations.Any())
        {
            Console.WriteLine("Cannot delete restaurant with existing reservations. Please cancel all reservations first.");
            return;
        }

        _context.Restaurants.Remove(restaurant);
        _context.SaveChanges();
        Console.WriteLine($"Restaurant '{restaurant.Name}' deleted successfully.");
    }

    static void AddDiningTable()
    {
        ListRestaurants();
        Console.Write("\nEnter restaurant ID: ");

        if (!int.TryParse(Console.ReadLine(), out int restaurantId))
        {
            Console.WriteLine("Invalid restaurant ID.");
            return;
        }

        var restaurant = _context!.Restaurants.Find(restaurantId);
        if (restaurant == null)
        {
            Console.WriteLine("Restaurant not found.");
            return;
        }

        var request = new CreateDiningTableRequest
        {
            RestaurantId = restaurantId
        };

        Console.Write("Enter dining table number: ");
        if (!int.TryParse(Console.ReadLine(), out int tableNumber))
        {
            Console.WriteLine("Invalid table number.");
            return;
        }
        request.DiningTableNumber = tableNumber;

        Console.Write("Enter capacity: ");
        if (!int.TryParse(Console.ReadLine(), out int capacity))
        {
            Console.WriteLine("Invalid capacity.");
            return;
        }
        request.SeatingCapacity = capacity;

        var validator = new CreateDiningTableRequestValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            Console.WriteLine("Validation errors:");
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine($"- {error.ErrorMessage}");
            }
            return;
        }

        var diningTable = new DiningTable
        {
            RestaurantId = request.RestaurantId,
            DiningTableNumber = request.DiningTableNumber,
            SeatingCapacity = request.SeatingCapacity
        };

        _context.DiningTables.Add(diningTable);
        _context.SaveChanges();
        Console.WriteLine($"Dining table #{diningTable.DiningTableNumber} added successfully with ID: {diningTable.Id}");
    }

    static void ListDiningTables()
    {
        ListRestaurants();
        Console.Write("\nEnter restaurant ID: ");

        if (!int.TryParse(Console.ReadLine(), out int restaurantId))
        {
            Console.WriteLine("Invalid restaurant ID.");
            return;
        }

        var restaurant = _context!.Restaurants
            .Include(r => r.DiningTables)
            .FirstOrDefault(r => r.Id == restaurantId);

        if (restaurant == null)
        {
            Console.WriteLine("Restaurant not found.");
            return;
        }

        var diningTables = restaurant.DiningTables
            .OrderBy(dt => dt.DiningTableNumber)
            .ToList();

        if (!diningTables.Any())
        {
            Console.WriteLine("No dining tables found for this restaurant.");
            return;
        }

        Console.WriteLine($"\nDining tables for '{restaurant.Name}':");
        foreach (var table in diningTables)
        {
            Console.WriteLine($"ID: {table.Id}, Table #: {table.DiningTableNumber}, Capacity: {table.SeatingCapacity}, Active: {table.IsActive}");
        }
    }

    static void CreateReservation()
    {
        ListRestaurants();
        Console.Write("\nEnter restaurant ID: ");

        if (!int.TryParse(Console.ReadLine(), out int restaurantId))
        {
            Console.WriteLine("Invalid restaurant ID.");
            return;
        }

        var restaurant = _context!.Restaurants
            .Include(r => r.DiningTables.Where(dt => dt.IsActive))
            .FirstOrDefault(r => r.Id == restaurantId);

        if (restaurant == null)
        {
            Console.WriteLine("Restaurant not found.");
            return;
        }

        var activeTables = restaurant.DiningTables.ToList();
        if (!activeTables.Any())
        {
            Console.WriteLine("No active dining tables found for this restaurant.");
            return;
        }

        Console.WriteLine("\nActive dining tables:");
        foreach (var table in activeTables)
        {
            Console.WriteLine($"ID: {table.Id}, Table #: {table.DiningTableNumber}, Capacity: {table.SeatingCapacity}");
        }

        var request = new CreateReservationRequest
        {
            RestaurantId = restaurantId
        };

        Console.Write("\nEnter dining table ID: ");
        if (!int.TryParse(Console.ReadLine(), out int diningTableId))
        {
            Console.WriteLine("Invalid dining table ID.");
            return;
        }
        request.DiningTableId = diningTableId;

        var diningTable = activeTables.FirstOrDefault(dt => dt.Id == diningTableId);
        if (diningTable == null)
        {
            Console.WriteLine("Dining table not found or not active.");
            return;
        }

        Console.Write("Enter customer name: ");
        request.CustomerName = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter guest count: ");
        if (!int.TryParse(Console.ReadLine(), out int guestCount))
        {
            Console.WriteLine("Invalid guest count.");
            return;
        }
        request.GuestCount = guestCount;

        if (request.GuestCount > diningTable.SeatingCapacity)
        {
            Console.WriteLine($"Guest count ({request.GuestCount}) exceeds table capacity ({diningTable.SeatingCapacity}).");
            return;
        }

        Console.Write("Enter reservation date (yyyy-MM-dd HH:mm): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime reservationDate))
        {
            Console.WriteLine("Invalid date format.");
            return;
        }
        request.ReservationDate = reservationDate;

        var validator = new CreateReservationRequestValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            Console.WriteLine("Validation errors:");
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine($"- {error.ErrorMessage}");
            }
            return;
        }

        var reservation = new Reservation
        {
            RestaurantId = request.RestaurantId,
            DiningTableId = request.DiningTableId,
            CustomerName = request.CustomerName,
            GuestCount = request.GuestCount,
            ReservationDate = request.ReservationDate
        };

        _context.Reservations.Add(reservation);
        _context.SaveChanges();
        Console.WriteLine($"Reservation created successfully with ID: {reservation.Id}");
    }

    static void ListReservations()
    {
        ListRestaurants();
        Console.Write("\nEnter restaurant ID: ");

        if (!int.TryParse(Console.ReadLine(), out int restaurantId))
        {
            Console.WriteLine("Invalid restaurant ID.");
            return;
        }

        var restaurant = _context!.Restaurants.Find(restaurantId);
        if (restaurant == null)
        {
            Console.WriteLine("Restaurant not found.");
            return;
        }

        var reservations = _context.Reservations
            .Include(r => r.DiningTable)
            .Where(r => r.RestaurantId == restaurantId)
            .OrderBy(r => r.ReservationDate)
            .ToList();

        if (!reservations.Any())
        {
            Console.WriteLine("No reservations found for this restaurant.");
            return;
        }

        Console.WriteLine($"\nReservations for '{restaurant.Name}':");
        foreach (var reservation in reservations)
        {
            Console.WriteLine($"ID: {reservation.Id}, Customer: {reservation.CustomerName}, Table #: {reservation.DiningTable.DiningTableNumber}, " +
                $"Guests: {reservation.GuestCount}, Date: {reservation.ReservationDate:yyyy-MM-dd HH:mm}, " +
                $"Created: {reservation.CreatedAt:yyyy-MM-dd HH:mm:ss} UTC");
        }
    }

    static void UpdateReservation()
    {
        Console.Write("Enter reservation ID: ");

        if (!int.TryParse(Console.ReadLine(), out int reservationId))
        {
            Console.WriteLine("Invalid reservation ID.");
            return;
        }

        var reservation = _context!.Reservations
            .Include(r => r.DiningTable)
            .FirstOrDefault(r => r.Id == reservationId);

        if (reservation == null)
        {
            Console.WriteLine("Reservation not found.");
            return;
        }

        Console.WriteLine($"Current reservation: Customer: {reservation.CustomerName}, Table #: {reservation.DiningTable.DiningTableNumber}, " +
            $"Guests: {reservation.GuestCount}, Date: {reservation.ReservationDate:yyyy-MM-dd HH:mm}");

        var request = new UpdateReservationRequest
        {
            ReservationId = reservationId
        };

        Console.Write("\nEnter new reservation date (yyyy-MM-dd HH:mm) or press Enter to skip: ");
        var dateInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(dateInput))
        {
            if (DateTime.TryParse(dateInput, out DateTime newDate))
            {
                request.ReservationDate = newDate;
            }
            else
            {
                Console.WriteLine("Invalid date format.");
                return;
            }
        }

        Console.Write("Enter new guest count or press Enter to skip: ");
        var guestCountInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(guestCountInput))
        {
            if (int.TryParse(guestCountInput, out int newGuestCount))
            {
                request.GuestCount = newGuestCount;
            }
            else
            {
                Console.WriteLine("Invalid guest count.");
                return;
            }
        }

        var validator = new UpdateReservationRequestValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            Console.WriteLine("Validation errors:");
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine($"- {error.ErrorMessage}");
            }
            return;
        }

        if (request.GuestCount.HasValue)
        {
            if (request.GuestCount.Value > reservation.DiningTable.SeatingCapacity)
            {
                Console.WriteLine($"Guest count ({request.GuestCount.Value}) exceeds table capacity ({reservation.DiningTable.SeatingCapacity}).");
                return;
            }
            reservation.GuestCount = request.GuestCount.Value;
        }

        if (request.ReservationDate.HasValue)
        {
            reservation.ReservationDate = request.ReservationDate.Value;
        }

        _context.SaveChanges();
        Console.WriteLine("Reservation updated successfully.");
    }

    static void CancelReservation()
    {
        Console.Write("Enter reservation ID to cancel: ");

        if (!int.TryParse(Console.ReadLine(), out int reservationId))
        {
            Console.WriteLine("Invalid reservation ID.");
            return;
        }

        var reservation = _context!.Reservations
            .Include(r => r.DiningTable)
            .FirstOrDefault(r => r.Id == reservationId);

        if (reservation == null)
        {
            Console.WriteLine("Reservation not found.");
            return;
        }

        Console.WriteLine($"Reservation: Customer: {reservation.CustomerName}, Table #: {reservation.DiningTable.DiningTableNumber}, " +
            $"Date: {reservation.ReservationDate:yyyy-MM-dd HH:mm}");
        Console.Write("Are you sure you want to cancel this reservation? (y/n): ");

        var confirm = Console.ReadLine();
        if (confirm?.ToLower() != "y")
        {
            Console.WriteLine("Cancellation aborted.");
            return;
        }

        _context.Reservations.Remove(reservation);
        _context.SaveChanges();
        Console.WriteLine("Reservation cancelled successfully.");
    }
}
