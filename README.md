# Catmash
Vote for the cutest cat.

# Developers
1. Build the EntityModel class library with Release configuration: `dotnet build -c Release`.

2. Run Seed.csx script with `dotnet-script Seed.csx x y` where `x` and `y` are respectively the initial score and number of votes you want to assign to all images.

3. The output of the script is the initialized SQLite database file `CatmashDatabase.db`.

4. Move `CatmashDatabase.db` to the CatmashWeb folder as the connection string in Startup.cs points to that location.

5. From CatmashWeb folder, run the application with `dotnet run`