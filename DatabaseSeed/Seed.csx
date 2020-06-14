/*
    This script automates the seeding of the CatmashDatabase.db file.
    Args:
        Arg1: The initiale score to assign to each image.
*/

#r "nuget: Microsoft.EntityFrameworkCore.SQLite, 5.0.0-preview.2.20120.8"
#r "..\EntityModel\bin\Debug\netstandard2.0\EntityModel.dll"

using System. Runtime. CompilerServices;
using Catmash.EntityModel;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

private class ImageSequence
{
    public IEnumerable<Image> images { get; set; }
}

ImageSequence sequence;
string jsonImagesPath = Path.Combine(Environment.CurrentDirectory, "Images.json");
using (var fs = File.OpenRead(jsonImagesPath))
{
    using (var sr = new StreamReader(fs))
    {
        sequence = JsonSerializer.Deserialize<ImageSequence>(sr.ReadToEnd());
    }
}

string dbName = "CatmashDatabase.db";
string databasePath = Path.Combine(Environment.CurrentDirectory, dbName);
if (File.Exists(dbName))
{
    File.Delete(dbName);
}

var options = new DbContextOptionsBuilder<CatmashEntities>()
    .UseSqlite($"Data Source={databasePath}")
    .Options;
using (var dbContext = new CatmashEntities(options))
{
    dbContext.Database.EnsureCreated();
    sequence.images.AsParallel().ForAll(image => image.Score = int.Parse(Args[0]));
    dbContext.Images.AddRange(sequence.images);
    dbContext.SaveChanges();
}