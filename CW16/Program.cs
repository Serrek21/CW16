using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Оберіть операцію:");
        Console.WriteLine("1. Копіювання файлу");
        Console.WriteLine("2. Переміщення файлу");
        Console.WriteLine("3. Копіювання папки");
        Console.WriteLine("4. Переміщення папки");
        Console.Write("Введіть номер операції: ");

        int operation;
        if (!int.TryParse(Console.ReadLine(), out operation) || operation < 1 || operation > 4)
        {
            Console.WriteLine("Невірний ввід.");
            return;
        }

        switch (operation)
        {
            case 1:
                CopyFile();
                break;
            case 2:
                MoveFile();
                break;
            case 3:
                CopyDirectory();
                break;
            case 4:
                MoveDirectory();
                break;
        }
    }

    static void CopyFile()
    {
        Console.WriteLine("Введіть шлях до оригінального файлу:");
        string sourceFilePath = Console.ReadLine();

        Console.WriteLine("Введіть шлях, куди потрібно скопіювати файл:");
        string destinationFilePath = Console.ReadLine();

        try
        {
            File.Copy(sourceFilePath, destinationFilePath, true);
            Console.WriteLine("Файл скопійовано успішно!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка копіювання файлу: {ex.Message}");
        }
    }

    static void MoveFile()
    {
        Console.WriteLine("Введіть шлях до оригінального файлу:");
        string sourceFilePath = Console.ReadLine();

        Console.WriteLine("Введіть шлях, куди потрібно перемістити файл:");
        string destinationFilePath = Console.ReadLine();

        try
        {
            File.Move(sourceFilePath, destinationFilePath);
            Console.WriteLine("Файл переміщено успішно!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка переміщення файлу: {ex.Message}");
        }
    }

    static void CopyDirectory()
    {
        Console.WriteLine("Введіть шлях до початкової папки:");
        string sourceDirectory = Console.ReadLine();

        Console.WriteLine("Введіть шлях, куди потрібно скопіювати папку:");
        string destinationDirectory = Console.ReadLine();

        try
        {
            CopyDirectoryRecursive(sourceDirectory, destinationDirectory);
            Console.WriteLine("Папку скопійовано успішно!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка копіювання папки: {ex.Message}");
        }
    }

    static void MoveDirectory()
    {
        Console.WriteLine("Введіть шлях до оригінальної папки:");
        string sourceDirectory = Console.ReadLine();

        Console.WriteLine("Введіть шлях, куди потрібно перемістити папку:");
        string destinationDirectory = Console.ReadLine();

        try
        {
            MoveDirectoryRecursive(sourceDirectory, destinationDirectory);
            Console.WriteLine("Папку переміщено успішно!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка переміщення папки: {ex.Message}");
        }
    }

    static void CopyDirectoryRecursive(string sourceDir, string destDir)
    {
        if (!Directory.Exists(sourceDir))
        {
            throw new DirectoryNotFoundException($"Початкова папка '{sourceDir}' не існує.");
        }

        if (!Directory.Exists(destDir))
        {
            Directory.CreateDirectory(destDir);
        }

        string[] files = Directory.GetFiles(sourceDir);
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(destDir, fileName);
            File.Copy(file, destFile, true);
        }

        string[] subDirectories = Directory.GetDirectories(sourceDir);
        foreach (string subDir in subDirectories)
        {
            string subDirName = Path.GetFileName(subDir);
            string destSubDir = Path.Combine(destDir, subDirName);
            CopyDirectoryRecursive(subDir, destSubDir);
        }
    }

    static void MoveDirectoryRecursive(string sourceDir, string destDir)
    {
        if (!Directory.Exists(sourceDir))
        {
            throw new DirectoryNotFoundException($"Початкова папка '{sourceDir}' не існує.");
        }

        if (!Directory.Exists(destDir))
        {
            Directory.CreateDirectory(destDir);
        }

        string[] files = Directory.GetFiles(sourceDir);
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(destDir, fileName);
            File.Move(file, destFile);
        }

        string[] subDirectories = Directory.GetDirectories(sourceDir);
        foreach (string subDir in subDirectories)
        {
            string subDirName = Path.GetFileName(subDir);
            string destSubDir = Path.Combine(destDir, subDirName);
            MoveDirectoryRecursive(subDir, destSubDir);
        }

        Directory.Delete(sourceDir);
    }
}