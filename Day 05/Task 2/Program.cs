
User user1 = new User("Артём");
User user2 = new User("Мария");


Smartphone[] phones = {
            new Smartphone("iPhone 15", 3500, user1),
            new Smartphone("Samsung S24", 4000, user1),
            new Smartphone("Xiaomi 14", 5000, user2)
        };


App whatsapp = new App("WhatsApp");
App telegram = new App("Telegram");
App youtube = new App("YouTube");

phones[0].InstallApp(whatsapp);
phones[0].InstallApp(telegram);

phones[1].InstallApp(whatsapp);
phones[1].InstallApp(youtube);

phones[2].InstallApp(telegram);
phones[2].InstallApp(youtube);

Console.WriteLine("=== ТЕЛЕФОНЫ И ПРИЛОЖЕНИЯ ===");
foreach (var phone in phones)
    phone.ShowInstalledApps();